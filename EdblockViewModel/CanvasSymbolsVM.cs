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
    public DelegateCommand MouseUpSymbol { get; init; }
    public DelegateCommand ClickCanvasSymbols { get; init; }
    public DelegateCommand ClickEsc { get; init; }
    public BlockSymbol? DraggableSymbol { get; set; }
    public ScaleData? ScaleData { get; set; }
    public ListLineSymbol? CurrentLines { get; set; }
    private readonly FactoryBlockSymbol factoryBlockSymbol;
    private readonly SerializableSymbols serializableSymbols = new();

    public CanvasSymbolsVM()
    {
        Symbols = new();
        ClickSymbol = new(CreateSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        MouseUpSymbol = new(RemoveSymbol);
        ClickCanvasSymbols = new(ClickCanvas);
        ClickEsc = new(DeleteLine);
        factoryBlockSymbol = new(this);
        var lengthGrid = CanvasSymbols.LengthGrid;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }

    private void DeleteLine()
    {
        if (CurrentLines != null)
        {
            Symbols.Remove(CurrentLines);
            CurrentLines = null;
        }
    }

    private void CreateSymbol(string nameBlockSymbol)
    {
        var currentSymbol = factoryBlockSymbol.Create(nameBlockSymbol);

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
        if (CurrentLines != null && CurrentLines.LineSymbols.Count > 1)
        {
            var line = CurrentLines.LineSymbols[^1];
            var positionConnectionPoint = CurrentLines.LineSymbolModel.LinesSymbols[^1].PositionConnectionPoint;

            var lineSymbolModel = FactoryLineSymbolModel.CreateLine(positionConnectionPoint);
            lineSymbolModel.X1 = line.X2;
            lineSymbolModel.Y1 = line.Y2;
            lineSymbolModel.X2 = line.X2;
            lineSymbolModel.Y2 = line.Y2;

            var lineSymbol = FactoryLineSymbol.CreateStartLine(lineSymbolModel);
            lineSymbol.X1 = line.X2;
            lineSymbol.Y1 = line.Y2;
            lineSymbol.X2 = line.X2;
            lineSymbol.Y2 = line.Y2;

            CurrentLines.LineSymbolModel.LinesSymbols.Add(lineSymbolModel);
            CurrentLines.LineSymbols.Add(lineSymbol);
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
       
        CurrentLines ??= new(lineSymbolModel);
        CurrentLines.LineSymbols.Add(lineSymbol);
        CurrentLines.SymbolOutgoingLine = blockSymbol;
        serializableSymbols.linesSymbolModel.Add(CurrentLines.LineSymbolModel);
        Symbols.Add(CurrentLines);
    }
}