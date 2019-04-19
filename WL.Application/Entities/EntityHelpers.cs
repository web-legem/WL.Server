using WL.Application.Entities.Commands;
using WL.Domain;

namespace WL.Application.Entities {

  public static class EntityHelpers {

    public static EntityDto ToEntityDto(this Entity entity) {
      return new EntityDto {
        Id = entity.EntityId,
        Name = entity.Name,
        Email = entity.Email,
        EntityType = entity.EntityType.EntityTypeId,
      };
    }

    public static Entity ToEntity(this CreateEntityCommand cmd) {
      return new Entity {
        Name = cmd.Name,
        Email = cmd.Email,
        EntityTypeId = cmd.EntityTypeId
      };
    }

    public static Entity ToEntity(this UpdateEntityCommand cmd) {
      return new Entity {
        EntityId = cmd.Id,
        Name = cmd.Name,
        Email = cmd.Email,
        EntityTypeId = cmd.EntityType
      };
    }
  }
}