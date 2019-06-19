using System;
using System.Security.Cryptography;
using System.Text;
using LanguageExt;
using WL.Application.Common;
using WL.Application.Interfaces.Persistance;
using WL.Application.Users.Commands;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Users.UserValidations;
using static LanguageExt.Prelude;
using System.Linq;
using WL.Domain.User;
using System.Net;
using WL.Application.Helpers;
using WL.Application.Common.Errors;
using System.Collections.Generic;

namespace WL.Application.Users.Queries {

   public class SessionQuery {
      readonly IUserRepository _repository;
      readonly ICredentialRepository _CreRepo;
      readonly IRestoreRepository _ResRepo;
      readonly IRoleRepository _RoleRepo;

      public SessionQuery(IUserRepository repository, ICredentialRepository CreRepo,
         IRestoreRepository ResRepo, IRoleRepository roleRepository) {
         _repository = repository;
         _CreRepo = CreRepo;
         _ResRepo = ResRepo;
         _RoleRepo = roleRepository;
      }

      // sign in -----------------------------------------------------------

      public Try<Validation<Error, CredentialDto>> Execute(UserCredentialCmd data, string address)
         => ()
         => from x in ValidateUserCredentialMsg(data)
            select PerformSideEfect(x, data.Password, address);

      Validation<Error, UserCredentialCmd> ValidateUserCredentialMsg(UserCredentialCmd msg)
        => from x in ValidateNonNull(msg)
           from y in (
                 ValidateNickname(x.Nickname),
                 validatePassword(x.Password))
             .Apply((v1, v2) => msg)
           select y;

      CredentialDto PerformSideEfect(UserCredentialCmd cmd, string password, string address) {
         var user = _repository.Get(cmd.Nickname);
         var credential = new CredentialDto();

         MD5 md5provider = new MD5CryptoServiceProvider();
         var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(user.IDDocument));
         var sb = new StringBuilder();
         for (int i = 0; i < bytes.Length; i++) {
            sb.Append(bytes[i].ToString("x2"));
         }
         var documentMd5 = sb.ToString();

         credential.id = user.Id;
         if (user.Password.Equals(password)) {
            if (!documentMd5.Equals(password)) {
               credential.firstName = user.FirstName;
               credential.newPasswordRequired = false;
               credential.token = GetUniqueToken(user.Id);
               string hostName = Dns.GetHostName();
               var myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

               if (UserHelpers.existThumbnail(user.Id)) {
                  credential.photo = address + "/api/User/Photo?id=" + user.Id + "&mode=min";
               } else {
                  credential.photo = "none";
               }
               Credential cre = new Credential {
                  UserId = credential.id,
                  Token = credential.token,
               };
               _CreRepo.Create(cre);
               Role role = _RoleRepo.Get(credential.token);
               credential.entityId = user.EntityId;

               credential.permissions = new List<Perm>{
                     new Perm { Name = "configSystem", Can = role.ConfigSystem == 1 },
                     new Perm { Name = "createDocuments", Can = role.CreateDocuments == 1 },
                     new Perm { Name = "deleteDocuments", Can = role.DeleteDocuments == 1 }
                 };
            } else {
               credential.newPasswordRequired = true;
            }
         } else {
            throw new FormFieldError(FormFieldError.invalidUserCredentials);
         }
         return credential;
      }

      string GetUniqueToken(long id) {
         byte[] key = Guid.NewGuid().ToByteArray();
         byte[] idb = BitConverter.GetBytes(id);
         string token = Convert.ToBase64String(idb.Concat(key).ToArray());
         return token;
      }

      // sign out -----------------------------------------------------------

      public Try<Unit> Execute(long id) {
         return () => fun((long x) => _CreRepo.Delete(x))(id);
      }

      // generate restore token ---------------------------------------------

      public Try<bool> ExecuteRestorePassword(string email, string Address) {
         return () => {
            string token = Guid.NewGuid().ToString();
            long userId = _ResRepo.CreateRestoreToken(email, token);
            if (userId == 0) {
               return false;
            }
            string link = Address + "?id=" + userId + "&token=" + token + "";
            string subject = "WebLegem: Recuperación de contraseña";
            string msg = "Recibimos una solicitud de cambio de contraseña. Para confirmar tu nueva contraseña haz click en el siguiente enlace:"
               + "<br/>"
               + "<p><a href = \"" + link + "\" > Link de recuperacion </a></p> "
               + "<br/>"
               + "Por favor, ignora este mensaje en el caso que no hayas solicitado un cambio de contraseña de tu cuenta.";
            return SendMail.Send(email, subject, msg);
         };
      }

      // verify token -------------------------------------------------------

      public Try<bool> VerifyToken(long id, string token) {
         return ()
            => _ResRepo.IsValidToken(id, token);
      }

      public struct Perm {
         public string Name { get; set; }
         public bool Can { get; set; }
         public Perm(string name, bool can) {
            Name = name;
            Can = can;
         }
      }

   }

}