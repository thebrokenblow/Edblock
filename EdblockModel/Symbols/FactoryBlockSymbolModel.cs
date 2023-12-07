using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols;

public class FactoryBlockSymbolModel
{
    private readonly Dictionary<string, Func<string, BlockSymbolModel>> instanceSymbolByName = new()
    {
        { "ActionSymbol", x => new ActionSymbolModel(_id) }
    };

    private static string _id;

    public BlockSymbolModel Create(string nameBlockSymbol, string id)
    {
        _id = id;
        return instanceSymbolByName[nameBlockSymbol].Invoke(nameBlockSymbol);
    }
}