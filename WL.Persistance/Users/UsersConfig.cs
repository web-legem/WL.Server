using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WL.Domain.User;

namespace WL.Persistance.Users {

  internal class UsersConfig : IEntityTypeConfiguration<User> {

    public void Configure(EntityTypeBuilder<User> user) {
      user.HasKey(x => x.Id);
      user.HasAlternateKey(usr => usr.Email);
      user.HasAlternateKey(usr => usr.IDDocument);
      user.HasAlternateKey(usr => usr.Nickname);
    }
  }
}