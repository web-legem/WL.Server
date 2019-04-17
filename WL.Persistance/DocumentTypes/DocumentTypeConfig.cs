using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Domain;

namespace WL.Persistance.DocumentTypes {

  internal class DocumentTypeConfig : IEntityTypeConfiguration<DocumentType> {

    public void Configure(EntityTypeBuilder<DocumentType> documentType) {
      documentType.HasAlternateKey(dt => dt.Name);
    }
  }
}