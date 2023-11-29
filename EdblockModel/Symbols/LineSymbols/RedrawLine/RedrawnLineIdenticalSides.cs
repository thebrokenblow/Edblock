using EdblockModel.Symbols.Enum;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineIdenticalSides
{
    private readonly List<CoordinateLine> _coordinatesLines;
    private readonly List<CoordinateLine> decoratedCoordinatesLines;
    private readonly List<LineSymbolModel> _linesSymbolModel;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesIdenticalSides = 3;

    public RedrawnLineIdenticalSides(DrawnLineSymbolModel drawnLineSymbolModel, List<CoordinateLine> coordinatesLines, int baseLineOffset)
    {
        _coordinatesLines = coordinatesLines;

        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        _linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;

        decoratedCoordinatesLines = new();

        _baseLineOffset = baseLineOffset;
    }

    public List<LineSymbolModel>? GetRedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        var coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        var coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Top)
        {
            RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, linesIdenticalSides);
            RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, decoratedCoordinatesLines, linesIdenticalSides);

            if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
            {
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolIncoming.Y);
            }
            else
            {
                SetCoordinatesIdenticalSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, coordinateSymbolOutgoing.Y);
            }
        }
        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            var buildCoordinateDecorator = new BuilderCoordinateDecorator().SetInversionYCoordinate();
            var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Build(coordinateSymbolOutgoing);
            var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Build(coordinateSymbolIncoming);

            RedrawnLine.ChangeCountLinesModel(_linesSymbolModel, linesIdenticalSides);
            RedrawnLine.ChangeCountDecoratedLines(_coordinatesLines, decoratedCoordinatesLines, linesIdenticalSides);

            if (borderCoordinateIncomingSymbol.y <= borderCoordinateOutgoingSymbol.y)
            {
                SetCoordinatesIdenticalSides(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, coordinateSymbolOutgoing.Y);
            }
            else
            {
                SetCoordinatesIdenticalSides(coordinateDecoratorSymbolOutgoing, coordinateDecoratorSymbolIncoming, coordinateSymbolIncoming.Y);
            }

        }

        return _linesSymbolModel;
    }

    private void SetCoordinatesIdenticalSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int coordinateUnmovableSymbol)
    {
        var firstLine = decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;
        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateUnmovableSymbol - _baseLineOffset;

        var secondLine = decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.FirstCoordinate.Y = firstLine.SecondCoordinate.Y;
        secondLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        secondLine.SecondCoordinate.Y = firstLine.SecondCoordinate.Y;

        var thirdLine = decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.FirstCoordinate.Y = secondLine.SecondCoordinate.Y;
        thirdLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }
}
