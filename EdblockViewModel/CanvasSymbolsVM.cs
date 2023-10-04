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
using EdblockViewModel.Symbols.LineSymbols;

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
    public DelegateCommand ClickCanvasSymbols { get; init; }
    public DelegateCommand ClickEsc { get; init; }
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
        ClickCanvasSymbols = new(ClickCanvas);
        ClickEsc = new(DeleteLine);
        var lengthGrid = CanvasSymbols.LENGTH_GRID;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }

    private void DeleteLine()
    {
        if (currentLines != null)
        {
            Symbols.Remove(currentLines);
            currentLines = null;
        }
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

    public void ClickCanvas()
    {
        if (currentLines != null && currentLines.LineSymbols.Count > 1)
        {
            var line = currentLines.LineSymbols[^1];
            var orientation = currentLines.LineSymbolModel.LinesSymbols[^1].Orientation;

            var lineSymbolModel = FactoryLineSymbolModel.CreateLine(orientation);
            lineSymbolModel.X1 = line.X2;
            lineSymbolModel.Y1 = line.Y2;
            lineSymbolModel.X2 = line.X2;
            lineSymbolModel.Y2 = line.Y2;

            var lineSymbol = FactoryLineSymbol.CreateStartLine(lineSymbolModel);
            lineSymbol.X1 = line.X2;
            lineSymbol.Y1 = line.Y2;
            lineSymbol.X2 = line.X2;
            lineSymbol.Y2 = line.Y2;

            currentLines.LineSymbolModel.LinesSymbols.Add(lineSymbolModel);
            currentLines.LineSymbols.Add(lineSymbol);
        }
        TextField.ChangeFocus(Symbols);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    internal void DrawLine(LineSymbolModel lineSymbolModel, BlockSymbol blockSymbol)
    {
        var lineSymbol = FactoryLineSymbol.CreateStartLine(lineSymbolModel);
       
        currentLines ??= new(lineSymbolModel);
        currentLines.LineSymbols.Add(lineSymbol);
        currentLines.SymbolOutgoingLine = blockSymbol;
        serializableSymbols.linesSymbolModel.Add(currentLines.LineSymbolModel);
        Symbols.Add(currentLines);
    }
}