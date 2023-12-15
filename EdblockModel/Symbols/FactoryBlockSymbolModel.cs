namespace EdblockModel.Symbols;

public class FactoryBlockSymbolModel
{
    private readonly static Dictionary<string, Func<string, BlockSymbolModel>> instanceSymbolByName = new()
    {
        { "ActionSymbol", _ => new ActionSymbolModel() { Id = _id, NameSymbol = _nameBlockSymbol, Color = _color} }
    };

    private static string? _id;
    private static string? _nameBlockSymbol;
    private static string? _color;

    public static BlockSymbolModel Create(string? id, string? nameBlockSymbol, string? color)
    {
        _id = id;
        _color = color;
        _nameBlockSymbol = nameBlockSymbol;

        if (_nameBlockSymbol is null)
        {
            throw new NullReferenceException("NameBlockSymbol is null");
        }

        return instanceSymbolByName[_nameBlockSymbol].Invoke(_nameBlockSymbol);
    }
}