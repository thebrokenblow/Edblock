using EdblockModel;
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
            ScalePartBlockSymbol?.SetWidthBlockSymbol(this);
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
            ScalePartBlockSymbol?.SetHeightBlockSymbol(this);
        }
    }

    private Cursor cursor;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand MouseMove { get; init; }
    public DelegateCommand MouseUp { get; init; }
    public DelegateCommand MouseLeftButtonDown { get; init; }

    public ObservableCollection<SymbolVM> Symbols { get; init; }
    public Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM>> BlockByDrawnLines { get; init; }

    public BlockSymbolVM? MovableBlockSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }

    public DrawnLineSymbolVM? DrawnLineSymbol { get; set; }
    public DrawnLineSymbolVM? SelectDrawnLineSymbol { get; set; }
    private List<DrawnLineSymbolVM>? RedrawDrawnLines { get; set; }
    public MovableRectangleLine? MovableRectangleLine { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public SerializableSymbols SerializableSymbols { get; set; }

    private const int lengthGridCell = 20;
    public CanvasSymbolsVM()
    {
        Symbols = new();
        BlockByDrawnLines = new();
        SerializableSymbols = new();

        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValue);
        MouseLeftButtonDown = new(AddLine);

        cursor = Cursors.Arrow;
        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);
    }

    public void DeleteLine()
    {
        if (DrawnLineSymbol != null)
        {
            Symbols.Remove(DrawnLineSymbol);

            DrawnLineSymbol.OutgoingConnectionPoint.IsHasConnectingLine = false;

            DrawnLineSymbol = null;
        }

        if (SelectDrawnLineSymbol != null)
        {
            var symbolIncomingLine = SelectDrawnLineSymbol.SymbolIncomingLine;
            var symbolOutgoingLine = SelectDrawnLineSymbol.SymbolOutgoingLine;

            if (symbolIncomingLine != null)
            {
                BlockByDrawnLines[symbolIncomingLine].Remove(SelectDrawnLineSymbol);
                BlockByDrawnLines[symbolOutgoingLine].Remove(SelectDrawnLineSymbol);
            }

            SelectDrawnLineSymbol.OutgoingConnectionPoint.IsHasConnectingLine = false;
            SelectDrawnLineSymbol.IncomingConnectionPoint!.IsHasConnectingLine = false;

            Symbols.Remove(SelectDrawnLineSymbol);
            SelectDrawnLineSymbol = null;
        }
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        MovableBlockSymbol = blockSymbolVM;

        Symbols.Add(blockSymbolVM);
        SerializableSymbols.BlocksSymbolModel.Add(blockSymbolVM.BlockSymbolModel);
    }

    //TODO: Подумать над названием метода
    public void AddLine()
    {
        DrawnLineSymbol?.AddLine();

        RemoveSelectDrawnLine();
    }

    private void RemoveSelectDrawnLine()
    {
        if (SelectDrawnLineSymbol != null)
        {
            //TODO: Плохой код
            var copySelectDrawnLineSymbol = SelectDrawnLineSymbol;
            SelectDrawnLineSymbol = null;
            copySelectDrawnLineSymbol.SetDefaultColorLines();
        }
    }

    private void SetDefaultValue()
    {
        Cursor = Cursors.Arrow;

        RedrawDrawnLines = null;
        MovableBlockSymbol = null;
        MovableRectangleLine = null;
        ScalePartBlockSymbol = null;

        TextField.ChangeFocus(Symbols);
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    private static int RoundCoordinate(int coordinate) //Округление координат, чтобы символ перемещался по сетке
    {
        int roundedCoordinate = coordinate - coordinate % (lengthGridCell / 2);

        return roundedCoordinate;
    }

    public void SetCurrentRedrawLines(BlockSymbolVM blockSymbolVM)
    {
        if (BlockByDrawnLines.ContainsKey(blockSymbolVM))
        {
            RedrawDrawnLines = BlockByDrawnLines[blockSymbolVM];
        }
    }

    private void RedrawSymbols()
    {
        var currentCoordinate = (xCoordinate, yCoordinate);
        var previousCoordinate = (previousXCoordinate, previousYCoordinate);

        MovableBlockSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
        DrawnLineSymbol?.ChangeCoordination(currentCoordinate);
        MovableRectangleLine?.ChangeCoordinateLine(currentCoordinate);

        previousXCoordinate = xCoordinate;
        previousYCoordinate = yCoordinate;

        if (RedrawDrawnLines != null)
        {
            foreach (var redrawLine in RedrawDrawnLines)
            {
                redrawLine.Redraw();
            }
        }
    }
}