using EdblockModel.Symbols.ConnectionPoints;

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
        if (outgoingPosition == PositionConnectionPoint.Bottom || incomingPosition == PositionConnectionPoint.Top)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var lastLine = linesSymbolModel[^1];
                ILineCoordinateDecorator coordinateLineDecorator = new LineCoordinateDecorator((lastLine.X2, lastLine.Y2), _finalCoordinate);

                CompleteParallel2(linesSymbolModel, _finalCoordinate, coordinateLineDecorator);
            }
        }
        else if (outgoingPosition == PositionConnectionPoint.Left || outgoingPosition == PositionConnectionPoint.Right)
        {
            if (incomingPosition == PositionConnectionPoint.Left || incomingPosition == PositionConnectionPoint.Right)
            {
                var lastLine = linesSymbolModel[^1];
                var build = new BuildLineCoordinateDecorato().SetSwapFinalCoordinate();
                ILineCoordinateDecorator coordinateLineDecorator = new LineCoordinateDecorator((lastLine.X2, lastLine.Y2), _finalCoordinate);
                coordinateLineDecorator = build.Build(coordinateLineDecorator);
                CompleteParallel2(linesSymbolModel, _finalCoordinate, coordinateLineDecorator);
            }
        }

        return linesSymbolModel;
    }

    interface ILineCoordinateDecorator
    {
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int FinalCoordinateX { get; set; }
        public int FinalCoordinateY { get; set; }
    }

    class LineCoordinateDecorator : ILineCoordinateDecorator
    {
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int FinalCoordinateX { get; set; }
        public int FinalCoordinateY { get; set; }

        public LineCoordinateDecorator((int x, int y) lineCoordinate, (int x, int y) finalCoordinate)
        {
            X2 = lineCoordinate.x;
            Y2 = lineCoordinate.y;
            FinalCoordinateX = finalCoordinate.x;
            FinalCoordinateY = finalCoordinate.y;
        }
    }

    class SwapFinalCoordinate : ILineCoordinateDecorator
    {
        public int X2
        {
            get => _lineCoordinateDecorator.FinalCoordinateX;
            set => _lineCoordinateDecorator.FinalCoordinateX = value;
        }

        public int Y2
        {
            get => _lineCoordinateDecorator.FinalCoordinateY;
            set => _lineCoordinateDecorator.FinalCoordinateY = value;
        }

        public int FinalCoordinateX
        {
            get => _lineCoordinateDecorator.X2;
            set => _lineCoordinateDecorator.X2 = value;
        }

        public int FinalCoordinateY
        {
            get => _lineCoordinateDecorator.Y2;
            set => _lineCoordinateDecorator.Y2 = value;
        }

        ILineCoordinateDecorator _lineCoordinateDecorator;
        public SwapFinalCoordinate(ILineCoordinateDecorator lineCoordinateDecorator)
        {
            _lineCoordinateDecorator = lineCoordinateDecorator;
        }
    }

    class BuildLineCoordinateDecorato
    {
        private bool IsSetSwapFinalCoordinate = false;

        public BuildLineCoordinateDecorato SetSwapFinalCoordinate()
        {
            IsSetSwapFinalCoordinate = true;

            return this;
        }

        public ILineCoordinateDecorator Build(ILineCoordinateDecorator coordinateDecorator)
        {
            if (IsSetSwapFinalCoordinate)
            {
                coordinateDecorator = new SwapFinalCoordinate(coordinateDecorator);
            }

            return coordinateDecorator;
        }
    }

    private List<LineSymbolModel> CompleteParallel2(List<LineSymbolModel> linesSymbolModel, (int x, int y) finalCoordinate, ILineCoordinateDecorator lineCoordinateDecorator)
    {
        var lastLine = linesSymbolModel[^1];

        if (lastLine.X2 == finalCoordinate.x || lastLine.Y2 == finalCoordinate.y)
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
                X2 = lineCoordinateDecorator.FinalCoordinateX,
                Y2 = lineCoordinateDecorator.Y2,
            };

            var secondLine = new LineSymbolModel
            {
                X1 = lineCoordinateDecorator.FinalCoordinateX,
                Y1 = lineCoordinateDecorator.Y2,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            linesSymbolModel.Add(firstLine);
            linesSymbolModel.Add(secondLine);
        }

        return linesSymbolModel;
    }
}