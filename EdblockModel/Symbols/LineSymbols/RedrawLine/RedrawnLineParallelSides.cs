using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineParallelSides
{
    private readonly BlockSymbolModel? _symbolaIncomingLine;
    private readonly RedrawnLine _redrawLine;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesSamePositions = 1;
    private const int linesOneDifferentPositions = 3;
    private const int linesTwoDifferentPositions = 5;

    public RedrawnLineParallelSides(DrawnLineSymbolModel drawnLineSymbolModel, RedrawnLine redrawLine, int baseLineOffset)
    {
        _symbolaIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        _redrawLine = redrawLine;
        _decoratedCoordinatesLines = redrawLine.DecoratedCoordinatesLines;

        _baseLineOffset = baseLineOffset;
    }

    public void RedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var widthlIncomingSymbol = _symbolaIncomingLine!.Width;

        if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top)
        {
            var coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
            var coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

            if (borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x && borderCoordinateOutgoingSymbol.y < borderCoordinateIncomingSymbol.y)
            {
                _redrawLine.ChangeCountLinesModel(linesSamePositions);
                _redrawLine.ChangeCountDecoratedLines(linesSamePositions);
                SetCoordinatesSamePositions(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.y < borderCoordinateIncomingSymbol.y)
            {
                _redrawLine.ChangeCountLinesModel(linesOneDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions);
                SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }
            else
            {
                _redrawLine.ChangeCountLinesModel(linesTwoDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions);
                SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, widthlIncomingSymbol);
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
            ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

            if (borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x && borderCoordinateOutgoingSymbol.y > borderCoordinateIncomingSymbol.y)
            {
                _redrawLine.ChangeCountLinesModel(linesSamePositions);
                _redrawLine.ChangeCountDecoratedLines(linesSamePositions);
                SetCoordinatesSamePositions(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.y > borderCoordinateIncomingSymbol.y)
            {
                _redrawLine.ChangeCountLinesModel(linesOneDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions);
                SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }
            else
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();
                coordinateSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                coordinateSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                _redrawLine.ChangeCountLinesModel(linesTwoDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions, buildCoordinateDecorator);
                SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, widthlIncomingSymbol);
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left)
        {
            if (borderCoordinateOutgoingSymbol.y == borderCoordinateIncomingSymbol.y && borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x)
            {
                _redrawLine.ChangeCountLinesModel(linesSamePositions);
                _redrawLine.ChangeCountDecoratedLines(linesSamePositions);
                SetCoordinatesSamePositions(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.x > borderCoordinateIncomingSymbol.x)
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap().SetInversionYCoordinate();
                var coordinateSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                _redrawLine.ChangeCountLinesModel(linesTwoDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions, buildCoordinateDecorator);

                SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, _symbolaIncomingLine.Height);
            }
            else
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap();
                var coordinateSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                _redrawLine.ChangeCountLinesModel(linesOneDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions, buildCoordinateDecorator);

                SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right)
        {
            if (borderCoordinateOutgoingSymbol.y == borderCoordinateIncomingSymbol.y && borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x)
            {
                _redrawLine.ChangeCountLinesModel(linesSamePositions);
                _redrawLine.ChangeCountDecoratedLines(linesSamePositions);
                SetCoordinatesSamePositions(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.x < borderCoordinateIncomingSymbol.x)
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap().SetInversionXCoordinate();
                var coordinateSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                _redrawLine.ChangeCountLinesModel(linesTwoDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions, buildCoordinateDecorator);

                SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, _symbolaIncomingLine.Height);
            }
            else
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap();
                var coordinateSymbolOutgoing = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateOutgoingSymbol));
                var coordinateSymbolIncoming = buildCoordinateDecorator.Build(new CoordinateDecorator(borderCoordinateIncomingSymbol));

                _redrawLine.ChangeCountLinesModel(linesOneDifferentPositions);
                _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions, buildCoordinateDecorator);

                SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }
        }
    }

    private void SetCoordinatesSamePositions((int x, int y) coordinateSymbolOutgoing, (int x, int y) coordinateSymbolaIncoming)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.x;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.y;
        firstLine.SecondCoordinate.X = coordinateSymbolaIncoming.x;
        firstLine.SecondCoordinate.Y = coordinateSymbolaIncoming.y;
    }

    private void SetCoordinatesOneDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;
        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateSymbolOutgoing.Y + (coordinateSymbolIncoming.Y - coordinateSymbolOutgoing.Y) / 2;

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

        var fourthLine = _decoratedCoordinatesLines[3];

        fourthLine.FirstCoordinate.X = thirdLine.SecondCoordinate.X;
        fourthLine.FirstCoordinate.Y = thirdLine.SecondCoordinate.Y;
        fourthLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        fourthLine.SecondCoordinate.Y = thirdLine.SecondCoordinate.Y;

        var fifthLine = _decoratedCoordinatesLines[4];

        fifthLine.FirstCoordinate.X = fourthLine.SecondCoordinate.X;
        fifthLine.FirstCoordinate.Y = fourthLine.SecondCoordinate.Y;
        fifthLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        fifthLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }
}