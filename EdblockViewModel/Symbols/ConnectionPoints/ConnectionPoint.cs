using System;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

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
    public DelegateCommand<ConnectionPoint> ClickConnectionPoint { get; init; }
    public BlockSymbolVM BlockSymbolVM { get; init; }
    public Func<(int, int)> GetCoordinate { get; init; }
    public PositionConnectionPoint PositionConnectionPoint { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private CompletedLineModel? completedLineModel;
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

    public void TrackStageDrawLine(ConnectionPoint connectionPoint)
    {
        if (_canvasSymbolsVM.CurrentDrawnLineSymbol == null)
        {
            StarDrawLine(connectionPoint);
        }
        else
        {
            EndDrawLine(connectionPoint);
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

    private void StarDrawLine(ConnectionPoint connectionPoint)
    {
        var coordinateConnectionPoint = (connectionPoint.XCoordinate, connectionPoint.YCoordinate);
        var positionConnectionPoint = connectionPoint.PositionConnectionPoint;
        var blockSymbolModel = connectionPoint.BlockSymbolVM.BlockSymbolModel;

        var drawnLineSymbolModel = new DrawnLineSymbolModel(connectionPoint.PositionConnectionPoint);
        drawnLineSymbolModel.AddFirstLine(coordinateConnectionPoint, positionConnectionPoint, blockSymbolModel);

        var drawnLineSymbolVM = new DrawnLineSymbolVM(positionConnectionPoint, drawnLineSymbolModel);
        _canvasSymbolsVM.CurrentDrawnLineSymbol = drawnLineSymbolVM;
        _canvasSymbolsVM.Symbols.Add(drawnLineSymbolVM);

        _canvasSymbolsVM.CurrentDrawnLineSymbol.SymbolOutgoingLine = connectionPoint.BlockSymbolVM;
    }

    private void EndDrawLine(ConnectionPoint connectionPoint)
    {
        var symbolaIncomingLine = connectionPoint.BlockSymbolVM;
        var incomingPositionConnectionPoint = connectionPoint.PositionConnectionPoint;
        var finalCoordinate = symbolaIncomingLine.GetBorderCoordinate(incomingPositionConnectionPoint);

        completedLineModel ??= new(_canvasSymbolsVM.CurrentDrawnLineSymbol!.DrawnLineSymbolModel, finalCoordinate);
        completedLineModel.Complete();

        _canvasSymbolsVM.CurrentDrawnLineSymbol!.ArrowSymbol.ChangeOrientationArrow(finalCoordinate, incomingPositionConnectionPoint);
        _canvasSymbolsVM.CurrentDrawnLineSymbol!.SymbolaIncomingLine = connectionPoint.BlockSymbolVM;

        AddBlockToLine(_canvasSymbolsVM.CurrentDrawnLineSymbol!.SymbolaIncomingLine);
        AddBlockToLine(_canvasSymbolsVM.CurrentDrawnLineSymbol!.SymbolOutgoingLine);

        _canvasSymbolsVM.CurrentDrawnLineSymbol = null;
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
            drawnLinesSymbolVM.Add(_canvasSymbolsVM.CurrentDrawnLineSymbol);
        }
        else
        {
            var drawnLinesSymbolVM = new List<DrawnLineSymbolVM?>
            {
                _canvasSymbolsVM.CurrentDrawnLineSymbol
            };
            _canvasSymbolsVM.BlockSymbolByLineSymbol.Add(blockSymbol, drawnLinesSymbolVM);
        }
    }
}