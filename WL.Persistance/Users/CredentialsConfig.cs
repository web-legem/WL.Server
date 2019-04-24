using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WL.Domain.User;

namespace WL.Persistance.Users {

  internal class CredentialsConfig : IEntityTypeConfiguration<Credential> {

    public void Configure(EntityTypeBuilder<Credential> credential) {
      credential.HasKey(c => c.UserId);

      credential.HasOne(c => c.User)
        .WithOne(u => u.Credential)
        .OnDelete(DeleteBehavior.Cascade);

      credential.HasAlternateKey(c => c.Token);
    }
  }
}