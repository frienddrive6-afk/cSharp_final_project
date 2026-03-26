namespace MediaTool.Core;

public interface IMetadataProcessor
{
    public MetadataContainer Read(string filePath);

    public void Write(string filePath, MetadataContainer data);
}
