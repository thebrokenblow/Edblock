using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using EdblockViewModel.Symbols;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.LineSymbols;
using System.Collections.Generic;

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

            DraggableSymbol?.SetCoordinate((xCoordinate, yCoordinate), (previousXCoordinate, previousYCoordinate));
            CurrentDrawnLineSymbol?.ChangeCoordination(xCoordinate, yCoordinate);

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

            DraggableSymbol?.SetCoordinate((xCoordinate, yCoordinate), (previousXCoordinate, previousYCoordinate));
            CurrentDrawnLineSymbol?.ChangeCoordination(xCoordinate, yCoordinate);

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
    public BlockSymbolVM? DraggableSymbol { get; set; }
    public ScalePartBlockSymbolVM? ScalePartBlockSymbolVM { get; set; }
    public DrawnLineSymbolVM? CurrentDrawnLineSymbol { get; set; }
    private List<DrawnLineSymbolVM?>? CurrentRedrawLines { get; set; }
    private readonly FactoryBlockSymbol factoryBlockSymbol;
    private RedrawLineSymbol? redrawLineSymbol;
    private const int lengthGridCell = 20;
    public CanvasSymbolsVM()
    {
        Symbols = new();
        MouseMoveCanvasSymbols = new(RedrawLine);
        BlockSymbolByLineSymbol = new();
        MouseUpCanvasSymbols = new(FinishRedrawingLine);
        ClickSymbol = new(CreateBlockSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        ClickCanvasSymbols = new(ClickCanvas);
        factoryBlockSymbol = new(this);
        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);
    }

    public void DeleteCurrentLine()
    {
        if (CurrentDrawnLineSymbol != null)
        {
            Symbols.Remove(CurrentDrawnLineSymbol);
            CurrentDrawnLineSymbol = null;
        }
    }

    public void CreateBlockSymbol(string nameBlockSymbol)
    {
        var currentSymbol = factoryBlockSymbol.Create(nameBlockSymbol);

        currentSymbol.TextField.Cursor = Cursors.SizeAll;
        Cursor = Cursors.SizeAll;

        DraggableSymbol = currentSymbol;
        Symbols.Add(currentSymbol);
    }

    public void MoveSymbol(BlockSymbolVM currentSymbol)
    {
        if (!currentSymbol.TextField.Focus)
        {
            ConnectionPoint.SetStateDisplay(currentSymbol.ConnectionPoints, false);
            ScaleRectangle.SetStateDisplay(currentSymbol.ScaleRectangles, false);

            currentSymbol.TextField.Cursor = Cursors.SizeAll;
            Cursor = Cursors.SizeAll;
        }

        DraggableSymbol = currentSymbol;
        SetCurrentRedrawLine(currentSymbol);
    }

    public void FinishMovingBlockSymbol()
    {
        DraggableSymbol = null;
        ScalePartBlockSymbolVM = null;

        previousXCoordinate = 0;
        previousYCoordinate = 0;
        
        Cursor = Cursors.Arrow;
    }

    public void ClickCanvas()
    {
        if (CurrentDrawnLineSymbol != null && CurrentDrawnLineSymbol?.LineSymbols.Count > 1)
        {
            var newLineSymbolModel = CurrentDrawnLineSymbol.DrawnLineSymbolModel.GetNewLine();
            var newLineSymbol = FactoryLineSymbol.CreateNewLine(newLineSymbolModel);

            CurrentDrawnLineSymbol.LineSymbols.Add(newLineSymbol);
        }
        TextField.ChangeFocus(Symbols);
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

    private void SetCurrentRedrawLine(BlockSymbolVM currentSymbol)
    {
        if (BlockSymbolByLineSymbol.ContainsKey(currentSymbol))
        {
            CurrentRedrawLines = BlockSymbolByLineSymbol[currentSymbol];
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
                redrawLineSymbol = new(currentRedrawLine);
                redrawLineSymbol.Redraw();
            }
        }
    }
}