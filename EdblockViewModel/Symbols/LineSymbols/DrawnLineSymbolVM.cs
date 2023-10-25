using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

public class DrawnLineSymbolVM : Symbol
{
    public BlockSymbol? SymbolOutgoingLine { get; set; }
    public BlockSymbol? SymbolaIncomingLine { get; set; }
    public DrawnLineSymbolModel DrawnLineSymbolModel { get; init; }
    public ObservableCollection<LineSymbolVM> LineSymbols { get; init; } = new();
    public ArrowSymbol ArrowSymbol { get; set; } = new();
    public PositionConnectionPoint PositionConnectionPoint { get; init; }

    public DrawnLineSymbolVM(PositionConnectionPoint positionConnectionPoint, DrawnLineSymbolModel drawnLineSymbolModel)
    {
        DrawnLineSymbolModel = drawnLineSymbolModel;
        PositionConnectionPoint = positionConnectionPoint;
    }

    public void ChangeCoordination(int currentX, int currentY)
    {
        var linesSymbolModel = DrawnLineSymbolModel.LinesSymbols;

        var startCoordinate = CoordinateLineModel.GetStartCoordinate(DrawnLineSymbolModel.LinesSymbols);


        if (PositionConnectionPoint == PositionConnectionPoint.Bottom || PositionConnectionPoint == PositionConnectionPoint.Top)
        {
            if (linesSymbolModel.Count % 2 == 1)
            {
                if (startCoordinate.Item2 - 10 > currentY)
                {
                    currentY += 10;
                }
            }
            else
            {
                if (startCoordinate.Item1 - 20 > currentX)
                {
                    currentX += 10;
                }
            }
        }

        if (PositionConnectionPoint == PositionConnectionPoint.Left || PositionConnectionPoint == PositionConnectionPoint.Right)
        {
            if (linesSymbolModel.Count % 2 == 1)
            {
                if (startCoordinate.Item1 - 10 > currentX)
                {
                    currentX += 10;
                }
            }
            else
            {
                if (startCoordinate.Item2 - 10 > currentY)
                {
                    currentY += 10;
                }
            }
        }

        ArrowSymbol.ChangeOrientationArrow(startCoordinate, (currentX, currentY), PositionConnectionPoint);
        DrawnLineSymbolModel.ChangeCoordinateLine(currentX, currentY);

        if (LineSymbols.Count == 0)
        {
            AddMissingLines(linesSymbolModel);
        }
        else if (linesSymbolModel.Count == 1)
        {
            ChangeFirstLine(linesSymbolModel);
        }
        else
        {
            ChangeSecondLine(linesSymbolModel);
        }
    }

    private void AddMissingLines(List<LineSymbolModel> linesSymbolModel)
    {
        foreach (var lineSymbolModel in linesSymbolModel)
        {
            var lineSymbolVM = FactoryLineSymbol.CreateLineByLineModel(lineSymbolModel);
            LineSymbols.Add(lineSymbolVM);
        }
    }

    private void ChangeFirstLine(List<LineSymbolModel> linesSymbolModel)
    {
        var firstLineSymbolModel = linesSymbolModel[0];

        if (LineSymbols.Count > linesSymbolModel.Count)
        {
            LineSymbols.RemoveAt(1);
        }

        ChangeLastCoordinate(LineSymbols[0], firstLineSymbolModel);
    }

    private void ChangeSecondLine(List<LineSymbolModel> linesSymbolModel)
    {
        var currentLinesSymbolModel = linesSymbolModel.TakeLast(2).ToList();

        if (LineSymbols.Count > linesSymbolModel.Count)
        {
            LineSymbols.RemoveAt(LineSymbols.Count - 1);
        }
        else if (LineSymbols.Count < linesSymbolModel.Count)
        {
            LineSymbols.Add(FactoryLineSymbol.CreateLineByLineModel(currentLinesSymbolModel[1]));
        }

        ChangeCurrentLine(linesSymbolModel);
    }

    private void ChangeCurrentLine(List<LineSymbolModel> linesSymbolModel)
    {
        for (int i = linesSymbolModel.Count - 2; i < linesSymbolModel.Count; i++)
        {
            ChangeCoordinate(LineSymbols[i], linesSymbolModel[i]);
        }
    }

    private static void ChangeCoordinate(LineSymbolVM lineSymbolVM, LineSymbolModel lineSymbolModel)
    {
        lineSymbolVM.X1 = lineSymbolModel.X1;
        lineSymbolVM.Y1 = lineSymbolModel.Y1;
        ChangeLastCoordinate(lineSymbolVM, lineSymbolModel);
    }

    private static void ChangeLastCoordinate(LineSymbolVM lineSymbolVM, LineSymbolModel lineSymbolModel)
    {
        lineSymbolVM.X2 = lineSymbolModel.X2;
        lineSymbolVM.Y2 = lineSymbolModel.Y2;
    }
}