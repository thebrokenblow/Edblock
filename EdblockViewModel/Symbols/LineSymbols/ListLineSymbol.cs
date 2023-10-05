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
        var positionConnectionPoint = LineSymbolModel.LinesSymbols[0].PositionConnectionPoint;

        int startX = 0;
        int startY = 0;

        if (LineSymbols.Count == 1 || LineSymbols.Count == 2)
        {
            var firstLine = LineSymbols[0];

            startX = firstLine.X1;
            startY = firstLine.Y1;
        }
        else if (LineSymbols.Count % 2 == 1)
        {
            var firstLine = LineSymbols[^1];

            startX = firstLine.X1;
            startY = firstLine.Y1;
        }
        else
        {
            var firstLine = LineSymbols[^2];

            startX = firstLine.X1;
            startY = firstLine.Y1;
        }

        ArrowSymbol.ChangeOrientationArrow(startX, startY, currentX, currentY, positionConnectionPoint);

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