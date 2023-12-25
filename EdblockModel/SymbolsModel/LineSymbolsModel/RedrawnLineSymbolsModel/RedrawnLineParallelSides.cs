using EdblockModel.SymbolsModel.Enum;
using EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbolsModel;

namespace EdblockModel.SymbolsModel.LineSymbolsModel.RedrawnLineSymbolsModel;

internal class RedrawnLineParallelSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly BuilderCoordinateDecorator _builderCoordinateDecorator;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesSamePositions = 1;
    private const int linesOneDifferentPositions = 3;

    public RedrawnLineParallelSides(DrawnLineSymbolModel drawnLineSymbolModel, RedrawnLine redrawLine, int baseLineOffset)
    {
        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        _decoratedCoordinatesLines = redrawLine.DecoratedCoordinatesLines;

        _redrawLine = redrawLine;

        _baseLineOffset = baseLineOffset;

        _builderCoordinateDecorator = SetBuilderCoordinateDecorator();
    }

    private BuilderCoordinateDecorator SetBuilderCoordinateDecorator()
    {
        var builderCoordinateDecorator = new BuilderCoordinateDecorator();

        if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left ||
            _positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right)
        {
            builderCoordinateDecorator = builderCoordinateDecorator.SetSwap();

            return builderCoordinateDecorator;
        }
        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            builderCoordinateDecorator = builderCoordinateDecorator.SetInversionYCoordinate();

            return builderCoordinateDecorator;
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Left)
        {
            builderCoordinateDecorator = builderCoordinateDecorator.SetInversionXCoordinate();

            return builderCoordinateDecorator;
        }

        return builderCoordinateDecorator;
    }

    public void RedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        (coordinateSymbolOutgoing, coordinateSymbolIncoming) =
                RedrawnLine.SetBuilderCoordinate(coordinateSymbolOutgoing, coordinateSymbolIncoming, _builderCoordinateDecorator);

        if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top ||
            _positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom ||
            _positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left ||
            _positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right)
        {
            ChooseWayRedrawDifferentSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }
        else
        {
            _redrawLine.ChangeCountLines(linesOneDifferentPositions, _builderCoordinateDecorator);

            (var firstCoordinateLineIncrement, var secondCoordinateLineIncrement) =
                GetVerticalCoordinateLineIncrement(coordinateSymbolOutgoing, coordinateSymbolIncoming);

            if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Left ||
                _positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Right)
            {
                (firstCoordinateLineIncrement, secondCoordinateLineIncrement) =
                    GetHorizontalCoordinateLineIncrement(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
    }

    private ((int x, int y), (int x, int y)) GetVerticalCoordinateLineIncrement(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        if (coordinateSymbolOutgoing.Y >= coordinateSymbolIncoming.Y)
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.Y - _baseLineOffset;

            var firstCoordinateLineIncrement = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLineIncrement = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            return (firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.Y - _baseLineOffset;

            var firstCoordinateLineIncrement = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLineIncrement = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            return (firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
    }

    private ((int x, int y), (int x, int y)) GetHorizontalCoordinateLineIncrement(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        if (coordinateSymbolOutgoing.X >= coordinateSymbolIncoming.X)
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.X + _baseLineOffset;

            var firstCoordinateLineIncrement = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLineIncrement = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            return (firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.X + _baseLineOffset;

            var firstCoordinateLineIncrement = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLineIncrement = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            return (firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
    }

    private void ChooseWayRedrawDifferentSides(
        ICoordinateDecorator coordinateSymbolOutgoing,
        ICoordinateDecorator coordinateSymbolIncoming,
        (int x, int y) borderCoordinateOutgoingSymbol,
        (int x, int y) borderCoordinateIncomingSymbol)
    {
        if (coordinateSymbolOutgoing.X == coordinateSymbolIncoming.X && coordinateSymbolOutgoing.Y < coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountLines(linesSamePositions);
            SetCoordinatesLastLine(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }
        else
        {
            _redrawLine.ChangeCountLines(linesOneDifferentPositions, _builderCoordinateDecorator);
            SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    private void SetCoordinatesLastLine(
        (int x, int y) borderCoordinateOutgoingSymbol,
        (int x, int y) borderCoordinateIncomingSymbol)
    {
        var firstLine = _decoratedCoordinatesLines[^1];

        firstLine.CoordinateSymbolOutgoing.X = borderCoordinateOutgoingSymbol.x;
        firstLine.CoordinateSymbolOutgoing.Y = borderCoordinateOutgoingSymbol.y;

        firstLine.CoordinateSymbolIncoming.X = borderCoordinateIncomingSymbol.x;
        firstLine.CoordinateSymbolIncoming.Y = borderCoordinateIncomingSymbol.y;
    }

    private void SetCoordinatesOneDifferentPositions(
        ICoordinateDecorator coordinateSymbolOutgoing,
        ICoordinateDecorator coordinateSymbolIncoming)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.CoordinateSymbolOutgoing.X = coordinateSymbolOutgoing.X;
        firstLine.CoordinateSymbolOutgoing.Y = coordinateSymbolOutgoing.Y;

        firstLine.CoordinateSymbolIncoming.X = coordinateSymbolOutgoing.X;
        firstLine.CoordinateSymbolIncoming.Y = coordinateSymbolOutgoing.Y + (coordinateSymbolIncoming.Y - coordinateSymbolOutgoing.Y) / 2;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.CoordinateSymbolOutgoing.X = firstLine.CoordinateSymbolIncoming.X;
        secondLine.CoordinateSymbolOutgoing.Y = firstLine.CoordinateSymbolIncoming.Y;

        secondLine.CoordinateSymbolIncoming.X = coordinateSymbolIncoming.X;
        secondLine.CoordinateSymbolIncoming.Y = firstLine.CoordinateSymbolIncoming.Y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.CoordinateSymbolOutgoing.X = secondLine.CoordinateSymbolIncoming.X;
        thirdLine.CoordinateSymbolOutgoing.Y = secondLine.CoordinateSymbolIncoming.Y;

        thirdLine.CoordinateSymbolIncoming.X = coordinateSymbolIncoming.X;
        thirdLine.CoordinateSymbolIncoming.Y = coordinateSymbolIncoming.Y;
    }

    private void SetCoordinatesIdenticalSides(
        ICoordinateDecorator coordinateSymbolOutgoing,
        ICoordinateDecorator coordinateSymbolIncoming,
        (int x, int y) coordinateFirstLine,
        (int x, int y) coordinateSecondLine)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.CoordinateSymbolOutgoing.X = coordinateSymbolOutgoing.X;
        firstLine.CoordinateSymbolOutgoing.Y = coordinateSymbolOutgoing.Y;

        firstLine.CoordinateSymbolIncoming.X = coordinateFirstLine.x;
        firstLine.CoordinateSymbolIncoming.Y = coordinateFirstLine.y;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.CoordinateSymbolOutgoing.X = firstLine.CoordinateSymbolIncoming.X;
        secondLine.CoordinateSymbolOutgoing.Y = firstLine.CoordinateSymbolIncoming.Y;

        secondLine.CoordinateSymbolIncoming.X = coordinateSecondLine.x;
        secondLine.CoordinateSymbolIncoming.Y = coordinateSecondLine.y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.CoordinateSymbolOutgoing.X = secondLine.CoordinateSymbolIncoming.X;
        thirdLine.CoordinateSymbolOutgoing.Y = secondLine.CoordinateSymbolIncoming.Y;

        thirdLine.CoordinateSymbolIncoming.X = coordinateSymbolIncoming.X;
        thirdLine.CoordinateSymbolIncoming.Y = coordinateSymbolIncoming.Y;
    }
}