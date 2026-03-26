using MediaTool.Core;
using MediaTool.Infrastructure;

AppOptions options = CommandLineParser.Parse(args);

AppRunner runner = new AppRunner(options, ProcessorFactory.Create); 
#if DEBUG
    Console.WriteLine($"[DEBUG] OpType: {options.OperationType}, Path: {options.Path}, WriteMode: {options.IsWriteMode}");
#endif
runner.Run();









// Тест записи video
// var processor = new Mp4Processor();

// // 1. Создаем контейнер с новыми данными
// var newMetadata = new MetadataContainer {
//     Common = new CommonMetadata { 
//         Author = "Roma Studio", 
//         FileName = "My Awesome Video" 
//     }
// };

// Console.WriteLine("Записываю метаданные в 1.mp4...");
// processor.Write("video_test/1.mp4", newMetadata);

// var updated = processor.Read("video_test/1.mp4");
// MetadataFormatter.Print(updated);





// // Тест записи photo
// var testContainer = new MetadataContainer {
//     Common = new CommonMetadata { Author = "Roma Architect" },
//     Photo = new PhotoMetadata { Model = "SuperCamera 3000", Iso = 400 }
// };

// var processor = new JpegProcessor();
// string fullPath = Path.GetFullPath("photos_test/Iphone_1.jpg");
// processor.Write(fullPath, testContainer);

// var updatedData = processor.Read("photos_test/Iphone_1.jpg");
// MetadataFormatter.Print(updatedData);








// // Тест запуска внешнего процесса
// CommandResult result = CommandRunner.Execute("exiftool", "-ver"); // Просто просим вернуть версию

// if (result.IsSuccess)
// {
//     Console.WriteLine($"ExifTool найден, версия: {result.StandardOutput}");
// }
// else
// {
//     Console.WriteLine($"Ошибка запуска ExifTool: {result.StandardError}");
// }


