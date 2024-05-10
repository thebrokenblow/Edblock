using EdblockViewModel.ComponentsVM;
using EdblockModel.SymbolsModel.LineSymbolsModel;

namespace Edblock.SymbolsViewModel.Symbols.LineSymbols;

public class FactoryLineSymbol
{
    public static LineSymbolVM CreateLine(DrawnLineSymbolVM drawnLineSymbolVM, LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolVM(drawnLineSymbolVM, lineSymbolModel)
        {
            X1 = lineSymbolModel.X2,
            Y1 = lineSymbolModel.Y2,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2,
        };

        return lineSymbol;
    }

    public static LineSymbolVM CreateLineByLineModel(DrawnLineSymbolVM drawnLineSymbolVM, LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolVM(drawnLineSymbolVM, lineSymbolModel)
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2,
        };

        return lineSymbol;
    }
}