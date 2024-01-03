using EdblockModel.SymbolsModel.Enum;
using EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbolsModel;

namespace EdblockModel.SymbolsModel.LineSymbolsModel.RedrawnLineSymbolsModel;

internal class RedrawnLineNoParallelSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BuilderCoordinateDecorator _builderCoordinateDecorator;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;
    private const int linesOneDifferentPositions = 2;
    private const int linesTwoDifferentPositions = 4;

    public RedrawnLineNoParallelSides(DrawnLineSymbolModel drawnLineSymbolModel, RedrawnLine redrawLine, int baseLineOffset)
    {
        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;
        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;

        _decoratedCoordinatesLines = redrawLine.DecoratedCoordinatesLines;

        _redrawLine = redrawLine;
        _baseLineOffset = baseLineOffset;

        _builderCoordinateDecorator = new BuilderCoordinateDecorator();

        if (_positionIncoming == PositionConnectionPoint.Bottom || _positionOutgoing == PositionConnectionPoint.Bottom)
        {
            _builderCoordinateDecorator = _builderCoordinateDecorator.SetInversionYCoordinate();
        }
    }

    public void RedrawLine((double x, double y) borderCoordinateOutgoingSymbol, (double x, double y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        (coordinateSymbolOutgoing, coordinateSymbolIncoming) =
                RedrawnLine.SetBuilderCoordinate(coordinateSymbolOutgoing, coordinateSymbolIncoming, _builderCoordinateDecorator);

        double horizontalOffsetLine = GetHorizontalOffsetLine();

        if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Right ||
            _positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Left ||
            _positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Left ||
            _positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Right)
        {
            ChangeLines(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else
        {
            ChangeLines(coordinateSymbolIncoming, coordinateSymbolOutgoing, horizontalOffsetLine);
            _redrawLine.ReverseCoordinateLine();
        }
    }

    private void ChangeLines(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, double horizontalOffsetLine)
    {
        if (coordinateSymbolOutgoing.Y < coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountLines(linesTwoDifferentPositions, _builderCoordinateDecorator);
            SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else
        {
            _redrawLine.ChangeCountLines(linesOneDifferentPositions, _builderCoordinateDecorator);
            SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    private double GetHorizontalOffsetLine()
    {
        if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Right ||
            _positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Top ||
            _positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Right ||
            _positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            double horizontalOffsetLine = _symbolOutgoingLine.XCoordinate - _baseLineOffset;

            return horizontalOffsetLine;
        }
        else
        {
            double horizontalOffsetLine = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _baseLineOffset;

            return horizontalOffsetLine;
        }
    }

    private void SetCoordinatesOneDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.CoordinateSymbolOutgoing.X = coordinateSymbolOutgoing.X;
        firstLine.CoordinateSymbolOutgoing.Y = coordinateSymbolOutgoing.Y;

        firstLine.CoordinateSymbolIncoming.X = coordinateSymbolOutgoing.X;
        firstLine.CoordinateSymbolIncoming.Y = coordinateSymbolIncoming.Y;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.CoordinateSymbolOutgoing.X = coordinateSymbolOutgoing.X;
        secondLine.CoordinateSymbolOutgoing.Y = coordinateSymbolIncoming.Y;

        secondLine.CoordinateSymbolIncoming.X = coordinateSymbolIncoming.X;
        secondLine.CoordinateSymbolIncoming.Y = coordinateSymbolIncoming.Y;
    }

    private void SetCoordinatesTwoDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, double horizontalOffsetLine)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.CoordinateSymbolOutgoing.X = coordinateSymbolOutgoing.X;
        firstLine.CoordinateSymbolOutgoing.Y = coordinateSymbolOutgoing.Y;

        firstLine.CoordinateSymbolIncoming.X = coordinateSymbolOutgoing.X;
        firstLine.CoordinateSymbolIncoming.Y = coordinateSymbolOutgoing.Y - _baseLineOffset;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.CoordinateSymbolOutgoing.X = firstLine.CoordinateSymbolIncoming.X;
        secondLine.CoordinateSymbolOutgoing.Y = firstLine.CoordinateSymbolIncoming.Y;

        secondLine.CoordinateSymbolIncoming.X = horizontalOffsetLine;
        secondLine.CoordinateSymbolIncoming.Y = firstLine.CoordinateSymbolIncoming.Y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.CoordinateSymbolOutgoing.X = secondLine.CoordinateSymbolIncoming.X;
        thirdLine.CoordinateSymbolOutgoing.Y = secondLine.CoordinateSymbolIncoming.Y;

        thirdLine.CoordinateSymbolIncoming.X = horizontalOffsetLine;
        thirdLine.CoordinateSymbolIncoming.Y = coordinateSymbolIncoming.Y;

        var fourtLine = _decoratedCoordinatesLines[3];

        fourtLine.CoordinateSymbolOutgoing.X = thirdLine.CoordinateSymbolIncoming.X;
        fourtLine.CoordinateSymbolOutgoing.Y = thirdLine.CoordinateSymbolIncoming.Y;

        fourtLine.CoordinateSymbolIncoming.X = coordinateSymbolIncoming.X;
        fourtLine.CoordinateSymbolIncoming.Y = coordinateSymbolIncoming.Y;
    }
}