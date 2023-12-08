using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols;

public class FactoryBlockSymbolModel
{
    private readonly Dictionary<string, Func<string, BlockSymbolModel>> instanceSymbolByName = new()
    {
        { "ActionSymbol", x => new ActionSymbolModel(_id, _nameBlockSymbol) }
    };

    private static string? _id;
    private static string? _nameBlockSymbol;

    public BlockSymbolModel Create(string? nameBlockSymbol, string id)
    {
        _id = id;

        if (nameBlockSymbol == null)
        {
            throw new NullReferenceException("Некорректное имя символа");    
        }

        _nameBlockSymbol = nameBlockSymbol;

        return instanceSymbolByName[nameBlockSymbol].Invoke(nameBlockSymbol);
    }
}