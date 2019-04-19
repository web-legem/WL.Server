namespace WL.Application.Entities.Commands {

  public class CreateEntityCommand {
    public string Name { get; set; }
    public string Email { get; set; }
    public long EntityTypeId { get; set; }
  }
}