using System;
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

        var startCoordinate = GetStartCoordinate();

        ArrowSymbol.ChangeOrientationArrow(startCoordinate, currentX, currentY, positionConnectionPoint);

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

    private Tuple<int, int> GetStartCoordinate() //Получение начальных координат в зависимости от уже нарисованных линий
    {
        if (LineSymbols.Count == 1 || LineSymbols.Count == 2)
        {
            var firstLine = LineSymbols[0];

            return new Tuple<int, int>(firstLine.X1, firstLine.Y1);
        }
        else if (LineSymbols.Count % 2 == 1)
        {
            var lastLine = LineSymbols[^1];

            return new Tuple<int, int>(lastLine.X1, lastLine.Y1);
        }
        else
        {
            var penultimateLine = LineSymbols[^2];

            return new Tuple<int, int>(penultimateLine.X1, penultimateLine.Y1);
        }
    }
}