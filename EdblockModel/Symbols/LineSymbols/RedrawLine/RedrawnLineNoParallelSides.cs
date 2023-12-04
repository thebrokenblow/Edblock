using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;
using EdblockViewModel.Symbols.LineSymbols;
using System.Collections.Generic;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

internal class RedrawnLineNoParallelSides
{
    private readonly RedrawnLine _redrawLine;
    private readonly BlockSymbolModel _symbolOutgoingLine;
    private readonly BlockSymbolModel? _symbolaIncomingLine;
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

        _redrawLine = redrawLine;

        _baseLineOffset = baseLineOffset;
    }

    public void RedrawLine()
    {
        var borderCoordinateOutgoingSymbol = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateIncomingSymbol = _symbolaIncomingLine!.GetBorderCoordinate(_positionIncoming);

        ICoordinateDecorator coordinateSymbolOutgoing = new CoordinateDecorator(borderCoordinateOutgoingSymbol);
        ICoordinateDecorator coordinateSymbolIncoming = new CoordinateDecorator(borderCoordinateIncomingSymbol);

        if (_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Right)
        {
            if (borderCoordinateOutgoingSymbol.y < borderCoordinateIncomingSymbol.y)
            {
                _redrawLine.ChangeCountDecoratedLines(4);
                SetCoordinateLine1(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }
            else
            {
                _redrawLine.ChangeCountDecoratedLines(2);
                SetCoordinateLine(coordinateSymbolOutgoing, coordinateSymbolIncoming);
            }

            _redrawLine.SetCoordinateLineModel();

        }

        if (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Top)
        {
            if (borderCoordinateOutgoingSymbol.y > borderCoordinateIncomingSymbol.y)
            {
                _redrawLine.ChangeCountDecoratedLines(4);
                SetCoordinateLine1(coordinateSymbolIncoming, coordinateSymbolOutgoing);
            }
            else
            {
                _redrawLine.ChangeCountDecoratedLines(2);
                SetCoordinateLine(coordinateSymbolIncoming, coordinateSymbolOutgoing);
            }

            _redrawLine.Reverse();
            _redrawLine.SetCoordinateLineModel();
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

    private void SetCoordinateLine1(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        int horizontalOffsetLine = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _baseLineOffset;

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