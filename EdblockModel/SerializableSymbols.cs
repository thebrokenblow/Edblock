using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.LineSymbols;

namespace EdblockModel;

public class SerializableSymbols
{
    public List<BlockSymbolModel> blocksSymbolModel { get; init; } = new();
    public List<ListLineSymbolModel> linesSymbolModel { get; init; } = new();

    public void SaveProject(string pathFile)
    {

    }
}