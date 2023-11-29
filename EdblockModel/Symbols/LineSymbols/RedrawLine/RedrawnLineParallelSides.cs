using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineParallelSides
{
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolaIncomingLine;
    private readonly List<CoordinateLine> _coordinatesLines;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly List<LineSymbolModel> _linesSymbolModel;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesSamePositions = 1;
    private const int linesOneDifferentPositions = 3;
    private const int lLinesTwoDifferentPositions = 5;

    public RedrawnLineParallelSides(DrawnLineSymbolModel drawnLineSymbolModel, List<CoordinateLine> coordinatesLines, int baseLineOffset)
    {
        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolaIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        _coordinatesLines = coordinatesLines;

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        _linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;

        _decoratedCoordinatesLines = new();

        _baseLineOffset = baseLineOffset;
    }

    public List<LineSymbolModel>? GetRedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var widthlIncomingSymbol = _symbolaIncomingLine!.Width;

        if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top)
        {
            var coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
            var coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

            if (borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x && borderCoordinateOutgoingSymbol.y < borderCoordinateIncomingSymbol.y)
            { 
                RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, linesSamePositions);
                RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, _decoratedCoordinatesLines, linesOneDifferentPositions);
                SetCoordinatesSamePositions(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.y < borderCoordinateIncomingSymbol.y)
            {
                RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, linesOneDifferentPositions);
                RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, _decoratedCoordinatesLines, linesOneDifferentPositions);
                SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }
            else
            {
                RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, lLinesTwoDifferentPositions);
                RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, _decoratedCoordinatesLines, lLinesTwoDifferentPositions);
                SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, widthlIncomingSymbol);
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            var coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
            var coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

            if (borderCoordinateOutgoingSymbol.x == borderCoordinateIncomingSymbol.x && borderCoordinateOutgoingSymbol.y > borderCoordinateIncomingSymbol.y)
            {
                RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, linesSamePositions);
                RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, _decoratedCoordinatesLines, linesOneDifferentPositions);
                SetCoordinatesSamePositions(borderCoordinateOutgoingSymbol, borderCoordinateIncomingSymbol);
            }
            else if (borderCoordinateOutgoingSymbol.y > borderCoordinateIncomingSymbol.y)
            {
                RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, linesOneDifferentPositions);
                RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, _decoratedCoordinatesLines, linesOneDifferentPositions);
                SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }
            else
            {
                var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();

                RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, lLinesTwoDifferentPositions);
                RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, _decoratedCoordinatesLines, lLinesTwoDifferentPositions, buildCoordinateDecorator);
                SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, widthlIncomingSymbol);
            }
        }

        return _linesSymbolModel;
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