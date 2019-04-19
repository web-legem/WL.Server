namespace WL.Application.Entities.Commands {

  public class UpdateEntityCommand {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public long EntityType { get; set; }
  }
}