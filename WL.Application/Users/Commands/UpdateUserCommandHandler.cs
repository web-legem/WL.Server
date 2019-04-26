using LanguageExt;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;
using static LanguageExt.Prelude;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Common.FormValidations;
using static WL.Application.Users.UserValidations;

namespace WL.Application.Users.Commands {

  public class UpdateUserCommandHandler {
    readonly IUserRepository _uRepository;

    public UpdateUserCommandHandler(IUserRepository uRepository) {
      _uRepository = uRepository;
    }

    public Try<Validation<Error, UserDto>> Execute(UpdateUserCmd msg, Stream stream, Boolean fileWasChange, Boolean restorePass)
       => ()
       => from x in ValidateUpdateMsg(msg) // validaciones antes
          let user = CreatePassToUser(x.ToUser(), restorePass)
          let y = _uRepository.Update(user) // otras funciones despues
          let z = SavePhoto(stream, fileWasChange, y)
          select y.ToUserDTO();

    bool SavePhoto(Stream stream, bool fileWasChange, User user) {
      if (stream != null && fileWasChange) {
        UserHelpers.SaveFile(user.Id + ".png", stream);
        return true;
      }
      return false;
    }

    User CreatePassToUser(User user, Boolean restorePass) {
      if (restorePass) {
        MD5 md5provider = new MD5CryptoServiceProvider();

        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(user.IDDocument));
        var sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++) {
          sb.Append(bytes[i].ToString("x2"));
        }
        user.Password = sb.ToString();
      }
      return user;
    }

    Validation<Error, UpdateUserCmd> ValidateUpdateMsg(UpdateUserCmd msg)
       => from x in ValidateNonNull(msg)
          from y in (
                ValidateId(x.Id),
                ValidateFirstName(x.FirstName),
                ValidateLastName(x.LastName),
                ValidateDocument(x.Document),
                ValidateNickname(x.Nickname),
                validatePassword(x.Password),
                ValidateEmail(x.Email),
                ValidateRoleId(x.RoleId)
             )
            .Apply((id, x1, x2, x3, x4, x5, x6, x7) => msg)
          select y;

    Validation<Error, UpdatePasswordCmd> ValidateUpdatePasswordMsg(UpdatePasswordCmd msg)
       => from x in ValidateNonNull(msg)
          from y in (
                ValidateId(x.UserId),
                validatePassword(x.Password))
            .Apply((id, pass) => msg)
          select y;

    public Try<Validation<Error, UserDto>> Execute(UpdatePasswordCmd data, String token)
       => ()
       => from x in ValidateUpdatePasswordMsg(data)
          let user = PerformSideEfect(x, token)
          select user.ToUserDTO();

    User PerformSideEfect(UpdatePasswordCmd cmd, String token) {
      var user = _uRepository.Get(cmd.UserId);

      MD5 md5provider = new MD5CryptoServiceProvider();
      byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(cmd.Password));
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < bytes.Length; i++) {
        sb.Append(bytes[i].ToString("x2"));
      }
      user.Password = sb.ToString();
      return _uRepository.Update(user);
    }
  }
}