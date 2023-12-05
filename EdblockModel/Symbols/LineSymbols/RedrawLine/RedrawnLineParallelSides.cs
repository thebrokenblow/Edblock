using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineParallelSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly BuilderCoordinateDecorator _builderCoordinateDecorator;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesSamePositions = 1;
    private const int linesIdenticalSides = 3;
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
        if ((_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left) ||
            _positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right)
        {
            var builderCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap();

            return builderCoordinateDecorator;
        }
        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            var builderCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionXCoordinate();

            return builderCoordinateDecorator;
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Left)
        {
            var builderCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();

            return builderCoordinateDecorator;
        }
        else
        {
            var builderCoordinateDecorator = new BuilderCoordinateDecorator();

            return builderCoordinateDecorator;
        }
    }

    public void RedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        (coordinateSymbolOutgoing, coordinateSymbolIncoming) =
                RedrawnLine.SetBuilderCoordinate(coordinateSymbolOutgoing, coordinateSymbolIncoming, _builderCoordinateDecorator);

        if ((_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top) ||
            (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom) ||
            (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right))
        {
            ChooseWayToRedrawDifferentSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }

        if ((_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Top) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom))
        {
            RedrawLineTopSide(coordinateSymbolOutgoing, coordinateSymbolIncoming, -_baseLineOffset);
        }
        else if ((_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Left))
        {
            RedrawLineRightSide(coordinateSymbolOutgoing, coordinateSymbolIncoming, _baseLineOffset);
        }
    }

    private void RedrawLineTopSide(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int offsetCoordinate)
    {
        _redrawLine.ChangeCountLines(linesIdenticalSides, _builderCoordinateDecorator);

        if (coordinateSymbolOutgoing.Y >= coordinateSymbolIncoming.Y)
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.Y + offsetCoordinate;

            var firstCoordinateLineIncrement = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLineIncrement = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.Y + offsetCoordinate;

            var firstCoordinateLineIncrement = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLineIncrement = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
    }

    private void RedrawLineRightSide(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int offsetCoordinate)
    {
        _redrawLine.ChangeCountLines(linesIdenticalSides, _builderCoordinateDecorator);

        if (coordinateSymbolOutgoing.X >= coordinateSymbolIncoming.X)
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.X + offsetCoordinate;

            var firstCoordinateLine = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLine = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.X + offsetCoordinate;

            var firstCoordinateLine = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLine = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
    }

    private void ChooseWayToRedrawDifferentSides(
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

    private void SetCoordinatesLastLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var firstLine = _decoratedCoordinatesLines[^1];

        firstLine.CoordinateSymbolOutgoing.X = borderCoordinateOutgoingSymbol.x;
        firstLine.CoordinateSymbolOutgoing.Y = borderCoordinateOutgoingSymbol.y;

        firstLine.CoordinateSymbolIncoming.X = borderCoordinateIncomingSymbol.x;
        firstLine.CoordinateSymbolIncoming.Y = borderCoordinateIncomingSymbol.y;
    }


    private void SetCoordinatesOneDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
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