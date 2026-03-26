namespace MediaTool.Core;




public class AppOptions
{
    public string OperationType {get; set; } = string.Empty;

    public string Path {get; set; } = string.Empty;

    public long MinSize {get; set; } = 0;
    public long MaxSize {get; set; } = long.MaxValue;

    public List<string> WhiteList {get; set; } = new List<string>();
    public List<string> BlackList {get; set; } = new List<string>();

}