using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols;

public class FactoryBlockSymbolModel
{
    public static BlockSymbolModel Create(string nameBlockSymbol)
    {
        if (nameBlockSymbol == "ActionSymbol")
        {
            return new ActionSymbolModel();
        }

        throw new Exception("Нет символа с таким именем, возможно вы ошиблись в названии");
    }
}