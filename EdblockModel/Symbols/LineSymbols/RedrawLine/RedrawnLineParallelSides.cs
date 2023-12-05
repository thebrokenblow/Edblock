using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineParallelSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolaIncomingLine;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesSamePositions = 1;
    private const int linesIdenticalSides = 3;
    private const int linesOneDifferentPositions = 3;
    private const int linesTwoDifferentPositions = 5;

    public RedrawnLineParallelSides(DrawnLineSymbolModel drawnLineSymbolModel, RedrawnLine redrawLine, int baseLineOffset)
    {
        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolaIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        _redrawLine = redrawLine;
        _decoratedCoordinatesLines = redrawLine.DecoratedCoordinatesLines;

        _baseLineOffset = baseLineOffset;
    }

    public void RedrawLine()
    {
        var borderCoordinateOutgoingSymbol = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateIncomingSymbol = _symbolaIncomingLine!.GetBorderCoordinate(_positionIncoming);

        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top)
        {
            RedrawLineBottomToTopSides(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            RedrawLineBottomToTopSides(coordinateSymbolIncoming, coordinateSymbolOutgoing);
            _redrawLine.ReverseCoordinateLine();
        }
        else if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left)
        {
            RedrawLineRightToLeftSides(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right)
        {
            RedrawLineLeftToRightSides(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }

        if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Top)
        {
            RedrawLineTopSide(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            RedrawLineBottomSide(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Right)
        {
            RedrawLineRightSide(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Left)
        {
            RedrawLineLeftSide(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
        }
    }

    private void RedrawLineBottomToTopSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        var widthlIncomingSymbol = _symbolaIncomingLine!.Width;

        if (coordinateSymbolOutgoing.X == coordinateSymbolIncoming.X && coordinateSymbolOutgoing.Y < coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountDecoratedLines(linesSamePositions);
            SetCoordinatesLastLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
        else if (coordinateSymbolOutgoing.Y < coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions);
            SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
        else
        {
            _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions);
            SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, widthlIncomingSymbol);
        }
    }

    private void RedrawLineRightToLeftSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        if (coordinateSymbolOutgoing.Y == coordinateSymbolIncoming.Y && coordinateSymbolOutgoing.X == coordinateSymbolIncoming.X)
        {
            _redrawLine.ChangeCountDecoratedLines(linesSamePositions);
            SetCoordinatesLastLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
        else if (coordinateSymbolOutgoing.X > coordinateSymbolIncoming.X)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap().SetInversionYCoordinate();

            coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions, buildCoordinateDecorator);

            SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, _symbolaIncomingLine!.Height);
        }
        else
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap();

            coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions, buildCoordinateDecorator);

            SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    private void RedrawLineLeftToRightSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        if (coordinateSymbolOutgoing.Y == coordinateSymbolIncoming.Y && coordinateSymbolOutgoing.X == coordinateSymbolIncoming.X)
        {
            _redrawLine.ChangeCountDecoratedLines(linesSamePositions);
            SetCoordinatesLastLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
        else if (coordinateSymbolOutgoing.X < coordinateSymbolIncoming.X)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap().SetInversionXCoordinate();

            coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions, buildCoordinateDecorator);

            SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, _symbolaIncomingLine!.Height);
        }
        else
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap();

            coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions, buildCoordinateDecorator);

            SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    private void SetCoordinatesLastLine(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        var firstLine = _decoratedCoordinatesLines[^1];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        firstLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }

    private void SetCoordinatesOneDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateSymbolOutgoing.Y + (coordinateSymbolIncoming.Y - coordinateSymbolOutgoing.Y) / 2;

        SetCoordinatePenultimateLine(coordinateSymbolIncoming);

        var secondLine = _decoratedCoordinatesLines[1];
        var coordinateSecodLine = secondLine.SecondCoordinate;

        SetCoordinatesLastLine(coordinateSecodLine, coordinateSymbolIncoming);
    }

    private void SetCoordinatePenultimateLine(ICoordinateDecorator coordinateSymbolIncoming)
    {
        var previousPenultimateLine = _decoratedCoordinatesLines[^3];
        var coordinatePreviousPenultimateLine = previousPenultimateLine.SecondCoordinate;

        var penultimateLine = _decoratedCoordinatesLines[^2];

        penultimateLine.FirstCoordinate.X = coordinatePreviousPenultimateLine.X;
        penultimateLine.FirstCoordinate.Y = coordinatePreviousPenultimateLine.Y;

        penultimateLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        penultimateLine.SecondCoordinate.Y = coordinatePreviousPenultimateLine.Y;
    }

    private void SetCoordinatesTwoDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int widthIncommingSymbol)
    {
        int offsetIncommingSymbolLine = widthIncommingSymbol / 2 + _baseLineOffset; //TODO: При масштабировании в ширину тут проблена происходит

        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateSymbolOutgoing.Y + _baseLineOffset;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.FirstCoordinate.Y = firstLine.SecondCoordinate.Y;

        secondLine.SecondCoordinate.X = coordinateSymbolOutgoing.X + offsetIncommingSymbolLine;
        secondLine.SecondCoordinate.Y = firstLine.SecondCoordinate.Y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.FirstCoordinate.Y = secondLine.SecondCoordinate.Y;

        thirdLine.SecondCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y - _baseLineOffset;

        SetCoordinatePenultimateLine(coordinateSymbolIncoming);

        var fourthLine = _decoratedCoordinatesLines[3];
        var coordinatePenultimateLine = fourthLine.SecondCoordinate;

        SetCoordinatesLastLine(coordinatePenultimateLine, coordinateSymbolIncoming);
    }

    private void RedrawLineTopSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides);

        if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.Y - _baseLineOffset;

            var firstCoordinateLineIncrement = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLineIncrement = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.Y - _baseLineOffset;

            var firstCoordinateLineIncrement = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLineIncrement = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLineIncrement, secondCoordinateLineIncrement);
        }
    }

    private void RedrawLineBottomSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();
        coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

        if (borderCoordinateIncomingSymbol.y >= borderCoordinateOutgoingSymbol.y)
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.Y - _baseLineOffset;

            var firstCoordinateLine = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLine = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.Y - _baseLineOffset;

            var firstCoordinateLine = (coordinateSymbolOutgoing.X, lineCoordinateIncrement);
            var secondCoordinateLine = (coordinateSymbolIncoming.X, lineCoordinateIncrement);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
    }

    private void RedrawLineRightSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var buildCoordinateDecorator = new BuilderCoordinateDecorator();

        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

        if (borderCoordinateIncomingSymbol.x <= borderCoordinateOutgoingSymbol.x)
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.X + _baseLineOffset;

            var firstCoordinateLine = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLine = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.X + _baseLineOffset;

            var firstCoordinateLine = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLine = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
    }

    private void RedrawLineLeftSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionXCoordinate();

        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

        if (borderCoordinateIncomingSymbol.x >= borderCoordinateOutgoingSymbol.x)
        {
            var lineCoordinateIncrement = coordinateSymbolOutgoing.X + _baseLineOffset;

            var firstCoordinateLine = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLine = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
        else
        {
            var lineCoordinateIncrement = coordinateSymbolIncoming.X + _baseLineOffset;

            var firstCoordinateLine = (lineCoordinateIncrement, coordinateSymbolOutgoing.Y);
            var secondCoordinateLine = (lineCoordinateIncrement, coordinateSymbolIncoming.Y);

            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, firstCoordinateLine, secondCoordinateLine);
        }
    }

    private void SetCoordinatesIdenticalSides(
        ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming,
        (int x, int y) coordinateFirstLine, (int x, int y) coordinateSecondLine)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateFirstLine.x;
        firstLine.SecondCoordinate.Y = coordinateFirstLine.y;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.FirstCoordinate.Y = firstLine.SecondCoordinate.Y;

        secondLine.SecondCoordinate.X = coordinateSecondLine.x;
        secondLine.SecondCoordinate.Y = coordinateSecondLine.y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.FirstCoordinate.Y = secondLine.SecondCoordinate.Y;

        thirdLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }
}