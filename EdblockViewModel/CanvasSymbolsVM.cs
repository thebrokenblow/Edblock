using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using EdblockViewModel.Symbols;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockModel.Symbols.LineSymbols.RedrawLine;

namespace EdblockViewModel;

public class CanvasSymbolsVM : INotifyPropertyChanged
{
    public Rect Grid { get; init; }

    private int xCoordinate;
    private int previousXCoordinate;
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = RoundCoordinate(value);

            var currentCoordinate = (xCoordinate, yCoordinate);
            var previousCoordinate = (previousXCoordinate, previousYCoordinate);

            MovableSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
            DrawnLineSymbol?.ChangeCoordination(currentCoordinate);
            ScalePartBlockSymbolVM?.SetWidthBlockSymbol(this);

            previousXCoordinate = xCoordinate;
        }
    }

    private int yCoordinate;
    private int previousYCoordinate;
    public int YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = RoundCoordinate(value);

            var currentCoordinate = (xCoordinate, yCoordinate);
            var previousCoordinate = (previousXCoordinate, previousYCoordinate);

            MovableSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
            DrawnLineSymbol?.ChangeCoordination(currentCoordinate);
            ScalePartBlockSymbolVM?.SetHeightBlockSymbol(this);

            previousYCoordinate = yCoordinate;
        }
    }

    private Cursor cursor = Cursors.Arrow;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<SymbolVM> Symbols { get; init; }
    public Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM?>> BlockSymbolByLineSymbol { get; init; }
    public DelegateCommand MouseMoveCanvasSymbols { get; init; }
    public DelegateCommand MouseUpCanvasSymbols { get; init; }
    public DelegateCommand ClickCanvasSymbols { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }
    public DelegateCommand<BlockSymbolVM> MouseMoveSymbol { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public BlockSymbolVM? MovableSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbolVM { get; set; }
    public DrawnLineSymbolVM? DrawnLineSymbol { get; set; }
    private List<DrawnLineSymbolVM?>? CurrentRedrawLines { get; set; }
    public DrawnLineSymbolVM? SelectDrawnLineSymbol { get; set; }

    private readonly FactoryBlockSymbol factoryBlockSymbol;
    private RedrawnLine? redrawLineSymbolVM;
    private const int lengthGridCell = 20;
    public CanvasSymbolsVM()
    {
        Symbols = new();
        MouseMoveCanvasSymbols = new(RedrawLine);
        BlockSymbolByLineSymbol = new();
        MouseUpCanvasSymbols = new(FinishRedrawingLine);
        ClickSymbol = new(CreateBlockSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        ClickCanvasSymbols = new(ClickOnCanvas);
        factoryBlockSymbol = new(this);
        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);
    }

    public void DeleteCurrentLine()
    {
        if (DrawnLineSymbol != null)
        {
            Symbols.Remove(DrawnLineSymbol);
            DrawnLineSymbol = null;
        }
    }

    public void CreateBlockSymbol(string nameBlockSymbol)
    {
        var currentSymbol = factoryBlockSymbol.Create(nameBlockSymbol);

        currentSymbol.TextField.Cursor = Cursors.SizeAll;
        Cursor = Cursors.SizeAll;

        MovableSymbol = currentSymbol;
        Symbols.Add(currentSymbol);
    }

    public void MoveSymbol(BlockSymbolVM currentSymbol)
    {
        if (!currentSymbol.TextField.Focus)
        {
            ConnectionPoint.SetDisplayConnectionPoints(currentSymbol.ConnectionPoints, false);
            ScaleRectangle.SetStateDisplay(currentSymbol.ScaleRectangles, false);

            currentSymbol.TextField.Cursor = Cursors.SizeAll;
            Cursor = Cursors.SizeAll;
        }

        MovableSymbol = currentSymbol;
        SetCurrentRedrawLines(currentSymbol);
        RedrawLine();
    }

    public void FinishMovingBlockSymbol()
    {
        MovableSymbol = null;
        ScalePartBlockSymbolVM = null;
        
        Cursor = Cursors.Arrow;
    }

    public void ClickOnCanvas()
    {
        AddLineSymbol();
        TextField.ChangeFocus(Symbols);
    }

    private void AddLineSymbol()
    {
        if (DrawnLineSymbol == null)
        {
            return;
        }

        var linesSymbol = DrawnLineSymbol.LinesSymbol;

        if (linesSymbol.Count > 1)
        {
            var drawnLineSymbolModel = DrawnLineSymbol.DrawnLineSymbolModel;
            var currentLineSymbolModel = drawnLineSymbolModel.GetNewLine();
            var currentLineSymbolVM = FactoryLineSymbol.CreateLine(currentLineSymbolModel);

            DrawnLineSymbol.LinesSymbol.Add(currentLineSymbolVM);
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public static int RoundCoordinate(int coordinate) //Округление координат, чтобы символ перемещался по сетке
    {
        int roundedCoordinate = coordinate - coordinate % (lengthGridCell / 2);
        return roundedCoordinate;
    }

    public void SetCurrentRedrawLines(BlockSymbolVM blockSymbolVM)
    {
        if (BlockSymbolByLineSymbol.ContainsKey(blockSymbolVM))
        {
            CurrentRedrawLines = BlockSymbolByLineSymbol[blockSymbolVM];
        }
    }

    private void FinishRedrawingLine()
    {
        CurrentRedrawLines = null;
    }

    private void RedrawLine()
    {
        if (CurrentRedrawLines == null)
        {
            return;
        }

        foreach (var currentRedrawLine in CurrentRedrawLines)
        {
            if (currentRedrawLine is not null)
            {
                var drawnLineSymbolModel = currentRedrawLine.DrawnLineSymbolModel;
                redrawLineSymbolVM = new (drawnLineSymbolModel);
                var redrawnLinesModel = redrawLineSymbolVM.GetRedrawLine();
                drawnLineSymbolModel.LinesSymbolModel = redrawnLinesModel;
                currentRedrawLine.RedrawAllLines();
            }
        }
    }
}