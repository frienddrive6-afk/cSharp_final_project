namespace MediaTool.Core.Specifications;

public class FileNameSpecification : ISpecification<string>
{
    private readonly string _excludedPattern;
    public FileNameSpecification(string excludedPattern) => _excludedPattern = excludedPattern;


    public bool IsSatisfied(string fullPath)
    {
        string fileName = Path.GetFileName(fullPath);
        
        if (string.IsNullOrEmpty(_excludedPattern)) return true;
        return fileName.Contains(_excludedPattern);
    }


}