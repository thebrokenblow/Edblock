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
        var outgoingPositionConnectionPoint = _canvasSymbolsVM.CurrentLines!.PositionConnectionPoint;
        var incomingPositionConnectionPoint = connectionPoint.PositionConnectionPoint;

        if (_canvasSymbolsVM.CurrentLines!.LineSymbols.Count % 2 == 1)
        {
            if (outgoingPositionConnectionPoint == PositionConnectionPoint.Bottom)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width / 2;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate - 8;

                    if (lastLine.X2 == finalX)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        var firstLine = new LineSymbolVM
                        {
                            X1 = lastLine.X2,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = lastLine.Y2
                        };

                        var secondLine = new LineSymbolVM
                        {
                            X1 = finalX,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = finalY,
                        };

                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(secondLine);

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(secondLine.X2, secondLine.Y2, incomingPositionConnectionPoint);
                    }
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width / 2;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height + 8;

                    if (lastLine.X2 == finalX)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        var firstLine = new LineSymbolVM
                        {
                            X1 = lastLine.X2,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = lastLine.Y2
                        };

                        var secondLine = new LineSymbolVM
                        {
                            X1 = finalX,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = finalY,
                        };

                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(secondLine);

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(secondLine.X2, secondLine.Y2, incomingPositionConnectionPoint);
                    }
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    lastLine.Y2 = finalY;

                    var firstLine = new LineSymbolVM
                    {
                        X1 = lastLine.X2,
                        Y1 = finalY,
                        X2 = finalX,
                        Y2 = finalY
                    };

                    _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                    _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(finalX, finalY, incomingPositionConnectionPoint);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    lastLine.Y2 = finalY;

                    var firstLine = new LineSymbolVM
                    {
                        X1 = lastLine.X2,
                        Y1 = finalY,
                        X2 = finalX,
                        Y2 = finalY
                    };

                    _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                    _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(finalX, finalY, incomingPositionConnectionPoint);
                }
            }
            else if (outgoingPositionConnectionPoint == PositionConnectionPoint.Top)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width / 2;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate - 8;

                    if (lastLine.X2 == finalX)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        var firstLine = new LineSymbolVM
                        {
                            X1 = lastLine.X2,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = lastLine.Y2
                        };

                        var secondLine = new LineSymbolVM
                        {
                            X1 = finalX,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = finalY,
                        };

                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(secondLine);

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(secondLine.X2, secondLine.Y2, incomingPositionConnectionPoint);
                    }
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width / 2;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height + 8;

                    if (lastLine.X2 == finalX)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        var firstLine = new LineSymbolVM
                        {
                            X1 = lastLine.X2,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = lastLine.Y2
                        };

                        var secondLine = new LineSymbolVM
                        {
                            X1 = finalX,
                            Y1 = lastLine.Y2,
                            X2 = finalX,
                            Y2 = finalY,
                        };

                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                        _canvasSymbolsVM.CurrentLines.LineSymbols.Add(secondLine);

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(secondLine.X2, secondLine.Y2, incomingPositionConnectionPoint);
                    }
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    lastLine.Y2 = finalY;

                    var firstLine = new LineSymbolVM
                    {
                        X1 = lastLine.X2,
                        Y1 = finalY,
                        X2 = finalX,
                        Y2 = finalY
                    };

                    _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                    _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(finalX, finalY, incomingPositionConnectionPoint);
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    lastLine.Y2 = finalY;

                    var firstLine = new LineSymbolVM
                    {
                        X1 = lastLine.X2,
                        Y1 = finalY,
                        X2 = finalX,
                        Y2 = finalY
                    };

                    _canvasSymbolsVM.CurrentLines.LineSymbols.Add(firstLine);
                    _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(finalX, finalY, incomingPositionConnectionPoint);
                }
            }
            else if (outgoingPositionConnectionPoint == PositionConnectionPoint.Left)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {
                    
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {

                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    if (lastLine.Y2 == finalY)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        //TODO: создать две линии и дорисовать
                    }
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    if (lastLine.Y2 == finalY)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        //TODO: создать две линии и дорисовать
                    }
                }
            }
            else if (outgoingPositionConnectionPoint == PositionConnectionPoint.Right)
            {
                if (incomingPositionConnectionPoint == PositionConnectionPoint.Top)
                {

                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Bottom)
                {

                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Left)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    if (lastLine.Y2 == finalY)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        //TODO: создать две линии и дорисовать
                    }
                }
                else if (incomingPositionConnectionPoint == PositionConnectionPoint.Right)
                {
                    var lastLine = _canvasSymbolsVM.CurrentLines.LineSymbols[^1];

                    int finalX = connectionPoint.BlockSymbol.XCoordinate + connectionPoint.BlockSymbol.Width;
                    int finalY = connectionPoint.BlockSymbol.YCoordinate + connectionPoint.BlockSymbol.Height / 2;

                    if (lastLine.Y2 == finalY)
                    {
                        lastLine.X2 = finalX;
                        lastLine.Y2 = finalY;

                        _canvasSymbolsVM.CurrentLines!.ArrowSymbol.ChangeOrientationArrow(lastLine.X2, lastLine.Y2, incomingPositionConnectionPoint);
                    }
                    else
                    {
                        //TODO: создать две линии и дорисовать
                    }
                }
            }
        }
        _canvasSymbolsVM.CurrentLines!.SymbolaIncomingLine = connectionPoint.BlockSymbol;
        _canvasSymbolsVM.CurrentLines = null;
    }
}