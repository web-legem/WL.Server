using System;
using System.Collections.Generic;
using System.Linq;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;

namespace WL.Persistance.Users {
   public class UserRepository : IUserRepository {
      readonly WLDbContext context;

      public UserRepository(WLDbContext context) {
         this.context = context;
      }

      public User Get(string nickname) {
         try {
            return context.Users.FirstOrDefault(u => u.Nickname == nickname);
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public User Get(long id) {
         try {
            return context.Users.Find(id);
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public IQueryable<User> GetAll() {
         try {
            return context.Users;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public User Create(User user) {
         try {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public User Update(User entity) {
         try {
            var original = context.Users.Find(entity.Id);
            original.Nickname = entity.Nickname;
            original.LastName = entity.LastName;
            original.Password = entity.Password;
            original.IDDocument = entity.IDDocument;
            original.RoleId = entity.RoleId;
            original.FirstName = entity.FirstName;
            original.State = entity.State;
            original.Email = entity.Email;
            context.SaveChanges();
            return original;
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }

      public void Delete(long id) {
         try {
            var original = context.Users.Find(id);
            context.Users.Remove(original);
            context.SaveChanges();
         }
         catch (Exception e) {
            throw ExceptionsToValidations.ExceptionsToValidations.WrapOracleExceptionsWithError(e);
         }
      }
   }
}