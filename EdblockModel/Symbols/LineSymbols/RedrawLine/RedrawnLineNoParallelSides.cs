using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineNoParallelSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolaIncomingLine;
    private readonly BuilderCoordinateDecorator builderCoordinateDecorator;
    private readonly List<CoordinateLine> _decoratedCoordinatesLines;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private readonly int _baseLineOffset;

    public RedrawnLineNoParallelSides(DrawnLineSymbolModel drawnLineSymbolModel, RedrawnLine redrawLine, int baseLineOffset)
    {
        _positionOutgoing = drawnLineSymbolModel.OutgoingPosition;
        _positionIncoming = drawnLineSymbolModel.IncomingPosition;

        _symbolOutgoingLine = drawnLineSymbolModel.SymbolOutgoingLine;
        _symbolaIncomingLine = drawnLineSymbolModel.SymbolIncomingLine;

        _decoratedCoordinatesLines = redrawLine.DecoratedCoordinatesLines;

        builderCoordinateDecorator = new BuilderCoordinateDecorator();

        if (_positionIncoming == PositionConnectionPoint.Bottom || _positionOutgoing == PositionConnectionPoint.Bottom)
        {
            builderCoordinateDecorator = builderCoordinateDecorator.SetInversionYCoordinate();
        }

        _redrawLine = redrawLine;

        _baseLineOffset = baseLineOffset;
    }

    private void RedrawTopToRightSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int horizontalOffsetLine)
    {
        if (coordinateSymbolOutgoing.Y < coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountDecoratedLines(4, builderCoordinateDecorator);
            SetCoordinateLine1(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else
        {
            _redrawLine.ChangeCountDecoratedLines(2, builderCoordinateDecorator);
            SetCoordinateLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    private void RedrawLeftToBottomSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int horizontalOffsetLine)
    {
        if (coordinateSymbolOutgoing.Y > coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountDecoratedLines(4, builderCoordinateDecorator);
            SetCoordinateLine1(coordinateSymbolIncoming, coordinateSymbolOutgoing, horizontalOffsetLine);
        }
        else
        {
            _redrawLine.ChangeCountDecoratedLines(2, builderCoordinateDecorator);
            SetCoordinateLine(coordinateSymbolIncoming, coordinateSymbolOutgoing);
        }

        _redrawLine.ReverseCoordinateLine();
    }

    private void RedrawRightToTopSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int horizontalOffsetLine)
    {
        if (coordinateSymbolOutgoing.Y > coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountDecoratedLines(4, builderCoordinateDecorator);
            SetCoordinateLine1(coordinateSymbolIncoming, coordinateSymbolOutgoing, horizontalOffsetLine);
        }
        else
        {
            _redrawLine.ChangeCountDecoratedLines(2, builderCoordinateDecorator);
            SetCoordinateLine(coordinateSymbolIncoming, coordinateSymbolOutgoing);
        }

        _redrawLine.ReverseCoordinateLine();
    }

    private void RedrawTopToLeftSides(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int horizontalOffsetLine)
    {
        if (coordinateSymbolOutgoing.Y < coordinateSymbolIncoming.Y)
        {
            _redrawLine.ChangeCountDecoratedLines(4, builderCoordinateDecorator);
            SetCoordinateLine1(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else
        {
            _redrawLine.ChangeCountDecoratedLines(2, builderCoordinateDecorator);
            SetCoordinateLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);
        }
    }

    public void RedrawLine((int x, int y) borderCoordinateOutgoingSymbol, (int x, int y) borderCoordinateIncomingSymbol)
    {
        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        coordinateSymbolOutgoing = builderCoordinateDecorator.Build(coordinateSymbolOutgoing);
        coordinateSymbolIncoming = builderCoordinateDecorator.Build(coordinateSymbolIncoming);

        int horizontalOffsetLine = _symbolOutgoingLine.XCoordinate;

        if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Right)
        {
            horizontalOffsetLine -= _baseLineOffset;
            RedrawTopToRightSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Top)
        {
            horizontalOffsetLine += _symbolOutgoingLine.Width + _baseLineOffset;
            RedrawRightToTopSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Top)
        {
            horizontalOffsetLine -= _baseLineOffset;
            RedrawLeftToBottomSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Left)
        {
            horizontalOffsetLine += _symbolOutgoingLine.Width + _baseLineOffset;
            RedrawTopToLeftSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Right)
        {
            horizontalOffsetLine -= _baseLineOffset;
            RedrawTopToRightSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Left)
        {
            horizontalOffsetLine += _symbolOutgoingLine.Width + _baseLineOffset;
            RedrawTopToLeftSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            horizontalOffsetLine += _symbolOutgoingLine.Width + _baseLineOffset;
            RedrawRightToTopSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
        else if (_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Bottom)
        {
            horizontalOffsetLine -= _baseLineOffset;
            RedrawLeftToBottomSides(coordinateSymbolOutgoing, coordinateSymbolIncoming, horizontalOffsetLine);
        }
    }

    private void SetCoordinateLine(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
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

    private void SetCoordinateLine1(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming, int horizontalOffsetLine)
    {
        var firstLine = _decoratedCoordinatesLines[0];

        firstLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y;

        firstLine.SecondCoordinate.X = coordinateSymbolOutgoing.X;
        firstLine.SecondCoordinate.Y = coordinateSymbolOutgoing.Y - _baseLineOffset;

        var secondLine = _decoratedCoordinatesLines[1];

        secondLine.FirstCoordinate.X = coordinateSymbolOutgoing.X;
        secondLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y - _baseLineOffset;

        secondLine.SecondCoordinate.X = horizontalOffsetLine;
        secondLine.SecondCoordinate.Y = coordinateSymbolOutgoing.Y - _baseLineOffset;

        var thirdLine = _decoratedCoordinatesLines[2];

        thirdLine.FirstCoordinate.X = horizontalOffsetLine;
        thirdLine.FirstCoordinate.Y = coordinateSymbolOutgoing.Y - _baseLineOffset;

        thirdLine.SecondCoordinate.X = horizontalOffsetLine;
        thirdLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;

        var fourtLine = _decoratedCoordinatesLines[3];

        fourtLine.FirstCoordinate.X = horizontalOffsetLine;
        fourtLine.FirstCoordinate.Y = coordinateSymbolIncoming.Y;

        fourtLine.SecondCoordinate.X = coordinateSymbolIncoming.X;
        fourtLine.SecondCoordinate.Y = coordinateSymbolIncoming.Y;
    }
}