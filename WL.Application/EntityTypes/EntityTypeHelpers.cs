using System.Linq;

using WL.Application.EntityTypes.Commands;
using WL.Domain;

namespace WL.Application.EntityTypes {

  public static class EntityTypeHelpers {

    public static EntityTypeDto ToTipoEntidadDTO(this EntityType entityType)
      => new EntityTypeDto {
        Id = entityType.Id,
        Name = entityType.Name,
        SupportedDocumentTypesIds = entityType
          .SupportedDocuments
          .Select(x => x.DocumentTypeId)
      };

    public static EntityType CreateEntityTypeFrom(CreateEntityTypeCommand cmd)
      => new EntityType {
        Name = cmd.Name,
        SupportedDocuments = cmd.SupportedDocumentTypesIds
          .Select(id => new EntityTypeDocumentType {
            DocumentTypeId = id
          }).ToList()
      };

    public static EntityType CreateEntityTypeFrom(UpdateEntityTypeCommand cmd)
      => new EntityType {
        Id = cmd.Id,
        Name = cmd.Name,
        SupportedDocuments = cmd.SupportedDocumentTypesIds
          .Select(id => new EntityTypeDocumentType {
            DocumentTypeId = id
          }).ToList()
      };
  }
}