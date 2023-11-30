using EdblockModel.Symbols.Enum;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineIdenticalSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesIdenticalSides = 3;

    public RedrawnLineIdenticalSides(DrawnLineSymbolModel drawnLineSymbolModel, RedrawnLine redrawLine, int baseLineOffset)
    {
        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        _redrawLine = redrawLine;
        _decoratedCoordinatesLines = redrawLine.DecoratedCoordinatesLines;

        _baseLineOffset = baseLineOffset;
    }

    public void RedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
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

    private void RedrawLineTopSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides);

        if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
        {
            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolIncoming.Y);
        }
        else
        {
            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolOutgoing.Y);
        }
    }

    private void RedrawLineBottomSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();
        coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

        _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

        if (borderCoordinateIncomingSymbol.y >= borderCoordinateOutgoingSymbol.y)
        {
            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolIncoming.Y);
        }
        else
        {
            SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolOutgoing.Y);
        }
    }

    private void RedrawLineRightSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var buildCoordinateDecorator = new BuilderCoordinateDecorator();

        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

        _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

        if (borderCoordinateIncomingSymbol.x <= borderCoordinateOutgoingSymbol.x)
        {
            SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolOutgoing.X);
        }
        else
        {
            SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolIncoming.X);
        }
    }

    private void RedrawLineLeftSide((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionXCoordinate();

        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

        _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
        _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

        if (borderCoordinateIncomingSymbol.x >= borderCoordinateOutgoingSymbol.x)
        {
            SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolOutgoing.X);
        }
        else
        {
            SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolIncoming.X);
        }
    }

    private void SetCoordinatesIdenticalSides1(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int coordinateUnmovableSymbol)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;
        firstLine.SecondCoordinate.X = coordinateUnmovableSymbol + _baseLineOffset;
        firstLine.SecondCoordinate.Y = coordinateSymbolOutgoing.Y;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.FirstCoordinate.Y = firstLine.SecondCoordinate.Y;
        secondLine.SecondCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.FirstCoordinate.Y = secondLine.SecondCoordinate.Y;
        thirdLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }

    private void SetCoordinatesIdenticalSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int coordinateUnmovableSymbol)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;
        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateUnmovableSymbol - _baseLineOffset;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.FirstCoordinate.Y = firstLine.SecondCoordinate.Y;
        secondLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        secondLine.SecondCoordinate.Y = firstLine.SecondCoordinate.Y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.FirstCoordinate.Y = secondLine.SecondCoordinate.Y;
        thirdLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }
}