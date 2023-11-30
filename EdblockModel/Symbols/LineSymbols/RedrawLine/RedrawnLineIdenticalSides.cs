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
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Top)
        {
            _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
            _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides);

            if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
            {
                var coordinateUnmovableSymbol = (coordinateSymbolOutgoing.X, coordinateSymbolIncoming.Y - _baseLineOffset);
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateUnmovableSymbol);
            }
            else
            {
                var coordinateUnmovableSymbol = (coordinateSymbolOutgoing.X, coordinateSymbolOutgoing.Y - _baseLineOffset);
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateUnmovableSymbol);
            }
        }

        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();
            var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
            _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides);

            if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
            {
                var coordinateUnmovableSymbol = (coordinateSymbolOutgoing.X, coordinateSymbolOutgoing.Y - _baseLineOffset);
                SetCoordinatesIdenticalSides(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, coordinateUnmovableSymbol);
            }
            else
            {
                var coordinateUnmovableSymbol = (coordinateSymbolOutgoing.X, coordinateSymbolOutgoing.Y - _baseLineOffset);
                SetCoordinatesIdenticalSides(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, coordinateUnmovableSymbol);
            }
        }

        else if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Right)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator();
            coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
            _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

            if (borderCoordinateIncomingSymbol.x <= borderCoordinateOutgoingSymbol.x)
            {
                var coordinateUnmovableSymbol = (coordinateSymbolOutgoing.X + _baseLineOffset, coordinateSymbolOutgoing.Y);
                SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateUnmovableSymbol);
            }
            else
            {
                var coordinateUnmovableSymbol = (coordinateSymbolIncoming.X + _baseLineOffset, coordinateSymbolOutgoing.Y);
                SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateUnmovableSymbol);
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Left)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionXCoordinate();
            coordinateSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            coordinateSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            _redrawLine.ChangeCountLinesModel(linesIdenticalSides);
            _redrawLine.ChangeCountDecoratedLines(linesIdenticalSides, buildCoordinateDecorator);

            if (borderCoordinateIncomingSymbol.x > borderCoordinateOutgoingSymbol.x)
            {
                var coordinateUnmovableSymbol = (coordinateSymbolOutgoing.X + _baseLineOffset, coordinateSymbolOutgoing.Y);
                SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateUnmovableSymbol);
            }
            else
            {
                var coordinateUnmovableSymbol = (coordinateSymbolIncoming.X + _baseLineOffset, coordinateSymbolOutgoing.Y);
                SetCoordinatesIdenticalSides1(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateUnmovableSymbol);
            }
        }
    }

    private void SetCoordinatesIdenticalSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, (int x, int y) coordinateUnmovableSymbol)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;
        firstLine.SecondCoordinate.X = coordinateUnmovableSymbol.x;
        firstLine.SecondCoordinate.Y = coordinateUnmovableSymbol.y;

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

    private void SetCoordinatesIdenticalSides1(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, (int x, int y) coordinateUnmovableSymbol)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;
        firstLine.SecondCoordinate.X = coordinateUnmovableSymbol.x;
        firstLine.SecondCoordinate.Y = coordinateUnmovableSymbol.y;

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
}