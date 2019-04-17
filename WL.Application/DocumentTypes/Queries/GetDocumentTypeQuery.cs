using LanguageExt;

using WL.Application.Interfaces.Persistance;

namespace WL.Application.DocumentTypes.Queries {

  public class GetDocumentTypeQuery {
    readonly IDocumentTypeRepository repository;

    public GetDocumentTypeQuery(IDocumentTypeRepository repository) {
      this.repository = repository;
    }

    public Try<DocumentTypeDto> Execute(long id)
      => ()
      => repository
        .Get(id)
        .ToDocumentTypeDto();
  }
}