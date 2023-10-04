using System.Collections.ObjectModel;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class ListLineSymbol : Symbol
{
    public BlockSymbol? SymbolOutgoingLine { get; set; }
    public BlockSymbol? SymbolaIncomingLine { get; set; }
    public ObservableCollection<LineSymbol> LineSymbols { get; init; } = new();
    public ListLineSymbolModel LineSymbolModel { get; init; } = new();
    public ArrowSymbol ArrowSymbol { get; set; } = new();

    public ListLineSymbol(LineSymbolModel lineSymbolModel)
    {
        LineSymbolModel.LinesSymbols.Add(lineSymbolModel);
    }

    public void ChangeCoordination(int currentX, int currentY)
    {
        var firstLine = LineSymbols[0];

        int startX = firstLine.X1;
        int startY = firstLine.Y1;

        ArrowSymbol.ChangeOrientationArrow(startX, startY, currentX, currentY);

        var linesSymbModel = LineSymbolModel.GetLines(currentX, currentY);
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