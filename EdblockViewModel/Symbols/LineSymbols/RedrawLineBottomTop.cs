using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

internal class RedrawLineBottomTop
{
    private readonly BlockSymbol? _symbolaIncomingLine;
    private readonly BlockSymbol? _symbolOutgoingLine;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;
    private const int baseLineOffset = 20;

    public RedrawLineBottomTop(DrawnLineSymbolVM drawnLineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        _positionOutgoing = drawnLineSymbolVM.PositionOutgoingConnectionPoint;
        _positionIncoming = drawnLineSymbolVM.PositionIncomingConnectionPoint;
        _symbolaIncomingLine = drawnLineSymbolVM.SymbolaIncomingLine;
        _symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;
    }

    public void Redraw()
    {
        if (_symbolOutgoingLine == null || _symbolaIncomingLine == null)
        {
            return;
        }

        var borderCoordinateSymbolOutgoing = _symbolOutgoingLine.GetBorderCoordinate(_positionOutgoing);
        var borderCoordinateSymbolaIncoming = _symbolaIncomingLine.GetBorderCoordinate(_positionIncoming);

        if ((_positionOutgoing == PositionConnectionPoint.Top && _positionIncoming == PositionConnectionPoint.Bottom) ||
            (_positionOutgoing == PositionConnectionPoint.Bottom && _positionIncoming == PositionConnectionPoint.Top))
        {
            if (borderCoordinateSymbolOutgoing.x == borderCoordinateSymbolaIncoming.x)
            {
                SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
            else if (borderCoordinateSymbolOutgoing.y < borderCoordinateSymbolaIncoming.y)
            {
                SetCoordnateTreeLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming, new BuildCoordinateDecorator());
            }
            else
            {
                SetCoordnateFive(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming, new BuildCoordinateDecorator());
            }
        }
        else if ((_positionOutgoing == PositionConnectionPoint.Left && _positionIncoming == PositionConnectionPoint.Right) || 
            (_positionOutgoing == PositionConnectionPoint.Right && _positionIncoming == PositionConnectionPoint.Left))
        {
            if (borderCoordinateSymbolOutgoing.y == borderCoordinateSymbolaIncoming.y)
            {
                SetCoordnateOneLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
            }
            else if (borderCoordinateSymbolOutgoing.x < borderCoordinateSymbolaIncoming.x)
            {
                SetCoordnateTreeLine(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming, new BuildCoordinateDecorator().SetSwap());
            }
            else
            {
                SetCoordnateFive(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming, new BuildCoordinateDecorator().SetSwap());
            }
        }        
    }

    private void SetCoordnateOneLine((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    {
        var firstLine = _drawnLineSymbolVM.LineSymbols[^1];

        firstLine.X1 = borderCoordinateSymbolOutgoing.x;
        firstLine.Y1 = borderCoordinateSymbolOutgoing.y;
        
        firstLine.X2 = borderCoordinateSymbolaIncoming.x;
        firstLine.Y2 = borderCoordinateSymbolaIncoming.y;
    }

    private void SetCoordnateTreeLine((int x, int y) coordinateBorderSymbolOutgoing, (int x, int y) coordinateBorderSymbolIncoming, BuildCoordinateDecorator buildCoordinateDecorator)
    {
        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolOutgoing));
        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolIncoming));

        var linesCoordinates = new (Coordinate firstCoordinate, Coordinate secondCoordinate)[3];
        var linesCoordinatesDecorator = new (ICoordinateDecorator firstCoordinate, ICoordinateDecorator secondCoordinate)[3];

        for (int i = 0; i < 3; i++)
        {
            linesCoordinates[i] = (new Coordinate(), new Coordinate());
            linesCoordinatesDecorator[i] = (
                buildCoordinateDecorator.Create(linesCoordinates[i].firstCoordinate),
                buildCoordinateDecorator.Create(linesCoordinates[i].secondCoordinate)
                );
        }

        linesCoordinatesDecorator[0].firstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        linesCoordinatesDecorator[0].firstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y;
        linesCoordinatesDecorator[0].secondCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        linesCoordinatesDecorator[0].secondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;

        linesCoordinatesDecorator[1].firstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        linesCoordinatesDecorator[1].firstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;
        linesCoordinatesDecorator[1].secondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        linesCoordinatesDecorator[1].secondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;

        linesCoordinatesDecorator[2].firstCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        linesCoordinatesDecorator[2].firstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + (coordinateDecoratorSymbolIncoming.Y - coordinateDecoratorSymbolOutgoing.Y) / 2;
        linesCoordinatesDecorator[2].secondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        linesCoordinatesDecorator[2].secondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y;

        for (int i = 0; i < 3; i++)
        {
            var lineSymbol = _drawnLineSymbolVM.LineSymbols[i];
            lineSymbol.X1 = linesCoordinates[i].firstCoordinate.X;
            lineSymbol.Y1 = linesCoordinates[i].firstCoordinate.Y;
            lineSymbol.X2 = linesCoordinates[i].secondCoordinate.X;
            lineSymbol.Y2 = linesCoordinates[i].secondCoordinate.Y;
        }
    }

    private void SetCoordnateFive((int x, int y) coordinateBorderSymbolOutgoing1, (int x, int y) coordinateBorderSymbolIncoming1, BuildCoordinateDecorator buildCoordinateDecorator)
    {
        var coordinateDecoratorSymbolOutgoing = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolOutgoing1));
        var coordinateDecoratorSymbolIncoming = buildCoordinateDecorator.Create(new Coordinate(coordinateBorderSymbolIncoming1));

        var linesCoordinates = new (Coordinate firstCoordinate, Coordinate secondCoordinate)[5];
        var linesCoordinatesDecorator = new (ICoordinateDecorator firstCoordinate, ICoordinateDecorator secondCoordinate)[5];

        for (int i = 0; i < 5; i++)
        {
            linesCoordinates[i] = (new Coordinate(), new Coordinate());
            linesCoordinatesDecorator[i] = (
                buildCoordinateDecorator.Create(linesCoordinates[i].firstCoordinate),
                buildCoordinateDecorator.Create(linesCoordinates[i].secondCoordinate)
                );
        }

        linesCoordinatesDecorator[0].firstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        linesCoordinatesDecorator[0].firstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y;
        linesCoordinatesDecorator[0].secondCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        linesCoordinatesDecorator[0].secondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;

        linesCoordinatesDecorator[1].firstCoordinate.X = coordinateDecoratorSymbolOutgoing.X;
        linesCoordinatesDecorator[1].firstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;
        linesCoordinatesDecorator[1].secondCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 2;
        linesCoordinatesDecorator[1].secondCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;

        linesCoordinatesDecorator[2].firstCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 2;
        linesCoordinatesDecorator[2].firstCoordinate.Y = coordinateDecoratorSymbolOutgoing.Y + baseLineOffset;
        linesCoordinatesDecorator[2].secondCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 2;
        linesCoordinatesDecorator[2].secondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;

        linesCoordinatesDecorator[3].firstCoordinate.X = coordinateDecoratorSymbolOutgoing.X + (coordinateDecoratorSymbolIncoming.X - coordinateDecoratorSymbolOutgoing.X) / 2;
        linesCoordinatesDecorator[3].firstCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;
        linesCoordinatesDecorator[3].secondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        linesCoordinatesDecorator[3].secondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;

        linesCoordinatesDecorator[4].firstCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        linesCoordinatesDecorator[4].firstCoordinate.Y = coordinateDecoratorSymbolIncoming.Y - baseLineOffset;
        linesCoordinatesDecorator[4].secondCoordinate.X = coordinateDecoratorSymbolIncoming.X;
        linesCoordinatesDecorator[4].secondCoordinate.Y = coordinateDecoratorSymbolIncoming.Y;

        for (int i = 0; i < 5; i++)
        {
            var lineSymbol = _drawnLineSymbolVM.LineSymbols[i];
            lineSymbol.X1 = linesCoordinates[i].firstCoordinate.X;
            lineSymbol.Y1 = linesCoordinates[i].firstCoordinate.Y;
            lineSymbol.X2 = linesCoordinates[i].secondCoordinate.X;
            lineSymbol.Y2 = linesCoordinates[i].secondCoordinate.Y;
        }
    }
}