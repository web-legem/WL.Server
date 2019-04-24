using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain.User;

namespace WL.Persistance.Roles {

  internal class RolesConfig : IEntityTypeConfiguration<Role> {

    public void Configure(EntityTypeBuilder<Role> role) {
      role.HasAlternateKey(r => r.Name);
    }
  }
}