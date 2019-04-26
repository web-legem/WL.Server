using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;

namespace WL.Persistance.Users {

   public class RestoreRepository : IRestoreRepository {
      readonly WLDbContext context;

      public RestoreRepository(WLDbContext context) {
         this.context = context;
      }

      public long CreateRestoreToken(string email, string token) {
         try {
            var user = context.Users.FirstOrDefault(x => x.Email == email);
            if (user != null) {
               var original = context.RestoreCredentials.FirstOrDefault(r => r.UserId == user.Id);
               if (original != null) {
                  original.Token = token;
                  context.SaveChanges();
               } else {
                  var restore = new RestoreCredential { UserId = user.Id, User = user, Token = token };
                  context.RestoreCredentials.Add(restore);
                  context.SaveChanges();
               }
               return user.Id;
            }

            // TODO - que pasa si el usuario no existe?
            return 0;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public bool IsValidToken(long userId, string token) {
         try {
            // TODO - que hacer si no lo encuentra, validar
            var restore = context.RestoreCredentials.Find(userId);
            return restore.Token == token;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public RestoreCredential Get(long id) => throw new NotImplementedException();

      public IQueryable<RestoreCredential> GetAll() => throw new NotImplementedException();

      public RestoreCredential Create(RestoreCredential entity) => throw new NotImplementedException();

      public RestoreCredential Update(RestoreCredential entity) => throw new NotImplementedException();

      public void Delete(long id) => throw new NotImplementedException();
   }
}