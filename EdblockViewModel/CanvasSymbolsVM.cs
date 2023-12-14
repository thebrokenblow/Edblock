using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using SerializationEdblock;
using System.ComponentModel;
using EdblockViewModel.Symbols;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

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

    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly FactoryBlockSymbolVM factoryBlockSymbol;
    private const int lengthGridCell = 20;
    public CanvasSymbolsVM()
    {
        SymbolsVM = new();
        BlockByDrawnLines = new();
        factoryBlockSymbol = new(this);

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
            SymbolsVM.Remove(DrawnLineSymbol);

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

            SymbolsVM.Remove(SelectDrawnLineSymbol);
            SelectDrawnLineSymbol = null;
        }
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        MovableBlockSymbol = blockSymbolVM;

        SymbolsVM.Add(blockSymbolVM);
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

        if (RedrawDrawnLines != null)
        {
            foreach (var redrawLine in RedrawDrawnLines)
            {
                redrawLine.Redraw();
            }
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

    private FactoryConnectionPoints factoryConnectionPoints;

    public async void LoadProject(string filePath)
    {
        var loadedProject = await SerializationProject.Read(filePath);
        var blockSymbolsVMById = new Dictionary<string, BlockSymbolVM>();

        SymbolsVM.Clear();
        BlockByDrawnLines.Clear();

        foreach (var blockSymbolSerializable in loadedProject.BlocksSymbolSerializable)
        {
            var blockSymbolVM = factoryBlockSymbol.CreateBySerialization(blockSymbolSerializable);
            blockSymbolsVMById.Add(blockSymbolSerializable.Id, blockSymbolVM);
            SymbolsVM.Add(blockSymbolVM);
        }

        foreach (var drawnLinesSymbolSerializable in loadedProject.DrawnLinesSymbolSerializable)
        {
            var symbolOutgoingLine = drawnLinesSymbolSerializable.SymbolOutgoingLine;
            var symbolIncomingLine = drawnLinesSymbolSerializable.SymbolIncomingLine;

            var symbolOutgoingLineVM = blockSymbolsVMById[symbolOutgoingLine.Id];
            var symbolIncomingLineVM = blockSymbolsVMById[symbolIncomingLine.Id];

            var linesSymbolModel = new List<LineSymbolModel>();

            foreach (var lineSymbolSerializable in drawnLinesSymbolSerializable.LinesSymbolSerializable)
            {
                var lineSymbolModel = new LineSymbolModel
                {
                    X1 = lineSymbolSerializable.X1,
                    Y1 = lineSymbolSerializable.Y1,
                    X2 = lineSymbolSerializable.X2,
                    Y2 = lineSymbolSerializable.Y2
                };

                linesSymbolModel.Add(lineSymbolModel);
            }

            var drawnLineSymbolModel = new DrawnLineSymbolModel(
                linesSymbolModel,
                symbolOutgoingLineVM.BlockSymbolModel,
                symbolIncomingLineVM.BlockSymbolModel,
                drawnLinesSymbolSerializable.OutgoingPosition,
                drawnLinesSymbolSerializable.IncomingPosition,
                drawnLinesSymbolSerializable.Color);

            factoryConnectionPoints = new(this, symbolOutgoingLineVM);

            var drawnLineSymbolVM = new DrawnLineSymbolVM(drawnLineSymbolModel, symbolOutgoingLineVM, symbolIncomingLineVM, this)
            {
                Text = drawnLinesSymbolSerializable.Text,
                OutgoingPosition = drawnLinesSymbolSerializable.OutgoingPosition,
                IncomingPosition = drawnLinesSymbolSerializable.IncomingPosition,
                OutgoingConnectionPoint = factoryConnectionPoints.CreateConnectionPoint(drawnLinesSymbolSerializable.OutgoingPosition),
                IncomingConnectionPoint = factoryConnectionPoints.CreateConnectionPoint(drawnLinesSymbolSerializable.IncomingPosition),
            };

            drawnLineSymbolVM.RedrawAllLines();

            if (!BlockByDrawnLines.ContainsKey(symbolOutgoingLineVM))
            {
                var drawnsLineSymbolVM = new List<DrawnLineSymbolVM>
                {
                    drawnLineSymbolVM
                };

                BlockByDrawnLines.Add(symbolOutgoingLineVM, drawnsLineSymbolVM);
            }
            else
            {
                BlockByDrawnLines[symbolOutgoingLineVM].Add(drawnLineSymbolVM);
            }

            if (!BlockByDrawnLines.ContainsKey(symbolIncomingLineVM))
            {
                var drawnsLineSymbolVM = new List<DrawnLineSymbolVM>
                {
                    drawnLineSymbolVM
                };

                BlockByDrawnLines.Add(symbolIncomingLineVM, drawnsLineSymbolVM);
            }
            else
            {
                BlockByDrawnLines[symbolIncomingLineVM].Add(drawnLineSymbolVM);
            }

            SymbolsVM.Add(drawnLineSymbolVM);
        }
    }
}