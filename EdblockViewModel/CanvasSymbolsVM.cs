using EdblockModel;
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
using EdblockModel.Symbols.ConnectionPoints;
using EdblockModel.Symbols.ScaleRectangles;
using System.Collections.Generic;

namespace EdblockViewModel;

public class CanvasSymbolsVM : INotifyPropertyChanged
{
    public Rect Grid { get; init; }

    private int x;
    public int X
    {
        get => x;
        set
        {
            CoordinateBlockSymbol.SetXCoordinate(DraggableSymbol, value, x);
            x = CanvasSymbols.СorrectionCoordinateSymbol(value);

            if (CurrentDrawnLineSymbol != null)
            {
                CurrentDrawnLineSymbol.ChangeCoordination(x, y);
            }

            if (ScaleData != null)
            {
                SetCurrentRedrawLine(ScaleData.BlockSymbol);
                RedrawLine();
                SizeBlockSymbol.SetSize(ScaleData, this, ScaleData?.GetWidthSymbol, ScaleData!.BlockSymbol.SetWidth);
                Cursor = ScaleData.Cursor;
                ScaleData.BlockSymbol.TextField.Cursor = ScaleData.Cursor;
            }
        }
    }

    private int y;
    public int Y
    {
        get => y;
        set
        {
            CoordinateBlockSymbol.SetYCoordinate(DraggableSymbol, value, y);
            y = CanvasSymbols.СorrectionCoordinateSymbol(value);

            if (CurrentDrawnLineSymbol != null)
            {
                CurrentDrawnLineSymbol.ChangeCoordination(x, y);
            }

            if (ScaleData != null)
            {
                SetCurrentRedrawLine(ScaleData.BlockSymbol);
                RedrawLine();
                SizeBlockSymbol.SetSize(ScaleData, this, ScaleData?.GetHeigthSymbol, ScaleData!.BlockSymbol.SetHeight);
                Cursor = ScaleData.Cursor;
                ScaleData.BlockSymbol.TextField.Cursor = ScaleData.Cursor;
            }
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
   
    public ObservableCollection<Symbol> Symbols { get; init; }
    public Dictionary<BlockSymbol, List<DrawnLineSymbolVM?>> BlockSymbolByLineSymbol { get; init; }
    public DelegateCommand MouseMoveCanvasSymbols { get; init; }
    public DelegateCommand MouseUpCanvasSymbols { get; init; }
    public DelegateCommand ClickCanvasSymbols { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }
    public DelegateCommand<BlockSymbol> MouseMoveSymbol { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public BlockSymbol? DraggableSymbol { get; set; }
    public ScaleData? ScaleData { get; set; }
    public DrawnLineSymbolVM? CurrentDrawnLineSymbol { get; set; }
    private List<DrawnLineSymbolVM?>? CurrentRedrawLines { get; set; }
    private readonly FactoryBlockSymbol factoryBlockSymbol;
    private RedrawLineSymbol? redrawLineSymbol;
    public CanvasSymbolsVM()
    {
        Symbols = new();
        MouseMoveCanvasSymbols = new(RedrawLine);
        BlockSymbolByLineSymbol = new();
        MouseUpCanvasSymbols = new(FinishRedrawingLine);
        ClickSymbol = new(CreateSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        ClickCanvasSymbols = new(ClickCanvas);
        factoryBlockSymbol = new(this);
        var lengthGrid = CanvasSymbols.LengthGrid;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }

    public void DeleteCurrentLine()
    {
        if (CurrentDrawnLineSymbol != null)
        {
            Symbols.Remove(CurrentDrawnLineSymbol);
            CurrentDrawnLineSymbol = null;
        }
    }

    public void CreateSymbol(string nameBlockSymbol)
    {
        var currentSymbol = factoryBlockSymbol.Create(nameBlockSymbol);

        currentSymbol.TextField.Cursor = Cursors.SizeAll;
        Cursor = Cursors.SizeAll;

        DraggableSymbol = currentSymbol;
        Symbols.Add(currentSymbol);
    }

    public void MoveSymbol(BlockSymbol currentSymbol)
    {
        if (!currentSymbol.TextField.Focus)
        {
            ConnectionPoint.SetEnterConnectionPoint(currentSymbol.ConnectionPoints, false);
            ScaleRectangle.SetColor(ScaleRectangleModel.HexNotFocusFill, ScaleRectangleModel.HexNotFocusBorderBrush, currentSymbol.ScaleRectangles);
            currentSymbol.TextField.Cursor = Cursors.SizeAll;
            Cursor = Cursors.SizeAll;
        }

        DraggableSymbol = currentSymbol;
        SetCurrentRedrawLine(currentSymbol);
    }
    public void RemoveSymbol()
    {
        DraggableSymbol = null;
        ScaleData = null;

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

    private void SetCurrentRedrawLine(BlockSymbol currentSymbol)
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