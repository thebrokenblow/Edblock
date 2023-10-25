using System;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;
using System.Collections.Generic;

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

    public int Diameter { get; init; } = ConnectionPointModel.diametr;

    private string? fill = ConnectionPointModel.HexNotFocusFill;
    public string? Fill
    {
        get => fill;
        set
        {
            fill = value;
            OnPropertyChanged();
        }
    }

    private string? stroke = ConnectionPointModel.HexNotFocusStroke;
    public string? Stroke
    {
        get => stroke;
        set
        {
            stroke = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public BlockSymbol BlockSymbol { get; init; }
    public DelegateCommand<ConnectionPoint> ClickConnectionPoint { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public Func<(int, int)> GetCoordinate { get; init; }
    public PositionConnectionPoint PositionConnectionPoint { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public ConnectionPoint(CanvasSymbolsVM canvasSymbolsVM, BlockSymbol blockSymbol, Func<(int, int)> getCoordinate, PositionConnectionPoint positionConnectionPoint)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        GetCoordinate = getCoordinate;

        PositionConnectionPoint = positionConnectionPoint;
        BlockSymbol = blockSymbol;
        
        EnterCursor = new(Show);
        LeaveCursor = new(Hide);
        ClickConnectionPoint = new(TrackStageDrawLine);

        (XCoordinate, YCoordinate) = getCoordinate.Invoke();
    }

    public void ChangeCoordination()
    {
        (XCoordinate, YCoordinate) = GetCoordinate.Invoke();
    }

    public void Show()
    {
        if (_canvasSymbolsVM.ScaleData == null)
        {
            _canvasSymbolsVM.Cursor = Cursors.Hand;
            SetFill(ConnectionPointModel.HexFocusFill, BlockSymbol.ConnectionPoints);
            Stroke = ConnectionPointModel.HexFocusStroke;
        }
    }

    public void Hide()
    {
        if (_canvasSymbolsVM.ScaleData == null)
        {
            _canvasSymbolsVM.Cursor = Cursors.Arrow;
            SetFill(ConnectionPointModel.HexNotFocusFill, BlockSymbol.ConnectionPoints);
            Stroke = ConnectionPointModel.HexNotFocusStroke;
        }
    }

    internal static void SetFill(string? hexFill, List<ConnectionPoint> connectionPoints)
    {
        foreach (var connectionPoint in connectionPoints)
        {
            connectionPoint.Fill = hexFill;
        }
    }

    public void TrackStageDrawLine(ConnectionPoint connectionPoint)
    {
        if (_canvasSymbolsVM.CurrentLines == null)
        {
            StarDrawLine(connectionPoint);
        }
        else
        {
            EndDrawLine(connectionPoint);
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    private void StarDrawLine(ConnectionPoint connectionPoint)
    {
        var coordinateConnectionPoint = (connectionPoint.XCoordinate, connectionPoint.YCoordinate);
        var positionConnectionPoint = connectionPoint.PositionConnectionPoint;
        var blockSymbolModel = connectionPoint.BlockSymbol.BlockSymbolModel;

        var drawnLineSymbolModel = new DrawnLineSymbolModel(connectionPoint.PositionConnectionPoint);
        drawnLineSymbolModel.AddFirstLine(coordinateConnectionPoint, positionConnectionPoint, blockSymbolModel);

        var drawnLineSymbolVM = new DrawnLineSymbolVM(positionConnectionPoint, drawnLineSymbolModel);
        _canvasSymbolsVM.CurrentLines = drawnLineSymbolVM;
        _canvasSymbolsVM.Symbols.Add(drawnLineSymbolVM);

        _canvasSymbolsVM.CurrentLines.SymbolOutgoingLine = connectionPoint.BlockSymbol;
    }

    private void EndDrawLine(ConnectionPoint connectionPoint)
    {
        _canvasSymbolsVM.CurrentLines!.SymbolaIncomingLine = connectionPoint.BlockSymbol;
        _canvasSymbolsVM.CurrentLines = null;
    }
}