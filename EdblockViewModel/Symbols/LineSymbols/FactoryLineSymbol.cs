using EdblockModel.Symbols.LineSymbols;
using EdblockModel.SymbolsModel.LineSymbols;

namespace EdblockViewModel.Symbols.LineSymbols;

public class FactoryLineSymbol
{
    public static LineSymbolVM CreateLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolVM(lineSymbolModel)
        {
            X1 = lineSymbolModel.X2,
            Y1 = lineSymbolModel.Y2,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2,
        };

        return lineSymbol;
    }

    public static LineSymbolVM CreateLineByLineModel(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolVM(lineSymbolModel)
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2,
        };

        return lineSymbol;
    }
}