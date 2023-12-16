using System.Linq;
using Prism.Commands;
using EdblockModel.Symbols.Enum;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockModel.Symbols.LineSymbols.RedrawLine;

namespace EdblockViewModel.Symbols.LineSymbols;

public class DrawnLineSymbolVM : SymbolVM
{
    public DrawnLineSymbolModel DrawnLineSymbolModel { get; set; }
    public ObservableCollection<LineSymbolVM> LinesSymbolVM { get; init; }
    public ObservableCollection<MovableRectangleLine> MovableRectanglesLine { get; init; }
    public ArrowSymbol ArrowSymbol { get; set; }
    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public ConnectionPoint? OutgoingConnectionPoint { get; init; }
    public ConnectionPoint? IncomingConnectionPoint { get; set; }

    private BlockSymbolVM? symbolOutgoingLine;
    public BlockSymbolVM? SymbolOutgoingLine
    {
        get => symbolOutgoingLine;
        init
        {
            symbolOutgoingLine = value;
            DrawnLineSymbolModel.SymbolOutgoingLine = symbolOutgoingLine?.BlockSymbolModel;
        }
    }

    private BlockSymbolVM? symbolIncomingLine;
    public BlockSymbolVM? SymbolIncomingLine
    {
        get => symbolIncomingLine;
        set
        {
            symbolIncomingLine = value;
            DrawnLineSymbolModel.SymbolIncomingLine = symbolIncomingLine?.BlockSymbolModel;
        }
    }

    private PositionConnectionPoint outgoingPosition;
    public PositionConnectionPoint OutgoingPosition
    {
        get => outgoingPosition;
        set
        {
            outgoingPosition = value;
            DrawnLineSymbolModel.OutgoingPosition = outgoingPosition;
        }
    }

    private PositionConnectionPoint incomingPosition;
    public PositionConnectionPoint IncomingPosition 
    {
        get => incomingPosition;
        set
        {
            incomingPosition = value;
            DrawnLineSymbolModel.IncomingPosition = incomingPosition;
        }
    }

    private const string defaultText = "да";
    private string? text;
    public string? Text 
    {
        get => text;
        set
        {
            text = value;
            DrawnLineSymbolModel.Text = text;
        }
    }

    private const string defaultColor = "#000000";
    private string? color;
    public override string? Color
    {
        get => color;
        set
        {
            color = value;
            DrawnLineSymbolModel.Color = color;
        }
    }
    
    private const int heightTextField = 20;
    public static int HeightTextField 
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

            if (OutgoingPosition == PositionConnectionPoint.Left)
            {
                SetCoordinateTextField();
            }
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

    public CanvasSymbolsVM CanvasSymbolsVM { get; init; }

    public DrawnLineSymbolVM(CanvasSymbolsVM canvasSymbolsVM, List<LineSymbolModel>? linesSymbolModel = null)
    {
        EnterCursor = new(SetHighlightColorLines);
        LeaveCursor = new(SetDefaultColorLines);

        ArrowSymbol = new();
        LinesSymbolVM = new();
        DrawnLineSymbolModel = new();
        MovableRectanglesLine = new();

        Text = defaultText;
        Color = defaultColor;

        CanvasSymbolsVM = canvasSymbolsVM;

        if (linesSymbolModel != null)
        {
            DrawnLineSymbolModel.LinesSymbolModel = linesSymbolModel;
        }
    }

    public void AddFirstLine()
    {
        DrawnLineSymbolModel.AddFirstLine();
    }

    public void RedrawMovableRectanglesLine()
    {
        if (MovableRectanglesLine.Count != LinesSymbolVM.Count - 2)
        {
            for (int i = 1; i < LinesSymbolVM.Count - 1; i++)
            {
                var lineSymbolVM = LinesSymbolVM[i];
                var movableRectangleLine = new MovableRectangleLine(this, lineSymbolVM);
                MovableRectanglesLine.Add(movableRectangleLine);
            }
        }
        else
        {
            foreach (var movableRectangleLine in MovableRectanglesLine)
            {
                movableRectangleLine.SetCoordinate();
            }
        }
    }

    public void SetDefaultColorLines()
    {
        var selectDrawnLineSymbol = CanvasSymbolsVM.SelectDrawnLineSymbol;
        var movableRectangleLine = CanvasSymbolsVM.MovableRectangleLine;

        if (selectDrawnLineSymbol != this && movableRectangleLine == null)
        {
            SetHighlightStatus(false);
            HideMovableRectanglesLine();
        }
    }

    public void ChangeCoordination((int, int) currentCoordinte)
    {
        var startCoordinate = DrawnLineSymbolModel.CoordinateLineModel.GetStartCoordinate();

        currentCoordinte = DrawnLineSymbolModel.RoundingCoordinatesLines(startCoordinate, currentCoordinte);

        ArrowSymbol.ChangeOrientationArrow(startCoordinate, currentCoordinte, OutgoingPosition);
        DrawnLineSymbolModel.ChangeCoordinateLine(currentCoordinte);

        RedrawPartLines();
    }

    public void RedrawAllLines()
    {
        LinesSymbolVM.Clear();
        MovableRectanglesLine.Clear();

        var linesSymbolModel = DrawnLineSymbolModel.LinesSymbolModel;

        foreach (var lineSymbolModel in linesSymbolModel)
        {
            var lineSymbolVM = FactoryLineSymbol.CreateLineByLineModel(lineSymbolModel);
            LinesSymbolVM.Add(lineSymbolVM);
        }

        SetCoordinateTextField();
        RedrawMovableRectanglesLine();

        var lastLine = linesSymbolModel[^1];
        var coordinateLastLine = (lastLine.X2, lastLine.Y2);
        ArrowSymbol.ChangeOrientationArrow(coordinateLastLine, IncomingPosition);
    }

    private void ShowMovableRectanglesLine()
    {
        SetDisplateMovableRectanglesStatus(true);
    }

    private void HideMovableRectanglesLine()
    {
        SetDisplateMovableRectanglesStatus(false);
    }

    private void SetDisplateMovableRectanglesStatus(bool displayStatus)
    {
        foreach (var movableRectangleLine in MovableRectanglesLine)
        {
            movableRectangleLine.IsShow = displayStatus;
        }
    }

    public void SelectLine()
    {
        var selectDrawnLineSymbol = CanvasSymbolsVM.SelectDrawnLineSymbol;

        if (selectDrawnLineSymbol != this && selectDrawnLineSymbol != null)
        {
            CanvasSymbolsVM.SelectDrawnLineSymbol = null;
            selectDrawnLineSymbol.SetDefaultColorLines();
        }

        SetHighlightColorLines();
        ShowMovableRectanglesLine();
        CanvasSymbolsVM.SelectDrawnLineSymbol = this;
    }

    private void SetCoordinateTextField()
    {
        var linesSymbolModel = DrawnLineSymbolModel.LinesSymbolModel;
        var firstLineSymbolModel = linesSymbolModel[0];

        LeftCoordinateTextField = firstLineSymbolModel.X1;
        TopCoordinateTextField = firstLineSymbolModel.Y1;

        if (OutgoingPosition != PositionConnectionPoint.Bottom)
        {
            TopCoordinateTextField -= heightTextField;
        }

        if (OutgoingPosition == PositionConnectionPoint.Left)
        {
            LeftCoordinateTextField -= widthTextField;
        }
    }

    private void SetHighlightColorLines()
    {
        var movableSymbol = CanvasSymbolsVM.MovableBlockSymbol;
        var drawnLineSymbol = CanvasSymbolsVM.DrawnLineSymbol;

        if (movableSymbol == null && drawnLineSymbol == null)
        {
            SetHighlightStatus(true);
        }
    }

    private void SetHighlightStatus(bool status)
    {
        foreach (var lineSymbol in LinesSymbolVM)
        {
            lineSymbol.IsHighlighted = status;
        }

        ArrowSymbol.IsHighlighted = status;
    }

    public void RedrawPartLines()
    {
        var linesSymbolModel = DrawnLineSymbolModel.LinesSymbolModel;

        if (LinesSymbolVM.Count == 0)
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
            LinesSymbolVM.Add(lineSymbolVM);
        }
    }

    private void ChangeFirstLine(List<LineSymbolModel> linesSymbolModel)
    {
        var firstLineSymbolModel = linesSymbolModel[0];

        var countLinesVM = LinesSymbolVM.Count;
        var countLinesModel = linesSymbolModel.Count;

        if (countLinesVM > countLinesModel)
        {
            LinesSymbolVM.RemoveAt(1);
        }

        ChangeLastCoordinate(LinesSymbolVM[0], firstLineSymbolModel);
    }

    private void ChangeSecondLine(List<LineSymbolModel> linesSymbolModel)
    {
        var currentLinesSymbolModel = linesSymbolModel.TakeLast(2).ToList();

        var countLinesVM = LinesSymbolVM.Count;
        var countLinesModel = linesSymbolModel.Count;

        if (countLinesVM > countLinesModel)
        {
            LinesSymbolVM.RemoveAt(countLinesVM - 1);
        }
        else if (countLinesVM < countLinesModel)
        {
            var secondLineModel = currentLinesSymbolModel[1];
            var secondLineVM = FactoryLineSymbol.CreateLineByLineModel(secondLineModel);
            LinesSymbolVM.Add(secondLineVM);
        }

        ChangeCurrentLine(linesSymbolModel);
    }

    private void ChangeCurrentLine(List<LineSymbolModel> linesSymbolModel)
    {
        var countLinesSymbolModel = linesSymbolModel.Count;

        for (int i = countLinesSymbolModel - 2; i < countLinesSymbolModel; i++)
        {
            ChangeCurrentCoordinate(LinesSymbolVM[i], linesSymbolModel[i]);
        }
    }

    private static void ChangeCurrentCoordinate(LineSymbolVM lineSymbolVM, LineSymbolModel lineSymbolModel)
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

    internal void Redraw()
    {
        var redrawLineSymbolVM = new RedrawnLine(DrawnLineSymbolModel);
        var redrawnLinesModel = redrawLineSymbolVM.GetRedrawLine();
        DrawnLineSymbolModel.LinesSymbolModel = redrawnLinesModel;

        RedrawAllLines();
    }

    internal void AddLine()
    {
        if (LinesSymbolVM.Count > 1)
        {
            var currentLineSymbolModel = DrawnLineSymbolModel.GetNewLine();
            var currentLineSymbolVM = FactoryLineSymbol.CreateLine(currentLineSymbolModel);

            LinesSymbolVM.Add(currentLineSymbolVM);
        }
    }
}