//using LanguageExt;

//using System;
//using System.Linq;

//using WL.Application.Interfaces.Persistance;

//using static LanguageExt.Prelude;

//namespace WL.Application.Entities.Queries {
//  public class GetAllEntitiesByUserQuery {
//    readonly IEntityRepository _repository;

//    public GetAllEntitiesByUserQuery(IEntityRepository repository) {
//      _repository = repository;
//    }

//    public Try<IQueryable<EntityDto>> Execute(string token) {
//      Func<IQueryable<EntityDto>> action = ()
//        => _repository
//        .GetAllByUser(token)
//        .Select(x => x.ToEntityDto());

//      return Try(action);
//    }
//  }
//}