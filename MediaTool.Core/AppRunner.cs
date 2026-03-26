namespace MediaTool.Core;

using MediaTool.Core.Specifications;

public class AppRunner
{
    private readonly AppOptions _options;

    public delegate IMetadataProcessor MetadataProcessorFactory(string path);

    MetadataProcessorFactory _metadataProcessorFactory;

    public AppRunner(AppOptions options,MetadataProcessorFactory metadataProcessorFactory) 
    {
        _options = options;
        _metadataProcessorFactory = metadataProcessorFactory;
    }

    public void Run()
    {
        if(_options.IsWriteMode)
        {
            ExecuteWrite();
        }else
        {
            if (_options.OperationType == "folder")
            {
                ProcessFolder();
            }
            else if (_options.OperationType == "photo")
            {
                ProcessFile(_options.Path);
            }
        }
    }

    private void ProcessFolder()
    {
        ISpecification<string> whiteSpec = new TrueSpecification<string>();
        if (_options.WhiteList.Any())
        {
            whiteSpec = new FalseSpecification<string>(); 
            foreach (var pattern in _options.WhiteList)
            {
                whiteSpec = whiteSpec.Or(new FileNameSpecification(pattern));
            }
        }

        ISpecification<string> blackSpec = new TrueSpecification<string>();
        foreach (var pattern in _options.BlackList)
        {
            blackSpec = blackSpec.And(new NotSpecification<string>(new FileNameSpecification(pattern)));
        }

        ISpecification<string> sizeSpec = new SizeSpecification(_options.MinSize, _options.MaxSize);

        var finalSpec = whiteSpec.And(blackSpec).And(sizeSpec);

        foreach (string file in Directory.GetFiles(_options.Path))
        {
            if (finalSpec.IsSatisfied(file))
            {
                ProcessFile(file);
            }
        }
    }


    private void ProcessFile(string path)
    {
        var processor = _metadataProcessorFactory(path);
        MetadataFormatter.Print(processor.Read(path));
    }


    private void ExecuteWrite()
    {
        var processor = _metadataProcessorFactory(_options.Path);
        processor.Write(_options.Path, _options.WriteValues);

#if DEBUG
Console.WriteLine($"[DEBUG] Запись успешна для {_options.Path}");
#endif
    }

}