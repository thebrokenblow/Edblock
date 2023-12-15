using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using SerializationEdblock;
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

    public ObservableCollection<SymbolVM> SymbolsVM { get; init; }
    public Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM>> BlockByDrawnLines { get; init; }

    public BlockSymbolVM? MovableBlockSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }

    public DrawnLineSymbolVM? DrawnLineSymbol { get; set; }
    public DrawnLineSymbolVM? SelectDrawnLineSymbol { get; set; }
    private List<DrawnLineSymbolVM>? RedrawDrawnLines { get; set; }
    public MovableRectangleLine? MovableRectangleLine { get; set; }
    private readonly ProjectVM projectVM;

    public event PropertyChangedEventHandler? PropertyChanged;

    private const int lengthGridCell = 20;
    public CanvasSymbolsVM()
    {
        SymbolsVM = new();
        BlockByDrawnLines = new();
        projectVM = new(this);
        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValue);
        MouseLeftButtonDown = new(AddLine);

        cursor = Cursors.Arrow;
        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);
    }

    public void DeleteLine()
    {
        DeleteDrawnLineSymbol();
        DeleteSelectDrawnLineSymbol();
    }

    private void DeleteDrawnLineSymbol()
    {
        if (DrawnLineSymbol is null)
        {
            return;
        }

        if (DrawnLineSymbol.OutgoingConnectionPoint is null)
        {
            return;
        }

        SymbolsVM.Remove(DrawnLineSymbol);

        var outgoingConnectionPoint = DrawnLineSymbol.OutgoingConnectionPoint;
        outgoingConnectionPoint.IsHasConnectingLine = false;

        DrawnLineSymbol = null;
    }

    private void DeleteSelectDrawnLineSymbol()
    {
        if (SelectDrawnLineSymbol is null)
        {
            return;
        }

        var symbolOutgoingLine = SelectDrawnLineSymbol.SymbolOutgoingLine;
        var symbolIncomingLine = SelectDrawnLineSymbol.SymbolIncomingLine;

        if (symbolOutgoingLine is not null && symbolIncomingLine is not null)
        {
            BlockByDrawnLines[symbolOutgoingLine].Remove(SelectDrawnLineSymbol);
            BlockByDrawnLines[symbolIncomingLine].Remove(SelectDrawnLineSymbol);
        }

        var outgoingConnectionPoint = SelectDrawnLineSymbol.OutgoingConnectionPoint;
        var incomingConnectionPoint = SelectDrawnLineSymbol.IncomingConnectionPoint;

        if (outgoingConnectionPoint is not null && incomingConnectionPoint is not null)
        {
            outgoingConnectionPoint.IsHasConnectingLine = false;
            incomingConnectionPoint.IsHasConnectingLine = false;
        }

        SymbolsVM.Remove(SelectDrawnLineSymbol);
        SelectDrawnLineSymbol = null;
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        MovableBlockSymbol = blockSymbolVM;

        SymbolsVM.Add(blockSymbolVM);
    }

    //TODO: Подумать над названием метода
    private void AddLine()
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

        TextField.ChangeFocus(SymbolsVM);
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

        if (RedrawDrawnLines == null)
        {
            return;
        }

        foreach (var redrawDrawnLine in RedrawDrawnLines)
        {
            redrawDrawnLine.Redraw();
        }
    }

    public void SaveProject(string filePath)
    {
        var blocksSymbolSerializable = new List<BlockSymbolSerializable>();
        var drawnLinesSymbolSerializable = new List<DrawnLineSymbolSerializable>();

        foreach (var symbol in SymbolsVM)
        {
            if (symbol is BlockSymbolVM blockSymbolVM)
            {
                var blockSymbolModel = blockSymbolVM.BlockSymbolModel;
                var blockSymbolSerializable = FactorySymbolSerializable.CreateBlockSymbolSerializable(blockSymbolModel);

                blocksSymbolSerializable.Add(blockSymbolSerializable);
            }

            if (symbol is DrawnLineSymbolVM drawnLineSymbolVM)
            {
                var drawnLineSymbolModel = drawnLineSymbolVM.DrawnLineSymbolModel;
                var drawnLineSymbolSerializable = FactorySymbolSerializable.CreateDrawnLineSymbolSerializable(drawnLineSymbolModel);

                drawnLinesSymbolSerializable.Add(drawnLineSymbolSerializable);
            }
        }

        var projectSerializable = new ProjectSerializable(blocksSymbolSerializable, drawnLinesSymbolSerializable);

        SerializationProject.Write(projectSerializable, filePath);
    }

    public async void LoadProject(string filePath)
    {
        var loadedProject = await SerializationProject.Read(filePath);

        projectVM.Load(loadedProject);
    }
}