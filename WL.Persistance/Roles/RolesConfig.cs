using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain.User;

namespace WL.Persistance.Roles {

  internal class RolesConfig : IEntityTypeConfiguration<Role> {

    public void Configure(EntityTypeBuilder<Role> role) {
      role.HasIndex(r => r.Name).IsUnique();
      SeedData(role);
      role.Property(dt => dt.Id)
        .HasDefaultValueSql("\"RolesSeq\".NEXTVAL");
    }

    private void SeedData(EntityTypeBuilder<Role> roles) {
      roles.HasData(
          new { Id = 1L, Name = "Super Admin", ConfigSystem = 1, CreateDocuments = 1, DeleteDocuments = 1 }
        );
    }
  }
}