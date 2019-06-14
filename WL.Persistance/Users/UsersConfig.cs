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

      user.HasOne(usr => usr.Entity)
        .WithMany()
        .IsRequired(false);

      user.HasOne(usr => usr.Role)
        .WithMany()
        .OnDelete(DeleteBehavior.Restrict);

      SeedData(user);
      user.Property(dt => dt.Id)
        .HasDefaultValueSql("\"UsersSeq\".NEXTVAL");
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
        },
        new {
          Id = 2L,
          Nickname = "andres",
          FirstName = "andres",
          LastName = "andres",
          IDDocument = "1085284234",
          Password = "202cb962ac59075b964b07152d234b70",
          Email = "andres.9010@hotmail.com",
          State = "active",
          RoleId = 1L
        },
        new {
          Id = 3L,
          Nickname = "mario",
          FirstName = "mario",
          LastName = "mario",
          IDDocument = "111111",
          Password = "202cb962ac59075b964b07152d234b70",
          Email = "marioffdsw@gmail.com",
          State = "active",
          RoleId = 1L
        },
        new {
          Id = 4L,
          Nickname = "adry",
          FirstName = "adry",
          LastName = "adry",
          IDDocument = "222222",
          Password = "202cb962ac59075b964b07152d234b70",
          Email = "andry2507@gmail.com",
          State = "active",
          RoleId = 1L
        }
        );
    }
  }
}