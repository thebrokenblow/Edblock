namespace Edblock.SymbolsSerialization.Interfaces;

public interface ISerializationProject
{
    void Write(ProjectSerializable projectSerializable, string pathFile);
    Task<ProjectSerializable> Read(string pathFile);
}
