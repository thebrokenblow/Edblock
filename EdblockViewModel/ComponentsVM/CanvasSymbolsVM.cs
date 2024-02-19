using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols;

namespace EdblockViewModel.ComponentsVM;

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

    public ObservableCollection<BlockSymbolVM> BlockSymbolVM { get; init; }
    public ObservableCollection<DrawnLineSymbolVM> DrawnLinesSymbolVM { get; init; }

    public Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM>> BlockByDrawnLines { get; init; }

    public BlockSymbolVM? MovableBlockSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }

    public DrawnLineSymbolVM? СurrentDrawnLineSymbol { get; set; }
    public DrawnLineSymbolVM? SelectedDrawnLineSymbol { get; set; }
    public List<BlockSymbolVM> SelectedBlockSymbols { get; set; }
    private List<DrawnLineSymbolVM>? DrawnLines { get; set; }
    public MovableRectangleLine? MovableRectangleLine { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private const int lengthGridCell = 20;

    public CanvasSymbolsVM()
    {
        BlockSymbolVM = new();
        BlockByDrawnLines = new();
        DrawnLinesSymbolVM = new();
        SelectedBlockSymbols = new();
        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValue);
        MouseLeftButtonDown = new(AddLine);

        cursor = Cursors.Arrow;
        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);
    }

    public void DeleteSymbols()
    {
        MovableBlockSymbol = null;
        DeleteCurrentDrawnLineSymbol();
        DeleteSelectedDrawnLineSymbol();

        foreach (var symbol in SelectedBlockSymbols)
        {
            if (BlockByDrawnLines.ContainsKey(symbol))
            {
                var lines = BlockByDrawnLines[symbol];

                foreach (var line in lines)
                {
                    var symbolOut = line.SymbolOutgoingLine;
                    var symbolInc = line.SymbolIncomingLine;

                    if (symbol == symbolOut)
                    {
                        BlockByDrawnLines[symbolInc].Remove(line);
                    }
                    else
                    {
                        BlockByDrawnLines[symbolOut].Remove(line);
                    }

                    var outgoingConnectionPoint = line.OutgoingConnectionPoint;
                    var incomingConnectionPoint = line.IncomingConnectionPoint;

                    if (outgoingConnectionPoint is not null && incomingConnectionPoint is not null)
                    {
                        outgoingConnectionPoint.IsHasConnectingLine = false;
                        incomingConnectionPoint.IsHasConnectingLine = false;
                    }

                    DrawnLinesSymbolVM.Remove(line);
                }
                BlockByDrawnLines.Remove(symbol);
            }

            BlockSymbolVM.Remove(symbol);
        }
    }

    private void DeleteCurrentDrawnLineSymbol()
    {
        if (СurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (СurrentDrawnLineSymbol.OutgoingConnectionPoint is null)
        {
            return;
        }

        DrawnLinesSymbolVM.Remove(СurrentDrawnLineSymbol);

        var outgoingConnectionPoint = СurrentDrawnLineSymbol.OutgoingConnectionPoint;
        outgoingConnectionPoint.IsHasConnectingLine = false;

        СurrentDrawnLineSymbol = null;
    }

    private void DeleteSelectedDrawnLineSymbol()
    {
        if (SelectedDrawnLineSymbol is null)
        {
            return;
        }

        var symbolOutgoingLine = SelectedDrawnLineSymbol.SymbolOutgoingLine;
        var symbolIncomingLine = SelectedDrawnLineSymbol.SymbolIncomingLine;

        if (symbolOutgoingLine is not null && symbolIncomingLine is not null)
        {
            BlockByDrawnLines[symbolOutgoingLine].Remove(SelectedDrawnLineSymbol);
            BlockByDrawnLines[symbolIncomingLine].Remove(SelectedDrawnLineSymbol);
        }

        var outgoingConnectionPoint = SelectedDrawnLineSymbol.OutgoingConnectionPoint;
        var incomingConnectionPoint = SelectedDrawnLineSymbol.IncomingConnectionPoint;

        if (outgoingConnectionPoint is not null && incomingConnectionPoint is not null)
        {
            outgoingConnectionPoint.IsHasConnectingLine = false;
            incomingConnectionPoint.IsHasConnectingLine = false;
        }

        DrawnLinesSymbolVM.Remove(SelectedDrawnLineSymbol);

        SelectedDrawnLineSymbol = null;
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        RemoveSelectDrawnLine();
        ClearSelectedBlockSymbols();

        MovableBlockSymbol = blockSymbolVM;
        MovableBlockSymbol.Select();

        BlockSymbolVM.Add(blockSymbolVM);
    }

    //TODO: Подумать над названием метода
    private void AddLine()
    {
        СurrentDrawnLineSymbol?.AddLine();
        ClearSelectedBlockSymbols();
        RemoveSelectDrawnLine();
    }

    public void RemoveSelectDrawnLine()
    {
        if (SelectedDrawnLineSymbol != null && MovableRectangleLine == null)
        {
            var copySelectDrawnLineSymbol = SelectedDrawnLineSymbol;
            SelectedDrawnLineSymbol = null;
            copySelectDrawnLineSymbol.SetDefaultColorLines();
        }
    }

    private void SetDefaultValue()
    {
        Cursor = Cursors.Arrow;

        DrawnLines = null;

        MovableBlockSymbol = null;

        MovableRectangleLine = null;
        ScalePartBlockSymbol = null;

        previousXCoordinate = 0;
        previousYCoordinate = 0;

        TextFieldSymbolVM.ChangeFocus(BlockSymbolVM);
    }

    private void ClearSelectedBlockSymbols()
    {
        foreach (var selectedBlockSymbols in SelectedBlockSymbols)
        {
            if (selectedBlockSymbols != MovableBlockSymbol)
            {
                selectedBlockSymbols.IsSelected = false;
            }
        }

        SelectedBlockSymbols.RemoveAll(x => x != MovableBlockSymbol);
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
            DrawnLines = BlockByDrawnLines[blockSymbolVM];
        }
    }

    private void RedrawSymbols()
    {
        var currentCoordinate = (xCoordinate, yCoordinate);
        var previousCoordinate = (previousXCoordinate, previousYCoordinate);

        MovableBlockSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
        СurrentDrawnLineSymbol?.ChangeCoordination(currentCoordinate);
        MovableRectangleLine?.ChangeCoordinateLine(currentCoordinate);

        previousXCoordinate = xCoordinate;
        previousYCoordinate = yCoordinate;

        if (DrawnLines is not null)
        {
            foreach (var redrawDrawnLine in DrawnLines)
            {
                redrawDrawnLine.Redraw();
            }
        }
    }

    public void RedrawnAllDrawnLines()
    {
        foreach (var item in BlockByDrawnLines)
        {
            var drawnLines = BlockByDrawnLines[item.Key];

            foreach (var redrawDrawnLine in drawnLines)
            {
                redrawDrawnLine.Redraw();
            }
        }
    }

    public void RemoveSelectedSymbol()
    {
        foreach (var selectedBlockSymbol in SelectedBlockSymbols)
        {
            selectedBlockSymbol.IsSelected = false;
        }

        SelectedDrawnLineSymbol = null;
    }
}