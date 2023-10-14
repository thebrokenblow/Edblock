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

            if (CurrentLines != null)
            {
                CurrentLines.ChangeCoordination(x, y);
            }

            if (ScaleData != null)
            {
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

            if (CurrentLines != null)
            {
                CurrentLines.ChangeCoordination(x, y);
            }

            if (ScaleData != null)
            {
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
    public DelegateCommand<string> ClickSymbol { get; init; }
    public DelegateCommand<BlockSymbol> MouseMoveSymbol { get; init; }
    public DelegateCommand ClickCanvasSymbols { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public BlockSymbol? DraggableSymbol { get; set; }
    public ScaleData? ScaleData { get; set; }
    public DrawnLineSymbolVM? CurrentLines { get; set; }
    private readonly FactoryBlockSymbol factoryBlockSymbol;

    public CanvasSymbolsVM()
    {
        Symbols = new();
        ClickSymbol = new(CreateSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        ClickCanvasSymbols = new(ClickCanvas);
        factoryBlockSymbol = new(this);
        var lengthGrid = CanvasSymbols.LengthGrid;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }

    public void DeleteCurrentLine()
    {
        if (CurrentLines != null)
        {
            Symbols.Remove(CurrentLines);
            CurrentLines = null;
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
            ConnectionPoint.SetFill(ConnectionPointModel.HexNotFocusFill, currentSymbol.ConnectionPoints);
            ScaleRectangle.SetColor(ScaleRectangleModel.HexNotFocusFill, ScaleRectangleModel.HexNotFocusBorderBrush, currentSymbol.ScaleRectangles);
            currentSymbol.TextField.Cursor = Cursors.SizeAll;
            Cursor = Cursors.SizeAll;
        }

        DraggableSymbol = currentSymbol;
    }

    public void RemoveSymbol()
    {
        DraggableSymbol = null;
        ScaleData = null;
        
        Cursor = Cursors.Arrow;
    }

    public void ClickCanvas()
    {
        if (CurrentLines != null && CurrentLines?.LineSymbols.Count > 1)
        {
            var newLineSymbolModel = CurrentLines.DrawnLineSymbolModel.GetNewLine();
            var newLineSymbol = FactoryLineSymbol.CreateNewLine(newLineSymbolModel);

            CurrentLines.LineSymbols.Add(newLineSymbol);
        }
        TextField.ChangeFocus(Symbols);
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}