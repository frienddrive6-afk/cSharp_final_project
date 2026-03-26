namespace MediaTool.Core;



public class CommandResult
{
    public bool IsSuccess { get; set; }
    public string StandardOutput { get; set; } = string.Empty;

    public string StandardError { get; set; } = string.Empty;
    public int ExitCode { get; set; }

}