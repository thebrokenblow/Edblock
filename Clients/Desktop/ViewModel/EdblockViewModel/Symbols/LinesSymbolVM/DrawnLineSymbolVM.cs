using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using Prism.Commands;
using EdblockViewModel.Symbols.LinesSymbolVM.Components;
using EdblockViewModel.Core;
using EdblockModel.Lines;
using EdblockModel.Lines.DecoratorLine;
using System.Collections.Generic;

namespace EdblockViewModel.Symbols.LinesSymbolVM;

public class DrawnLineSymbolVM : ObservableObject
{
    public ObservableCollection<LineSymbolVM> LinesSymbolVM { get; } = [];
    public ObservableCollection<MovableRectangleLineVM> MovableRectanglesLineVM { get; } = [];
    public ConnectionPointVM? OutgoingConnectionPoint { get; set; }
    public ConnectionPointVM? IncommingConnectionPoint { get; set; }
    public BlockSymbolVM? OutgoingBlockSymbol { get; set; }
    public BlockSymbolVM? IncommingBlockSymbol { get; set; }
    public ArrowLineVM ArrowLineVM { get; set; }

    private readonly DrawnLineSymbolModel drawnLineSymbolModel = new(); 

    public ICanvasSymbolsComponentVM CanvasSymbolsComponentVM { get; }

    public DelegateCommand HighlightDrawnLineCommand { get; }
    public DelegateCommand UnhighlightDrawnLineCommand { get; }

    private readonly Brush? selectedBrush; 
    private const string highlightStroke = "#00FF00";
    private readonly FactoryLineVM factoryLineVM = new(); 
    public DrawnLineSymbolVM(ICanvasSymbolsComponentVM canvasSymbolsComponentVM)
    {
        var selectedBrushConverter = new BrushConverter().ConvertFrom(highlightStroke);

        if (selectedBrushConverter is not null)
        {
            selectedBrush = (Brush)selectedBrushConverter;
        }

        ArrowLineVM = new(drawnLineSymbolModel.ArrowLineModel);

        CanvasSymbolsComponentVM = canvasSymbolsComponentVM;

        HighlightDrawnLineCommand = new(HighlightDrawnLine);
        UnhighlightDrawnLineCommand = new(UnhighlightDrawnLine);
    }

    private const string defaultText = "Да";

    private string? text;
    public string? Text
    {
        get => text;
        set
        {
            text = value;
            drawnLineSymbolModel.Text = text;
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

            if (OutgoingConnectionPoint?.Position == SideSymbol.Left)
            {
                SetCoordinateTextField();
            }
        }
    }

    public void SetCoordinateTextField()
    {
        var firstLineSymbolModel = LinesSymbolVM[0];

        LeftOffsetTextField = firstLineSymbolModel.X1;
        TopOffsetTextField = firstLineSymbolModel.Y1;

        if (OutgoingConnectionPoint?.Position != SideSymbol.Bottom)
        {
            TopOffsetTextField -= heightTextField;
        }

        if (OutgoingConnectionPoint?.Position == SideSymbol.Left)
        {
            LeftOffsetTextField -= widthTextField;
        }
    }

    private double topOffsetTextField;
    public double TopOffsetTextField
    {
        get => topOffsetTextField;
        set
        {
            topOffsetTextField = value;
            OnPropertyChanged();
        }
    }

    private double leftOffsetTextField;
    public double LeftOffsetTextField
    {
        get => leftOffsetTextField;
        set
        {
            leftOffsetTextField = value;
            OnPropertyChanged();
        }
    }

    private bool isShowTextField;
    public bool IsShowTextField
    {
        get => isShowTextField;
        set
        {
            isShowTextField = value;
            OnPropertyChanged();
        }
    }

    public bool IsSelect { get; set; }

    private void HighlightDrawnLine()
    {
        if (selectedBrush is null || CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM == this || IsSelect)
        {
            return;
        }

        SetStroke(selectedBrush);

        foreach (var movableRectangleLineVM in MovableRectanglesLineVM)
        {
            if (!movableRectangleLineVM.LinesSymbolVM.IsZero())
            {
                movableRectangleLineVM.IsShow = true;
            }
        }

        if (CanvasSymbolsComponentVM.Cursor != Cursors.SizeWE && CanvasSymbolsComponentVM.Cursor != Cursors.SizeNS)
        {
            CanvasSymbolsComponentVM.Cursor = Cursors.Hand;
        }
    }

    private void UnhighlightDrawnLine()
    {
        if (CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM == this || IsSelect || CanvasSymbolsComponentVM.MovableRectangleLineVM is not null)
        {
            return;
        }

        SetStroke(Brushes.Black);
        
        foreach (var movableRectangleLineVM in MovableRectanglesLineVM)
        {
            movableRectangleLineVM.IsShow = false;
        }

        CanvasSymbolsComponentVM.Cursor = Cursors.Arrow;
    }

    public void CreateFirstLine(ConnectionPointVM? outgoingConnectionPoint, double startXCoordinate, double startYCoordinate)
    {
        OutgoingConnectionPoint = outgoingConnectionPoint;
        OutgoingBlockSymbol = outgoingConnectionPoint?.BlockSymbolVM;
        drawnLineSymbolModel.OutgoingPosition = outgoingConnectionPoint.Position;
        drawnLineSymbolModel.CreateFirstLine(startXCoordinate, startYCoordinate);

        var firstLine = new LineSymbolVM(this)
        {
            X1 = startXCoordinate,
            Y1 = startYCoordinate,
            X2 = startXCoordinate,
            Y2 = startYCoordinate,
        };

        LinesSymbolVM.Add(firstLine);
    }

    public void DrawnLine(double xCoordinate, double yCoordinate)
    {
        if (OutgoingConnectionPoint is null)
        {
            return;
        }

        var linesModel = drawnLineSymbolModel.Redraw(new CoordinateDecorator(xCoordinate, yCoordinate));
        RedrawLines(linesModel);
    }

    private void RedrawLines(List<LineModel> linesModel)
    {
        LinesSymbolVM.Clear();

        foreach (var lineModel in linesModel)
        {
            var lineVM = factoryLineVM.CreateLineByModel(this, lineModel);
            LinesSymbolVM.Add(lineVM);
        }

        ArrowLineVM.Redraw();
    }

    public void FinishDrawing(ConnectionPointVM incommingConnectionPoint, double xCoordinateDranLine, double yCoordinateDranLine)
    {
        IncommingConnectionPoint = incommingConnectionPoint;
        IncommingBlockSymbol = incommingConnectionPoint.BlockSymbolVM;
        var linesModel = drawnLineSymbolModel.FinishDrawing(incommingConnectionPoint.Position, xCoordinateDranLine, yCoordinateDranLine);
        RedrawLines(linesModel);

        SetMovableRectangle();
        ArrowLineVM.Redraw();
    }

    private void SetMovableRectangle()
    {
        for (int i = 1; i < LinesSymbolVM.Count - 1; i++)
        {
            var lineSymbolVM = LinesSymbolVM[i];
            var movableRectangleLineVM = new MovableRectangleLineVM(lineSymbolVM);
            MovableRectanglesLineVM.Add(movableRectangleLineVM);
        }
    }

    public void Select()
    {
        CanvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.SelectedDrawnLinesVM.Add(this);

        IsSelect = true;
    }

    public void Unselect()
    {
        IsSelect = false;
        SetStroke(Brushes.Black);

        foreach (var movableRectangleLineVM in MovableRectanglesLineVM)
        {
            movableRectangleLineVM.IsShow = false;
        }
    }

    private void SetStroke(Brush brushLine)
    {
        foreach (var linesSymbolVM in LinesSymbolVM)
        {
            linesSymbolVM.Stroke = brushLine;
        }
    }
}