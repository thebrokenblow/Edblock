using System.Collections.ObjectModel;
using System.Linq;
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

        var startCoordinate = CoordinateLineModel.GetStartCoordinate(LineSymbolModel.LinesSymbols);
        var currentCoordinateLine = (currentX, currentY);

        ArrowSymbol.ChangeOrientationArrow(startCoordinate, currentCoordinateLine, positionConnectionPoint);
        LineSymbolModel.GetLines(currentX, currentY);

        if (LineSymbolModel.LinesSymbols.Count >= 2)
        {
            var linesSymbModel = LineSymbolModel.LinesSymbols.TakeLast(2).ToList();

            if (LineSymbolModel.LinesSymbols.Count < LineSymbols.Count)
            {
                LineSymbols.RemoveAt(LineSymbols.Count - 1);
                LineSymbols[^1] = FactoryLineSymbol.CreateLine(linesSymbModel[1]);
            }
            else if (LineSymbolModel.LinesSymbols.Count > LineSymbols.Count)
            {
                LineSymbols[^1] = FactoryLineSymbol.CreateLine(linesSymbModel[0]);
                LineSymbols.Add(FactoryLineSymbol.CreateLine(linesSymbModel[1]));
            }
            else if (LineSymbolModel.LinesSymbols.Count == LineSymbols.Count)
            {
                for (int i = LineSymbolModel.LinesSymbols.Count - 2; i < LineSymbolModel.LinesSymbols.Count; i++)
                {
                    LineSymbols[i] = FactoryLineSymbol.CreateLine(LineSymbolModel.LinesSymbols[i]);
                }
            }
        }
        else
        {
            var lineSymbol = FactoryLineSymbol.CreateLine(LineSymbolModel.LinesSymbols[0]);
            if (LineSymbols.Count == 0)
            {
                LineSymbols.Add(lineSymbol);
            }
            else
            {
                LineSymbols[0] = lineSymbol;
            }
        }

    }
}