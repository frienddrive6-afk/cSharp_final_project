using MediaTool.Core;
using MediaTool.Infrastructure;

AppOptions options = CommandLineParser.Parse(args);

AppRunner runner = new AppRunner(options, ProcessorFactory.Create); 

runner.Run();