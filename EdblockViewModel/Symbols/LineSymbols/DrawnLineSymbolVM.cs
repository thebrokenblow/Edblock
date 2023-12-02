using System.Linq;
using Prism.Commands;
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
    public DrawnLineSymbolModel DrawnLineSymbolModel { get; set; }
    public ObservableCollection<LineSymbolVM> LinesSymbol { get; init; } = new();
    public ArrowSymbol ArrowSymbol { get; set; } = new();
    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public PositionConnectionPoint OutgoingPosition { get; init; }
    public PositionConnectionPoint IncomingPosition { get; set; }
    
    private const int heightTextField = 20;
    public int HeightTextField 
    {
        get => heightTextField;
    }

    private double widthTextField = 20;
    public double WidthTextField
    {
        get => widthTextField;
        set
        {
            widthTextField = value;

            SetCoordinateTextField();
        }
    }

    private double topCoordinateTextField;
    public double TopCoordinateTextField
    {
        get => topCoordinateTextField;
        set
        {
            topCoordinateTextField = value;
            OnPropertyChanged();
        }
    }

    private double leftCoordinateTextField;
    public double LeftCoordinateTextField
    {
        get => leftCoordinateTextField;
        set
        {
            leftCoordinateTextField = value;
            OnPropertyChanged();
        }
    }

    private readonly CanvasSymbolsVM _canvasSymbolsVM;

    public DrawnLineSymbolVM(PositionConnectionPoint positionConnectionPoint, DrawnLineSymbolModel drawnLineSymbolModel, CanvasSymbolsVM canvasSymbolsVM)
    {
        EnterCursor = new(SetHighlightColorLines);
        LeaveCursor = new(SetDefaultColorLines);

        DrawnLineSymbolModel = drawnLineSymbolModel;
        OutgoingPosition = positionConnectionPoint;

        _canvasSymbolsVM = canvasSymbolsVM;

        RedrawAllLines();
    }

    public void ChangeCoordination((int, int) currentCoordinte)
    {
        var linesSymbolModel = DrawnLineSymbolModel.LinesSymbolModel;
        var startCoordinate = DrawnLineSymbolModel.CoordinateLineModel.GetStartCoordinate();

        //currentCoordinte = DrawnLineSymbolModel.RoundingCoordinatesLines(startCoordinate, currentCoordinte);

        ArrowSymbol.ChangeOrientationArrow(startCoordinate, currentCoordinte, OutgoingPosition);
        DrawnLineSymbolModel.ChangeCoordinateLine(currentCoordinte);

        RedrawPartLines(linesSymbolModel);
    }

    private void SetCoordinateTextField()
    {
        var linesSymbol = DrawnLineSymbolModel.LinesSymbolModel;
        var firstLineSymbol = linesSymbol[0];

        LeftCoordinateTextField = firstLineSymbol.X1;
        TopCoordinateTextField = firstLineSymbol.Y1;

        if (OutgoingPosition != PositionConnectionPoint.Bottom)
        {
            TopCoordinateTextField -= heightTextField;
        }

        if (OutgoingPosition == PositionConnectionPoint.Left)
        {
            LeftCoordinateTextField -= widthTextField;
        }
    }

    public void RedrawAllLines()
    {
        LinesSymbol.Clear();

        foreach (var lineSymbolModel in DrawnLineSymbolModel.LinesSymbolModel)
        {
            var lineSymbolVM = FactoryLineSymbol.CreateLineByLineModel(lineSymbolModel);
            LinesSymbol.Add(lineSymbolVM);
        }

        SetCoordinateTextField();

        var lastLine = DrawnLineSymbolModel.LinesSymbolModel[^1];
        var coordinateLastLine = (lastLine.X2, lastLine.Y2);
        ArrowSymbol.ChangeOrientationArrow(coordinateLastLine, IncomingPosition);

    }

    private void SetHighlightColorLines()
    {
        if (_canvasSymbolsVM.DrawnLineSymbol != null)
        {
            return;
        }

        foreach (var lineSymbol in LinesSymbol)
        {
            lineSymbol.IsHighlighted = true;
        }

        ArrowSymbol.IsHighlighted = true;
    }

    private void SetDefaultColorLines()
    {
        foreach (var lineSymbol in LinesSymbol)
        {
            lineSymbol.IsHighlighted = false;
        }

        ArrowSymbol.IsHighlighted = false;
    }

    private void RedrawPartLines(List<LineSymbolModel> linesSymbolModel)
    {
        if (LinesSymbol.Count == 0)
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
            LinesSymbol.Add(lineSymbolVM);
        }
    }

    private void ChangeFirstLine(List<LineSymbolModel> linesSymbolModel)
    {
        var firstLineSymbolModel = linesSymbolModel[0];

        if (LinesSymbol.Count > linesSymbolModel.Count)
        {
            LinesSymbol.RemoveAt(1);
        }

        ChangeLastCoordinate(LinesSymbol[0], firstLineSymbolModel);
    }

    private void ChangeSecondLine(List<LineSymbolModel> linesSymbolModel)
    {
        var currentLinesSymbolModel = linesSymbolModel.TakeLast(2).ToList();

        if (LinesSymbol.Count > linesSymbolModel.Count)
        {
            LinesSymbol.RemoveAt(LinesSymbol.Count - 1);
        }
        else if (LinesSymbol.Count < linesSymbolModel.Count)
        {
            var secondLineModel = currentLinesSymbolModel[1];
            var secondLineVM = FactoryLineSymbol.CreateLineByLineModel(secondLineModel);
            LinesSymbol.Add(secondLineVM);
        }

        ChangeCurrentLine(linesSymbolModel);
    }

    private void ChangeCurrentLine(List<LineSymbolModel> linesSymbolModel)
    {
        for (int i = linesSymbolModel.Count - 2; i < linesSymbolModel.Count; i++)
        {
            ChangeCoordinate(LinesSymbol[i], linesSymbolModel[i]);
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