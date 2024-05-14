using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using Edblock.SymbolsViewModel.Symbols.LineSymbols;

namespace Edblock.ComponentsViewModel;

public class CanvasSymbolsViewModel
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

    private double width;
    public double Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }

    private double height;
    public double Height
    {
        get => height;
        set
        {
            height = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand MouseMove { get; init; }
    public DelegateCommand MouseUp { get; init; }
    public DelegateCommand MouseLeftButtonDown { get; init; }

    public ObservableCollection<BlockSymbolViewModel> BlockSymbolsVM { get; init; } = [];
    public ObservableCollection<DrawnLineSymbolVM> DrawnLinesSymbolVM { get; init; } = [];

    public Dictionary<BlockSymbolViewModel, List<DrawnLineSymbolVM>> BlockByDrawnLines { get; init; } = [];

    public BlockSymbolViewModel? MovableBlockSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }

    public DrawnLineSymbolVM? CurrentDrawnLineSymbol { get; set; }
    public DrawnLineSymbolVM? SelectedDrawnLineSymbol { get; set; }
    public List<BlockSymbolViewModel> SelectedBlockSymbols { get; set; } = [];
    public List<DrawnLineSymbolVM>? DrawnLines { get; set; } = [];
    public MovableRectangleLine? MovableRectangleLine { get; set; }

    public ScalingCanvasSymbolsViewModel ScalingCanvasSymbolsViewModel { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private const int lengthGridCell = 20;

    public CanvasSymbolsViewModel()
    {
        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValue);
        MouseLeftButtonDown = new(AddLine);

        ScalingCanvasSymbolsViewModel = new(this);

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

            BlockSymbolsVM.Remove(symbol);
        }

        ScalingCanvasSymbolsViewModel.Resize();
    }

    private void DeleteCurrentDrawnLineSymbol()
    {
        if (CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (CurrentDrawnLineSymbol.OutgoingConnectionPoint is null)
        {
            return;
        }

        DrawnLinesSymbolVM.Remove(CurrentDrawnLineSymbol);

        var outgoingConnectionPoint = CurrentDrawnLineSymbol.OutgoingConnectionPoint;
        outgoingConnectionPoint.IsHasConnectingLine = false;

        CurrentDrawnLineSymbol = null;
    }

    private void DeleteSelectedDrawnLineSymbol()
    {
        if (SelectedDrawnLineSymbol is null)
        {
            return;
        }

        var symbolOutgoingLine = SelectedDrawnLineSymbol.SymbolOutgoingLine;
        var symbolIncomingLine = SelectedDrawnLineSymbol.SymbolIncomingLine;

        if (symbolOutgoingLine is not null)
        {
            BlockByDrawnLines[symbolOutgoingLine].Remove(SelectedDrawnLineSymbol);
        }

        if (symbolIncomingLine is not null)
        {
            BlockByDrawnLines[symbolIncomingLine].Remove(SelectedDrawnLineSymbol);
        }

        var outgoingConnectionPoint = SelectedDrawnLineSymbol.OutgoingConnectionPoint;
        var incomingConnectionPoint = SelectedDrawnLineSymbol.IncomingConnectionPoint;

        if (outgoingConnectionPoint is not null)
        {
            outgoingConnectionPoint.IsHasConnectingLine = false;
        }

        if (incomingConnectionPoint is not null)
        {
            incomingConnectionPoint.IsHasConnectingLine = false;
        }

        DrawnLinesSymbolVM.Remove(SelectedDrawnLineSymbol);

        SelectedDrawnLineSymbol = null;
    }

    public void AddBlockSymbol(BlockSymbolViewModel blockSymbolVM)
    {
        RemoveSelectDrawnLine();
        ClearSelectedBlockSymbols();

        blockSymbolVM.MoveMiddle = true;
        MovableBlockSymbol = blockSymbolVM;
        MovableBlockSymbol.Select();

        BlockSymbolsVM.Add(blockSymbolVM);
    }

    //TODO: Подумать над названием метода
    private void AddLine()
    {
        CurrentDrawnLineSymbol?.AddLine();
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

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.MoveMiddle = false;
        }

        MovableBlockSymbol = null;

        MovableRectangleLine = null;
        ScalePartBlockSymbol = null;

        previousXCoordinate = 0;
        previousYCoordinate = 0;

        TextFieldSymbolVM.ChangeFocus(BlockSymbolsVM);
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

    public List<DrawnLineSymbolVM>? GetCurrentRedrawLines(BlockSymbolViewModel blockSymbolVM)
    {
        if (BlockByDrawnLines.ContainsKey(blockSymbolVM))
        {
            return BlockByDrawnLines[blockSymbolVM];
        }

        return null;
    }

    public void RedrawSymbols()
    {
        var currentCoordinate = (xCoordinate, yCoordinate);
        var previousCoordinate = (previousXCoordinate, previousYCoordinate);

        RedrawDrawnLinesSymbol(MovableBlockSymbol);

        CurrentDrawnLineSymbol?.ChangeCoordination(currentCoordinate);
        MovableRectangleLine?.ChangeCoordinateLine(currentCoordinate);
        MovableBlockSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
        ScalePartBlockSymbol?.SetWidthBlockSymbol(this);
        ScalePartBlockSymbol?.SetHeightBlockSymbol(this);

        previousXCoordinate = xCoordinate;
        previousYCoordinate = yCoordinate;
    }


    public void RedrawDrawnLinesSymbol(BlockSymbolViewModel? blockSymbolVM)
    {
        if (blockSymbolVM is null || !BlockByDrawnLines.ContainsKey(blockSymbolVM))
        {
            return;
        }

        var drawnLines = BlockByDrawnLines[blockSymbolVM];

        foreach (var drawnLine in drawnLines)
        {
            drawnLine.Redraw();
        }
    }

    public void RedrawnAllDrawnLines()
    {
        foreach (var blockByDrawnLine in BlockByDrawnLines)
        {
            var drawnLines = BlockByDrawnLines[blockByDrawnLine.Key];

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
