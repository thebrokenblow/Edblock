using System.Collections.ObjectModel;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ListLineSymbol : Symbol
{
    public ObservableCollection<LineSymbol> LineSymbols { get; init; } = new();
    public ListLineSymbolModel LineSymbolModel { get; init; } = new();

    public ListLineSymbol(LineSymbolModel lineSymbolModel)
    {
        LineSymbolModel.LinesSymbols.Add(lineSymbolModel);
    }

    public void ChangeCoordination(int x, int y)
    {
        var linesSymbModel = LineSymbolModel.GetLines(x, y);

        if (linesSymbModel.Count == 1)
        {
            LineSymbols.Clear();
            var lineSymbol = FactoryLineSymbol.CreateLine(linesSymbModel[0]);
            LineSymbols.Add(lineSymbol);
        }
        else
        {
            LineSymbols.Clear();
            for (int i = 0; i < linesSymbModel.Count; i++)
            {
                var lineSymbol = FactoryLineSymbol.CreateLine(linesSymbModel[i]);
                LineSymbols.Add(lineSymbol);
            }
        }
    }
}