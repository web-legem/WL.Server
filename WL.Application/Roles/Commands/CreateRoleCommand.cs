namespace WL.Application.Roles.Commands {

  public class CreateRoleCommand {
    public string Name { get; set; }
    public int ConfigSystem { get; set; }
    public int CreateDocuments { get; set; }
    public int DeleteDocuments { get; set; }
  }
}