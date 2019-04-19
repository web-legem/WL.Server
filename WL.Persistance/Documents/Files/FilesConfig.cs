using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;
using WL.Domain;

namespace WL.Persistance.Documents.Files {

  public class FilesConfig : IEntityTypeConfiguration<File> {

    public void Configure(EntityTypeBuilder<File> file) {
      file.HasKey(f => f.DocumentId);

      file.HasOne(f => f.Document)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}