using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WL.Domain.User;

namespace WL.Persistance.Users {

  internal class UsersConfig : IEntityTypeConfiguration<User> {

    public void Configure(EntityTypeBuilder<User> user) {
      user.HasKey(x => x.Id);
      user.HasIndex(usr => usr.Email).IsUnique();
      user.HasIndex(usr => usr.IDDocument).IsUnique();
      user.HasIndex(usr => usr.Nickname).IsUnique();

      SeedData(user);
    }

    private void SeedData(EntityTypeBuilder<User> users) {
      users.HasData(
        new {
          Id = 1L,
          Nickname = "admin",
          FirstName = "admin",
          LastName = "admin",
          IDDocument = "123456789",
          Password = "202cb962ac59075b964b07152d234b70",
          Email = "andres_solarte@hotmail.com",
          State = "active",
          RoleId = 1L
        }
        );
    }
  }
}