using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols;

public class CompletedLineModel
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLineModel(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    public List<LineSymbolModel> GetCompleteLines()
    {
        var lastLine = linesSymbolModel[^1];
        if (linesSymbolModel.Count % 2 == 1)
        {
            if (outgoingPosition == PositionConnectionPoint.Top || outgoingPosition == PositionConnectionPoint.Bottom)
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    var coordinatePenultimateLine = (_finalCoordinate.x, lastLine.Y2);
                    FinishDrawingVerticalToVerticalLines(coordinatePenultimateLine);
                }
                else
                {
                    lastLine.Y2 = _finalCoordinate.y;
                    FinishLineParallelSide();
                }
            }
            else
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    lastLine.X2 = _finalCoordinate.x;
                    FinishLineParallelSide();
                }
                else
                {
                    var coordinatePenultimateLine = (lastLine.X2, _finalCoordinate.y);
                    FinishDrawingVerticalToVerticalLines(coordinatePenultimateLine);
                }
            }
        }
        else
        {
            if (outgoingPosition == PositionConnectionPoint.Top || outgoingPosition == PositionConnectionPoint.Bottom)
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    lastLine.X2 = _finalCoordinate.x;
                    FinishLineParallelSide();
                }
                else
                {
                    var penultimateLine = linesSymbolModel[^2];

                    ICoordinateLineDecorator coordinateLastLine = new SwapCoordinateLineDecorator(lastLine);
                    ICoordinateLineDecorator coordinatePenultimateLine = new SwapCoordinateLineDecorator(penultimateLine);
                    ICoordinateDecorator finalCoordinate = new SwapCoordinateDecorator(new CoordinateDecorator(_finalCoordinate));

                    FinishLineNotParallelSide(coordinateLastLine, coordinatePenultimateLine, finalCoordinate);
                }
            }
            else
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    var penultimateLine = linesSymbolModel[^2];

                    ICoordinateLineDecorator coordinateLastLine = new CoordinateLineDecorator(lastLine);
                    ICoordinateLineDecorator coordinatePenultimateLine = new CoordinateLineDecorator(penultimateLine);
                    ICoordinateDecorator finalCoordinate = new CoordinateDecorator(_finalCoordinate);

                    FinishLineNotParallelSide(coordinateLastLine, coordinatePenultimateLine, finalCoordinate);
                }
                else
                {
                    lastLine.Y2 = _finalCoordinate.y;
                    FinishLineParallelSide();
                }
            }
        }

        return linesSymbolModel;
    }

    private void FinishDrawingVerticalToVerticalLines((int X2, int Y2) coordinatePenultimateLine)
    {
        var lastLine = linesSymbolModel[^1];

        if (lastLine.X2 == _finalCoordinate.x || lastLine.Y2 == _finalCoordinate.y)
        {
            lastLine.X2 = _finalCoordinate.x;
            lastLine.Y2 = _finalCoordinate.y;
        }
        else
        {
            var penultimateLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = coordinatePenultimateLine.X2,
                Y2 = coordinatePenultimateLine.Y2,
            };

            linesSymbolModel.Add(penultimateLine);

            FinishLineParallelSide();
        }
    }

    private void FinishLineParallelSide()
    {
        var penultimateLine = linesSymbolModel[^1];

        var lastLine = new LineSymbolModel
        {
            X1 = penultimateLine.X2,
            Y1 = penultimateLine.Y2,
            X2 = _finalCoordinate.x,
            Y2 = _finalCoordinate.y
        };

        linesSymbolModel.Add(lastLine);
    }

    interface ICoordinateLineDecorator
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public void SetCoordinate();
    }

    class CoordinateLineDecorator : ICoordinateLineDecorator
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        private readonly LineSymbolModel _lineSymbol;

        public CoordinateLineDecorator(LineSymbolModel lineSymbol)
        {
            X1 = lineSymbol.X1;
            Y1 = lineSymbol.Y1;
            X2 = lineSymbol.X2;
            Y2 = lineSymbol.Y2;
            _lineSymbol = lineSymbol;
        }

        public void SetCoordinate()
        {
            _lineSymbol.X1 = X1;
            _lineSymbol.Y1 = Y1;
            _lineSymbol.X2 = X2;
            _lineSymbol.Y2 = Y2;
        }

    }

    class SwapCoordinateLineDecorator : ICoordinateLineDecorator
    {
        public int X1 
        {
            get
            {
                return _lineSymbol.Y1;
            }
            set
            {
                _lineSymbol.Y1 = value;
            }
        }

        public int Y1
        {
            get
            {
                return _lineSymbol.X1;
            }
            set
            {
                _lineSymbol.X1 = value;
            }
        }

        public int X2
        {
            get
            {
                return _lineSymbol.Y2;
            }
            set
            {
                _lineSymbol.Y2 = value;
            }
        }

        public int Y2 
        {
            get
            {
                return _lineSymbol.X2;
            }
            set
            {
                _lineSymbol.X2 = value;
            }
        }

        private readonly LineSymbolModel _lineSymbol;

        public SwapCoordinateLineDecorator(LineSymbolModel lineSymbol)
        {
            _lineSymbol = lineSymbol;
        }

        public void SetCoordinate()
        {
            _lineSymbol.X1 = Y1;
            _lineSymbol.Y1 = X1;
            _lineSymbol.X2 = Y2;
            _lineSymbol.Y2 = X2;
        }
    }

    private static void FinishLineNotParallelSide(ICoordinateLineDecorator coordinateLastLine, ICoordinateLineDecorator coordinatePenultimateLine, ICoordinateDecorator finalCoordinate)
    {
        if (coordinateLastLine.X2 == finalCoordinate.X)
        {
            coordinateLastLine.Y2 = finalCoordinate.Y;
            coordinateLastLine.SetCoordinate();
        }
        else
        {
            coordinatePenultimateLine.X2 = finalCoordinate.X;

            coordinateLastLine.X1 = coordinatePenultimateLine.X2;
            coordinateLastLine.X2 = coordinatePenultimateLine.X2;
            coordinateLastLine.Y2 = finalCoordinate.Y;

            coordinateLastLine.SetCoordinate();
            coordinatePenultimateLine.SetCoordinate();
        }
    }
}