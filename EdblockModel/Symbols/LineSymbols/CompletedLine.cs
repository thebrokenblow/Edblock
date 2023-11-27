using EdblockModel.Symbols.Enum;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols;

public class CompletedLineModel
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly LineSymbolModel lastLine;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLineModel(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        lastLine = linesSymbolModel[^1];
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    public List<LineSymbolModel> GetCompleteLines()
    {
        if (linesSymbolModel.Count % 2 == 1)
        {
            if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    FinishDrawingHorizontalToHorizontalLines(lastLine, _finalCoordinate);
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
                    FinishDrawingVerticalToVerticalLines(lastLine, _finalCoordinate);
                }
            }
        }
        else
        {
            if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    lastLine.X2 = _finalCoordinate.x;
                    FinishLineParallelSide();
                }
                else
                {
                    var penultimateLine = linesSymbolModel[^2];

                    ICoordinateLineDecorator coordinateLastLine = new SwapCoordinateLineDecorator(new CoordinateLineDecorator(lastLine), lastLine);
                    ICoordinateLineDecorator coordinatePenultimateLine = new SwapCoordinateLineDecorator(new CoordinateLineDecorator(penultimateLine), penultimateLine);
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
    private void FinishLineParallelSide()
    {
        var firstLine = new LineSymbolModel
        {
            X1 = lastLine.X2,
            Y1 = lastLine.Y2,
            X2 = _finalCoordinate.x,
            Y2 = _finalCoordinate.y
        };

        linesSymbolModel.Add(firstLine);
    }

    private void FinishDrawingVerticalToVerticalLines(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.Y2 == finalCoordinate.y)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = lastLine.X2,
                Y2 = finalCoordinate.y
            };

            var secondLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            linesSymbolModel.Add(firstLine);
            linesSymbolModel.Add(secondLine);
        }
    }

    private void FinishDrawingHorizontalToHorizontalLines(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.X2 == finalCoordinate.x)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = lastLine.Y2
            };

            var secondLine = new LineSymbolModel
            {
                X1 = firstLine.X2,
                Y1 = firstLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            linesSymbolModel.Add(firstLine);
            linesSymbolModel.Add(secondLine);
        }
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
                return _coordinateLine.Y1;
            }
            set
            {
                _coordinateLine.Y1 = value;
            }
        }

        public int Y1
        {
            get
            {
                return _coordinateLine.X1;
            }
            set
            {
                _coordinateLine.X1 = value;
            }
        }

        public int X2
        {
            get
            {
                return _coordinateLine.Y2;
            }
            set
            {
                _coordinateLine.Y2 = value;
            }
        }

        public int Y2 
        {
            get
            {
                return _coordinateLine.X2;
            }
            set
            {
                _coordinateLine.X2 = value;
            }
        }

        private readonly ICoordinateLineDecorator _coordinateLine;
        private readonly LineSymbolModel _lineSymbol;

        public SwapCoordinateLineDecorator(ICoordinateLineDecorator coordinateLine, LineSymbolModel lineSymbol)
        {
            _coordinateLine = coordinateLine;
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