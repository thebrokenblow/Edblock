using System;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using EdblockModel.Symbols.Enum;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ConnectionPoints;

public class ConnectionPoint : INotifyPropertyChanged
{
    private int xCoordinate;
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            OnPropertyChanged();
        }
    }

    private int yCoordinate;
    public int YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
            OnPropertyChanged();
        }
    }

    private bool isShow = false;
    public bool IsShow
    {
        get => isShow;
        set
        {
            isShow = value;
            OnPropertyChanged();
        }
    }

    private bool isSelect = false;
    public bool IsSelect
    {
        get => isSelect;
        set
        {
            isSelect = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public DelegateCommand ClickConnectionPoint { get; init; }
    public BlockSymbolVM BlockSymbolVM { get; init; }
    public Func<(int, int)> GetCoordinate { get; init; }
    public PositionConnectionPoint PositionConnectionPoint { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public ConnectionPoint(
        CanvasSymbolsVM canvasSymbolsVM, 
        BlockSymbolVM blockSymbolVM, 
        Func<(int, int)> getCoordinate,
        PositionConnectionPoint positionConnectionPoint)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        GetCoordinate = getCoordinate;

        PositionConnectionPoint = positionConnectionPoint;
        BlockSymbolVM = blockSymbolVM;
        
        EnterCursor = new(ShowConnectionPoints);
        LeaveCursor = new(HideConnectionPoints);
        ClickConnectionPoint = new(TrackStageDrawLine);

        (XCoordinate, YCoordinate) = getCoordinate.Invoke();
    }

    public void ChangeCoordination()
    {
        (XCoordinate, YCoordinate) = GetCoordinate.Invoke();
    }

    public void ShowConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Hand, true, true);
    }

    public void HideConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Arrow, false, false);
    }

    public void TrackStageDrawLine()
    {
        if (_canvasSymbolsVM.DrawnLineSymbol == null)
        {
            StarDrawLine();
        }
        else
        {
            EndDrawLine();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string nameProperty = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProperty));
    }

    public static void SetDisplayConnectionPoints(List<ConnectionPoint> connectionPoints, bool isShow)
    {
        foreach (var connectionPoint in connectionPoints)
        {
            connectionPoint.IsShow = isShow;
        }
    }

    private void SetDisplayConnectionPoint(Cursor cursorDisplaying, bool isEnterConnectionPoint, bool isSelectConnectionPoint)
    {
        if (_canvasSymbolsVM.ScalePartBlockSymbolVM == null) //Код выполняется, если символ не масштабируется
        {
            SetDisplayConnectionPoints(BlockSymbolVM.ConnectionPoints, isEnterConnectionPoint);

            _canvasSymbolsVM.Cursor = cursorDisplaying;
            IsSelect = isSelectConnectionPoint;
        }
    }

    private void StarDrawLine()
    {
        var positionConnectionPoint = PositionConnectionPoint;
        var blockSymbolModel = BlockSymbolVM.BlockSymbolModel;

        var drawnLineSymbolModel = new DrawnLineSymbolModel(blockSymbolModel, positionConnectionPoint);
        drawnLineSymbolModel.AddFirstLine();

        var drawnLineSymbolVM = new DrawnLineSymbolVM(positionConnectionPoint, drawnLineSymbolModel, _canvasSymbolsVM);

        _canvasSymbolsVM.Symbols.Add(drawnLineSymbolVM);
        _canvasSymbolsVM.DrawnLineSymbol = drawnLineSymbolVM;
        _canvasSymbolsVM.DrawnLineSymbol.SymbolOutgoingLine = BlockSymbolVM;
    }

    private void EndDrawLine()
    {
        var symbolIncomingLineModel = BlockSymbolVM.BlockSymbolModel;

        var drawnLineSymbolVM = _canvasSymbolsVM.DrawnLineSymbol;
        var drawnLineSymbolModel = drawnLineSymbolVM!.DrawnLineSymbolModel;

        drawnLineSymbolVM.IncomingPosition = PositionConnectionPoint;
        drawnLineSymbolModel.IncomingPosition = PositionConnectionPoint;
        
        drawnLineSymbolVM.SymbolIncomingLine = BlockSymbolVM;
        drawnLineSymbolModel.SymbolIncomingLine = symbolIncomingLineModel;

        var finalLineCoordinate = symbolIncomingLineModel.GetBorderCoordinate(PositionConnectionPoint);

        var completedLineModel = new CompletedLine(drawnLineSymbolModel, finalLineCoordinate);
        var completeLinesSymbolModel = completedLineModel.GetCompleteLines();

        drawnLineSymbolModel.LinesSymbolModel = completeLinesSymbolModel;

        drawnLineSymbolVM.RedrawAllLines();
        
        AddBlockToLine(drawnLineSymbolVM.SymbolIncomingLine);
        AddBlockToLine(drawnLineSymbolVM.SymbolOutgoingLine);

        _canvasSymbolsVM.DrawnLineSymbol = null;
    }

    private void AddBlockToLine(BlockSymbolVM? blockSymbol)
    {
        if (blockSymbol == null)
        {
            return;
        }

        if (_canvasSymbolsVM.BlockSymbolByLineSymbol.ContainsKey(blockSymbol))
        {
            var drawnLinesSymbolVM = _canvasSymbolsVM.BlockSymbolByLineSymbol[blockSymbol];
            drawnLinesSymbolVM.Add(_canvasSymbolsVM.DrawnLineSymbol);
        }
        else
        {
            var drawnLinesSymbolVM = new List<DrawnLineSymbolVM?>
            {
                _canvasSymbolsVM.DrawnLineSymbol
            };
            _canvasSymbolsVM.BlockSymbolByLineSymbol.Add(blockSymbol, drawnLinesSymbolVM);
        }
    }
}