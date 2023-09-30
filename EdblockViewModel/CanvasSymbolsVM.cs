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
using EdblockModel.Symbols.LineSymbols;

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

            if (currentLines != null)
            {
                currentLines.ChangeCoordination(x, y);
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

            if (currentLines != null)
            {
                currentLines.ChangeCoordination(x, y);
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
    public DelegateCommand MouseUpSymbol { get; init; }
    public DelegateCommand FocusableRemove { get; init; }
    public BlockSymbol? DraggableSymbol { get; set; }
    public ScaleData? ScaleData { get; set; }
    private ListLineSymbol? currentLines;
    private readonly SerializableSymbols serializableSymbols = new();

    public CanvasSymbolsVM()
    {
        Symbols = new();
        ClickSymbol = new(CreateSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        MouseUpSymbol = new(RemoveSymbol);
        FocusableRemove = new(ChangeFocus);
        var lengthGrid = CanvasSymbols.LENGTH_GRID;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }

    private void CreateSymbol(string nameBlockSymbol)
    {
        var currentSymbol = FactoryBlockSymbol.Create(nameBlockSymbol, this);

        currentSymbol.TextField.Cursor = Cursors.SizeAll;
        Cursor = Cursors.SizeAll;

        DraggableSymbol = currentSymbol;
        serializableSymbols.blocksSymbolModel.Add(currentSymbol.BlockSymbolModel);
        Symbols.Add(currentSymbol);
    }

    private void MoveSymbol(BlockSymbol currentSymbol)
    {
        if (!currentSymbol.TextField.Focus)
        {
            ColorConnectionPoint.Hide(currentSymbol.ConnectionPoints);
            ColorScaleRectangle.Hide(currentSymbol.ScaleRectangles);
            currentSymbol.TextField.Cursor = Cursors.SizeAll;
            Cursor = Cursors.SizeAll;
        }

        DraggableSymbol = currentSymbol;
    }

    private void RemoveSymbol()
    {
        DraggableSymbol = null;
        ScaleData = null;
        
        Cursor = Cursors.Arrow;
    }

    public void ChangeFocus()
    {
        foreach (var symbol in Symbols)
        {
            if (symbol is BlockSymbol blockSymbol)
            {
                if (blockSymbol.TextField.Focus)
                {
                    blockSymbol.TextField.Focus = false;
                }
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    internal void DrawLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = FactoryLineSymbol.CreateStartLine(lineSymbolModel);

        currentLines ??= new(lineSymbolModel);
        currentLines.LineSymbols.Add(lineSymbol);
        serializableSymbols.linesSymbolModel.Add(currentLines.LineSymbolModel);
        Symbols.Add(currentLines);
    }
}