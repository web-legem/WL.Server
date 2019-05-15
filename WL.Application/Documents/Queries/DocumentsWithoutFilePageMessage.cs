namespace WL.Application.Documents.Queries {

  public class DocumentsWithoutFilePageMessage {

    // page configuration
    public long? PageSize { get; set; }

    public long? Page { get; set; }

    // filters
    public long? PublicationYear { get; set; }

    public string Number { get; set; }
    public long? EntityId { get; set; }
    public long? DocumentTypeId { get; set; }

    // order
    public string OrderBy { get; set; }

    public bool? Descend { get; set; }
  }
}