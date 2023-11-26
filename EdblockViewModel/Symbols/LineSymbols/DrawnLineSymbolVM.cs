using System.Linq;
using EdblockModel.Symbols.Enum;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class DrawnLineSymbolVM : SymbolVM
{
    public BlockSymbolVM? SymbolOutgoingLine { get; set; }
    public BlockSymbolVM? SymbolIncomingLine { get; set; }
    public DrawnLineSymbolModel DrawnLineSymbolModel { get; init; }
    public ObservableCollection<LineSymbolVM> LineSymbols { get; init; } = new();
    public ArrowSymbol ArrowSymbol { get; set; } = new();
    public PositionConnectionPoint OutgoingPosition { get; init; }
    public PositionConnectionPoint IncomingPosition { get; set; }

    public DrawnLineSymbolVM(PositionConnectionPoint positionConnectionPoint, DrawnLineSymbolModel drawnLineSymbolModel)
    {
        DrawnLineSymbolModel = drawnLineSymbolModel;
        OutgoingPosition = positionConnectionPoint;
    }

    public void ChangeCoordination((int, int) currentCoordinte)
    {
        var linesSymbolModel = DrawnLineSymbolModel.LinesSymbolModel;
        var startCoordinate = DrawnLineSymbolModel.CoordinateLineModel.GetStartCoordinate();

        currentCoordinte = DrawnLineSymbolModel.RoundingCoordinatesLines(startCoordinate, currentCoordinte);

        ArrowSymbol.ChangeOrientationArrow(startCoordinate, currentCoordinte, OutgoingPosition);
        DrawnLineSymbolModel.ChangeCoordinateLine(currentCoordinte);

        RedrawPartLines(linesSymbolModel);
    }

    public void RedrawAllLines(List<LineSymbolModel> linesSymbolModel)
    {
        LineSymbols.Clear();

        foreach (var lineSymbolModel in linesSymbolModel)
        {
            var lineSymbolVM = FactoryLineSymbol.CreateLineByLineModel(lineSymbolModel);
            LineSymbols.Add(lineSymbolVM);
        }

        var lastLine = linesSymbolModel[^1];
        var coordinateLastLine = (lastLine.X2, lastLine.Y2);
        ArrowSymbol.ChangeOrientationArrow(coordinateLastLine, IncomingPosition);

    }

    private void RedrawPartLines(List<LineSymbolModel> linesSymbolModel)
    {
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