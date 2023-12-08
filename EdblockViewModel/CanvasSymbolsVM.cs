using System;
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
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockModel.Symbols.LineSymbols.RedrawLine;

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

            var currentCoordinate = (xCoordinate, yCoordinate);
            var previousCoordinate = (previousXCoordinate, previousYCoordinate);

            MovableSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
            DrawnLineSymbol?.ChangeCoordination(currentCoordinate);
            ScalePartBlockSymbolVM?.SetWidthBlockSymbol(this);

            previousXCoordinate = xCoordinate;
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

            var currentCoordinate = (xCoordinate, yCoordinate);
            var previousCoordinate = (previousXCoordinate, previousYCoordinate);

            MovableRectangleLine?.ChangeCoordinateLine(currentCoordinate);
            MovableSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
            DrawnLineSymbol?.ChangeCoordination(currentCoordinate);
            ScalePartBlockSymbolVM?.SetHeightBlockSymbol(this);

            previousYCoordinate = yCoordinate;
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

    public ObservableCollection<SymbolVM> Symbols { get; init; }
    public Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM?>> BlockSymbolByLineSymbol { get; init; }
    public DelegateCommand MouseMoveCanvasSymbols { get; init; }
    public DelegateCommand MouseUpCanvasSymbols { get; init; }
    public DelegateCommand ClickCanvasSymbols { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }
    public DelegateCommand<BlockSymbolVM> MouseMoveSymbol { get; init; }
    public BlockSymbolVM? MovableSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbolVM { get; set; }
    public DrawnLineSymbolVM? DrawnLineSymbol { get; set; }
    private List<DrawnLineSymbolVM?>? CurrentRedrawLines { get; set; }
    public DrawnLineSymbolVM? SelectDrawnLineSymbol { get; set; }
    public MovableRectangleLine? MovableRectangleLine { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    public SerializableSymbols SerializableSymbols { get; set; }

    private readonly FactoryBlockSymbol factoryBlockSymbol;
    private RedrawnLine? redrawLineSymbolVM;
    private const int lengthGridCell = 20;
    public CanvasSymbolsVM()
    {
        Symbols = new();
        SerializableSymbols = new();
        MouseMoveCanvasSymbols = new(RedrawLine);
        BlockSymbolByLineSymbol = new();
        MouseUpCanvasSymbols = new(FinishRedrawingLine);
        ClickSymbol = new(CreateBlockSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        ClickCanvasSymbols = new(ClickOnCanvas);
        factoryBlockSymbol = new(this);
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

            if (symbolIncomingLine != null && symbolOutgoingLine != null)
            {
                BlockSymbolByLineSymbol[symbolIncomingLine].Remove(SelectDrawnLineSymbol);
                BlockSymbolByLineSymbol[symbolOutgoingLine].Remove(SelectDrawnLineSymbol);
            }

            SelectDrawnLineSymbol.OutgoingConnectionPoint.IsHasConnectingLine = false;
            SelectDrawnLineSymbol.IncomingConnectionPoint!.IsHasConnectingLine = false;

            Symbols.Remove(SelectDrawnLineSymbol);
            SelectDrawnLineSymbol = null;
        }
    }

    public void CreateBlockSymbol(string nameBlockSymbol)
    {
        var currentSymbol = factoryBlockSymbol.Create(nameBlockSymbol);

        currentSymbol.TextField.Cursor = Cursors.SizeAll;
        Cursor = Cursors.SizeAll;

        MovableSymbol = currentSymbol;
        SerializableSymbols.BlocksSymbolModel.Add(currentSymbol.BlockSymbolModel);
        Symbols.Add(currentSymbol);
    }

    public void MoveSymbol(BlockSymbolVM currentSymbol)
    {
        if (!currentSymbol.TextField.Focus)
        {
            ConnectionPoint.SetDisplayConnectionPoints(currentSymbol.ConnectionPoints, false);
            ScaleRectangle.SetStateDisplay(currentSymbol.ScaleRectangles, false);

            currentSymbol.TextField.Cursor = Cursors.SizeAll;
            Cursor = Cursors.SizeAll;
        }

        MovableSymbol = currentSymbol;
        SetCurrentRedrawLines(currentSymbol);
        RedrawLine();
    }

    public void FinishMovingBlockSymbol()
    {
        MovableSymbol = null;
        ScalePartBlockSymbolVM = null;
        
        Cursor = Cursors.Arrow;
    }

    public void ClickOnCanvas()
    {
        AddLineSymbol();
        RemoveSelectDrawnLine();
        MovableRectangleLine = null;
        TextField.ChangeFocus(Symbols);
    }

    private void RemoveSelectDrawnLine()
    {
        if (SelectDrawnLineSymbol != null)
        {
            var copySelectDrawnLineSymbol = SelectDrawnLineSymbol;
            SelectDrawnLineSymbol = null;
            copySelectDrawnLineSymbol.SetDefaultColorLines();
        }
    }

    private void AddLineSymbol()
    {
        if (DrawnLineSymbol == null)
        {
            return;
        }

        var linesSymbol = DrawnLineSymbol.LinesSymbolVM;

        if (linesSymbol.Count > 1)
        {
            var drawnLineSymbolModel = DrawnLineSymbol.DrawnLineSymbolModel;
            var currentLineSymbolModel = drawnLineSymbolModel.GetNewLine();
            var currentLineSymbolVM = FactoryLineSymbol.CreateLine(currentLineSymbolModel);

            DrawnLineSymbol.LinesSymbolVM.Add(currentLineSymbolVM);
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public static int RoundCoordinate(int coordinate) //Округление координат, чтобы символ перемещался по сетке
    {
        int roundedCoordinate = coordinate - coordinate % (lengthGridCell / 2);
        return roundedCoordinate;
    }

    public void SetCurrentRedrawLines(BlockSymbolVM blockSymbolVM)
    {
        if (BlockSymbolByLineSymbol.ContainsKey(blockSymbolVM))
        {
            CurrentRedrawLines = BlockSymbolByLineSymbol[blockSymbolVM];
        }
    }

    private void FinishRedrawingLine()
    {
        MovableRectangleLine = null;
        CurrentRedrawLines = null;
        Cursor = Cursors.Arrow;
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
                var drawnLineSymbolModel = currentRedrawLine.DrawnLineSymbolModel;
                redrawLineSymbolVM = new (drawnLineSymbolModel);
                var redrawnLinesModel = redrawLineSymbolVM.GetRedrawLine();
                drawnLineSymbolModel.LinesSymbolModel = redrawnLinesModel;
                currentRedrawLine.RedrawAllLines();
            }
        }
    }

    public void SaveProject()
    {
        SerializableSymbols.SaveProject();
    }

    public void UploadProject()
    {
        SerializableSymbols.UploadProject();

        //foreach(var symbolModel in symbolsModel)
        //{
        //    var symbolVM = factoryBlockSymbol.CreateByModel(symbolModel);

        //    symbolVM.XCoordinate = symbolModel.XCoordinate;
        //    symbolVM.YCoordinate = symbolModel.YCoordinate;

        //    symbolVM.Width = symbolModel.Width;
        //    symbolVM.Height = symbolModel.Height;

        //    Symbols.Add(symbolVM);
        //}
    }
}