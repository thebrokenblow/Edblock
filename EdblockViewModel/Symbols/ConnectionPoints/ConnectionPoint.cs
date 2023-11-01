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
    public DelegateCommand<ConnectionPoint> ClickConnectionPoint { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public BlockSymbol BlockSymbol { get; init; }
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

    private static (int, int) GetTopFinalCoordinate(ConnectionPoint connectionPoint)
    {
        int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width / 2;
        int finalY = connectionPoint.BlockSymbol.YCoordinate;

        return (finalX, finalY);
    }

    private static (int, int) GetBottomFinalCoordinate(ConnectionPoint connectionPoint)
    {
        int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width / 2;
        int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height;

        return (finalX, finalY);
    }

    private static (int, int) GetLeftFinalCoordinate(ConnectionPoint connectionPoint)
    {
        int finalX = connectionPoint.BlockSymbol.XCoordinate;
        int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

        return (finalX, finalY);
    }

    private static (int, int) GetRightFinalCoordinate(ConnectionPoint connectionPoint)
    {
        int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width;
        int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

        return (finalX, finalY);
    }

    private void FinishDrawingHorizontalToHorizontalLines(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.X2 == finalCoordinate.x)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new LineSymbolVM
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = lastLine.Y2
            };

            var secondLine = new LineSymbolVM
            {
                X1 = finalCoordinate.x,
                Y1 = lastLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            _canvasSymbolsVM.CurrentLines!.LineSymbols.Add(firstLine);
            _canvasSymbolsVM.CurrentLines.LineSymbols.Add(secondLine);
        }
    }

    private void FinishDrawingHorizontalToVerticalLines(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.Y2 = finalCoordinate.y;

        var firstLine = new LineSymbolVM
        {
            X1 = lastLine.X2,
            Y1 = finalCoordinate.y,
            X2 = finalCoordinate.x,
            Y2 = finalCoordinate.y
        };

        _canvasSymbolsVM.CurrentLines!.LineSymbols.Add(firstLine);
    }

    private void FinishDrawingVerticalToHorizontalLines(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.X2 = finalCoordinate.x;

        var firstLine = new LineSymbolVM
        {
            X1 = finalCoordinate.x,
            Y1 = lastLine.Y2,
            X2 = finalCoordinate.x,
            Y2 = finalCoordinate.y
        };

        _canvasSymbolsVM.CurrentLines!.LineSymbols.Add(firstLine);
    }

    private void FinishDrawingVerticalToVerticalLines(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.Y2 == finalCoordinate.y)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new LineSymbolVM
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = lastLine.X2,
                Y2 = finalCoordinate.y
            };

            var secondLine = new LineSymbolVM
            {
                X1 = lastLine.X2,
                Y1 = finalCoordinate.y,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            _canvasSymbolsVM.CurrentLines!.LineSymbols.Add(firstLine);
            _canvasSymbolsVM.CurrentLines!.LineSymbols.Add(secondLine);
        }
    }

    private void FinishDrawingHorizontalToHorizontalLines2(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.X2 = finalCoordinate.x;

        var firstLine = new LineSymbolVM
        {
            X1 = finalCoordinate.x,
            Y1 = lastLine.Y2,
            Y2 = finalCoordinate.y,
            X2 = finalCoordinate.x
        };

        _canvasSymbolsVM.CurrentLines!.LineSymbols.Add(firstLine);
    }

    private void FinishDrawingHorizontalToVerticalLines2(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.Y2 == finalCoordinate.y)
        {
            lastLine.X2 = finalCoordinate.x;
        }
        else
        {
            var secondLine = _canvasSymbolsVM.CurrentLines!.LineSymbols[^2];

            secondLine.Y2 = finalCoordinate.y;

            lastLine.Y1 = finalCoordinate.y;
            lastLine.Y2 = finalCoordinate.y;
            lastLine.X2 = finalCoordinate.x;
        }
    }
    private void FinishDrawingVerticalToHorizontalLines2(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        if (finalCoordinate.x == lastLine.X2)
        {
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var secondLine = _canvasSymbolsVM.CurrentLines!.LineSymbols[^2];

            secondLine.X2 = finalCoordinate.x;

            lastLine.X1 = finalCoordinate.x;
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
    }

    private void FinishDrawingVerticalToVerticalLines2(LineSymbolVM lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.Y2 = finalCoordinate.y;

        var firstLine = new LineSymbolVM
        {
            X1 = lastLine.X2,
            Y1 = lastLine.Y2,
            X2 = finalCoordinate.x,
            Y2 = lastLine.Y2
        };

        _canvasSymbolsVM.CurrentLines!.LineSymbols.Add(firstLine);
    }

    private void EndDrawLine(ConnectionPoint connectionPoint)
    {
        var outgoingPositionConnectionPoint = _canvasSymbolsVM.CurrentLines!.PositionOutgoingConnectionPoint;
        var incomingPositionConnectionPoint = connectionPoint.PositionConnectionPoint;

        var finalCoordinate = (x: 0, y: 0);
        var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];
        if (_canvasSymbolsVM.CurrentLines!.LineSymbols.Count % 2 == 1)
        {
            if (outgoingPositionConnectionPoint == PositionConnectionPoint.Bottom ||
                outgoingPositionConnectionPoint == PositionConnectionPoint.Top)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {
                    finalCoordinate = GetTopFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToHorizontalLines(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {
                    finalCoordinate = GetBottomFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToHorizontalLines(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    finalCoordinate = GetLeftFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToVerticalLines(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    finalCoordinate = GetRightFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToVerticalLines(lastLine, finalCoordinate);
                }
            }
            else if (outgoingPositionConnectionPoint == PositionConnectionPoint.Left ||
                     outgoingPositionConnectionPoint == PositionConnectionPoint.Right)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {
                    finalCoordinate = GetTopFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToHorizontalLines(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {
                    finalCoordinate = GetTopFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToHorizontalLines(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    finalCoordinate = GetLeftFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToVerticalLines(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    finalCoordinate = GetRightFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToVerticalLines(lastLine, finalCoordinate);
                }
            }
        }
        else
        {
            if (outgoingPositionConnectionPoint == PositionConnectionPoint.Bottom ||
                outgoingPositionConnectionPoint == PositionConnectionPoint.Top)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {
                    finalCoordinate = GetTopFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToHorizontalLines2(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {
                    finalCoordinate = GetBottomFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToHorizontalLines2(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    finalCoordinate = GetLeftFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToVerticalLines2(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    finalCoordinate = GetRightFinalCoordinate(connectionPoint);
                    FinishDrawingHorizontalToVerticalLines2(lastLine, finalCoordinate);
                }
            }
            else if (outgoingPositionConnectionPoint == PositionConnectionPoint.Left ||
                outgoingPositionConnectionPoint == PositionConnectionPoint.Right)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {
                    finalCoordinate = GetTopFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToHorizontalLines2(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {
                    finalCoordinate = GetBottomFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToHorizontalLines2(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    finalCoordinate = GetLeftFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToVerticalLines2(lastLine, finalCoordinate);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    finalCoordinate = GetRightFinalCoordinate(connectionPoint);
                    FinishDrawingVerticalToVerticalLines2(lastLine, finalCoordinate);
                }
            }
        }
        _canvasSymbolsVM.CurrentLines.PositionIncomingConnectionPoint = incomingPositionConnectionPoint;

        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(finalCoordinate, incomingPositionConnectionPoint);
        _canvasSymbolsVM.CurrentLines!.SymbolaIncomingLine = connectionPoint.BlockSymbol;

        if (_canvasSymbolsVM.CurrentLines.SymbolOutgoingLine != null)
        {
            _canvasSymbolsVM.BlockSymbolByLineSymbol.Add(_canvasSymbolsVM.CurrentLines.SymbolOutgoingLine, _canvasSymbolsVM.CurrentLines);
        }
        if (_canvasSymbolsVM.CurrentLines.SymbolaIncomingLine != null)
        {
            _canvasSymbolsVM.BlockSymbolByLineSymbol.Add(_canvasSymbolsVM.CurrentLines.SymbolaIncomingLine, _canvasSymbolsVM.CurrentLines);
        }

        _canvasSymbolsVM.CurrentLines = null;
    }
}