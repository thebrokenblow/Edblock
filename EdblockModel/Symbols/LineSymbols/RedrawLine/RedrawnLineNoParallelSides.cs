using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineNoParallelSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BuilderCoordinateDecorator builderCoordinateDecorator;
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
       
        builderCoordinateDecorator = new BuilderCoordinateDecorator();

        if (_positionIncoming == PositionConnectionPoint.Bottom || _positionOutgoing == PositionConnectionPoint.Bottom)
        {
            builderCoordinateDecorator = builderCoordinateDecorator.SetInversionYCoordinate();
        }
    }

    public void RedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        coordinateSymbolOutgoing = builderCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = builderCoordinateDecorator.Build(coordinateSymbolIncoming);

        int horizontalOffsetLine = GetHorizontalOffsetLine();

        if ((_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Left) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Left))
        {
            RedrawTopToRightSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else
        {
            RedrawTopToRightSides(coordinateSymbolIncoming, coordinateSymbolOutgoing, horizontalOffsetLine);
            _redrawLine.ReverseCoordinateLine();
        }
    }

    private void RedrawTopToRightSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int horizontalOffsetLine)
    {
        if (coordinateSymbolOutgoing.Y < coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountDecoratedLines(linesTwoDifferentPositions, builderCoordinateDecorator);
            SetCoordinatesTwoDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else
        {
            _redrawLine.ChangeCountDecoratedLines(linesOneDifferentPositions, builderCoordinateDecorator);
            SetCoordinatesOneDifferentPositions(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    private int GetHorizontalOffsetLine()
    {
        if ((_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Top) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Right) ||
            (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Bottom))
        {
            int horizontalOffsetLine = _symbolOutgoingLine.XCoordinate - _baseLineOffset;

            return horizontalOffsetLine;
        }
        else
        {
            int horizontalOffsetLine = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _baseLineOffset;

            return horizontalOffsetLine;
        }
    }

    private void SetCoordinatesOneDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        secondLine.FirstCoordinate.Y = coordinateSymbolIncoming.Y;

        secondLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        secondLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }

    private void SetCoordinatesTwoDifferentPositions(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int horizontalOffsetLine)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateSymbolOutgoing.Y - _baseLineOffset;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = firstLine.SecondCoordinate.X;
        secondLine.FirstCoordinate.Y = firstLine.SecondCoordinate.Y;

        secondLine.SecondCoordinate.X = horizontalOffsetLine;
        secondLine.SecondCoordinate.Y = firstLine.SecondCoordinate.Y;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = secondLine.SecondCoordinate.X;
        thirdLine.FirstCoordinate.Y = secondLine.SecondCoordinate.Y;

        thirdLine.SecondCoordinate.X = horizontalOffsetLine;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;

        var fourtLine = _decoratedCoordinatesLines[3];

        fourtLine.FirstCoordinate.X = thirdLine.SecondCoordinate.X;
        fourtLine.FirstCoordinate.Y = thirdLine.SecondCoordinate.Y;

        fourtLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        fourtLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }
}