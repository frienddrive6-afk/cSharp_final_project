namespace MediaTool.Infrastructure;

using System.Diagnostics;
using MediaTool.Core;

public static class CommandRunner
{
    public static CommandResult Execute(string fileName, string arguments)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,

        };

        Process process = new Process { StartInfo = startInfo };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        return new CommandResult
        {
            IsSuccess = process.ExitCode == 0,
            StandardOutput = output,
            StandardError = error,
            ExitCode = process.ExitCode
        };



    }

}
