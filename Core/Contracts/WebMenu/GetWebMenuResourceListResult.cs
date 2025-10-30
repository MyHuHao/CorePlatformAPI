namespace Core.Contracts.WebMenu;

public class GetGetWebMenuRoutelListResult
{
  public string ParentId { get; set; } = "";
  public int Sequence { get; set; }
  public string Path { get; set; } = "";
  public string Name { get; set; } = "";
  public RouterMeta Meta { get; set; } = new RouterMeta();
  public string Src { get; set; } = "";
  public List<GetGetWebMenuRoutelListResult> Children { get; set; } = [];
}

public class RouterMeta
{
  public string Title { get; set; } = "";
  public string Icon { get; set; } = "";
  public bool KeepAlive { get; set; }
}