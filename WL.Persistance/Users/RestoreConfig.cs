using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain.User;

namespace WL.Persistance.Users {

  public class RestoresConfig : IEntityTypeConfiguration<Restore> {

    public void Configure(EntityTypeBuilder<Restore> restore) {
      restore.HasKey(r => r.UserId);

      restore.HasOne(r => r.User)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}