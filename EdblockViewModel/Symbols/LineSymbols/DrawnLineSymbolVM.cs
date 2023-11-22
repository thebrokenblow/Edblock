using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

public class DrawnLineSymbolVM : SymbolVM
{
    public BlockSymbolVM? SymbolOutgoingLine { get; set; }
    public BlockSymbolVM? SymbolIncomingLine { get; set; }
    public DrawnLineSymbolModel DrawnLineSymbolModel { get; init; }
    public ObservableCollection<LineSymbolVM> LineSymbols { get; init; } = new();
    public ArrowSymbol ArrowSymbol { get; set; } = new();
    public PositionConnectionPoint PositionOutgoingConnectionPoint { get; init; }
    public PositionConnectionPoint IncomingPosition { get; set; }

    public DrawnLineSymbolVM(PositionConnectionPoint positionConnectionPoint, DrawnLineSymbolModel drawnLineSymbolModel)
    {
        DrawnLineSymbolModel = drawnLineSymbolModel;
        PositionOutgoingConnectionPoint = positionConnectionPoint;
    }

    public void ChangeCoordination(int currentX, int currentY)
    {
        var linesSymbolModel = DrawnLineSymbolModel.LinesSymbolModel;

        var startCoordinate = CoordinateLineModel.GetStartCoordinate(DrawnLineSymbolModel.LinesSymbolModel);

        (currentX, currentY) = DrawnLineSymbolModel.RoundingCoordinatesLines(startCoordinate, (currentX, currentY));
        ArrowSymbol.ChangeOrientationArrow(startCoordinate, (currentX, currentY), PositionOutgoingConnectionPoint);
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

    public void RedrawLines(List<SymbolLineModel> linesSymbolModel)
    {
        LineSymbols.Clear();

        foreach (var lineSymbolModel in linesSymbolModel)
        {
            var lineSymbolVM = FactoryLineSymbol.CreateLineByLineModel(lineSymbolModel);
            LineSymbols.Add(lineSymbolVM);
        }
    }

    private void AddMissingLines(List<SymbolLineModel> linesSymbolModel)
    {
        foreach (var lineSymbolModel in linesSymbolModel)
        {
            var lineSymbolVM = FactoryLineSymbol.CreateLineByLineModel(lineSymbolModel);
            LineSymbols.Add(lineSymbolVM);
        }
    }

    private void ChangeFirstLine(List<SymbolLineModel> linesSymbolModel)
    {
        var firstLineSymbolModel = linesSymbolModel[0];

        if (LineSymbols.Count > linesSymbolModel.Count)
        {
            LineSymbols.RemoveAt(1);
        }

        ChangeLastCoordinate(LineSymbols[0], firstLineSymbolModel);
    }

    private void ChangeSecondLine(List<SymbolLineModel> linesSymbolModel)
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

    private void ChangeCurrentLine(List<SymbolLineModel> linesSymbolModel)
    {
        for (int i = linesSymbolModel.Count - 2; i < linesSymbolModel.Count; i++)
        {
            ChangeCoordinate(LineSymbols[i], linesSymbolModel[i]);
        }
    }

    private static void ChangeCoordinate(LineSymbolVM lineSymbolVM, SymbolLineModel lineSymbolModel)
    {
        lineSymbolVM.X1 = lineSymbolModel.X1;
        lineSymbolVM.Y1 = lineSymbolModel.Y1;
        ChangeLastCoordinate(lineSymbolVM, lineSymbolModel);
    }

    private static void ChangeLastCoordinate(LineSymbolVM lineSymbolVM, SymbolLineModel lineSymbolModel)
    {
        lineSymbolVM.X2 = lineSymbolModel.X2;
        lineSymbolVM.Y2 = lineSymbolModel.Y2;
    }
}