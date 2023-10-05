using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols;

public class FactoryBlockSymbolModel
{
    private readonly Dictionary<string, Func<string, BlockSymbolModel>> instanceSymbolByName = new()
    {
        { "ActionSymbol", x => new ActionSymbolModel() }
    };

    public BlockSymbolModel Create(string nameBlockSymbol)
    {
        return instanceSymbolByName[nameBlockSymbol].Invoke(nameBlockSymbol);
    }
}