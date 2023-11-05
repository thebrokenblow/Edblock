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
            else if (borderCoordinateSymbolOutgoing.y != borderCoordinateSymbolaIncoming.y)
            {
                SetCoordnateTreeLine1(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
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
                SetCoordnateTreeLine2(borderCoordinateSymbolOutgoing, borderCoordinateSymbolaIncoming);
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

    private void InversionCoordinates(ref (int x, int y) borderCoordinateSymbolOutgoing, ref (int x, int y) borderCoordinateSymbolaIncoming)
    {
        (borderCoordinateSymbolOutgoing.x, borderCoordinateSymbolOutgoing.y) = (borderCoordinateSymbolOutgoing.y, borderCoordinateSymbolOutgoing.x);
        (borderCoordinateSymbolaIncoming.x, borderCoordinateSymbolaIncoming.y) = (borderCoordinateSymbolaIncoming.y, borderCoordinateSymbolaIncoming.x);
    }

    private void SetCoordnateTreeLine1((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    {
        var firstLine = _drawnLineSymbolVM.LineSymbols[^3];

        firstLine.X1 = borderCoordinateSymbolOutgoing.x;
        firstLine.Y1 = borderCoordinateSymbolOutgoing.y;
        firstLine.X2 = borderCoordinateSymbolOutgoing.x;
        firstLine.Y2 = borderCoordinateSymbolOutgoing.y + (borderCoordinateSymbolaIncoming.y - borderCoordinateSymbolOutgoing.y) / 2;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^2];

        secondLine.X1 = borderCoordinateSymbolOutgoing.x;
        secondLine.Y1 = borderCoordinateSymbolOutgoing.y + (borderCoordinateSymbolaIncoming.y - borderCoordinateSymbolOutgoing.y) / 2;
        secondLine.X2 = borderCoordinateSymbolaIncoming.x;
        secondLine.Y2 = borderCoordinateSymbolOutgoing.y + (borderCoordinateSymbolaIncoming.y - borderCoordinateSymbolOutgoing.y) / 2;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^1];

        thirdLine.X1 = borderCoordinateSymbolaIncoming.x;
        thirdLine.Y1 = borderCoordinateSymbolOutgoing.y + (borderCoordinateSymbolaIncoming.y - borderCoordinateSymbolOutgoing.y) / 2;
        thirdLine.X2 = borderCoordinateSymbolaIncoming.x;
        thirdLine.Y2 = borderCoordinateSymbolaIncoming.y;
    }


    private void SetCoordnateTreeLine2((int x, int y) borderCoordinateSymbolOutgoing, (int x, int y) borderCoordinateSymbolaIncoming)
    {
        var firstLine = _drawnLineSymbolVM.LineSymbols[^3];
        firstLine.X1 = borderCoordinateSymbolOutgoing.x;
        firstLine.Y1 = borderCoordinateSymbolOutgoing.y;
        firstLine.X2 = borderCoordinateSymbolOutgoing.x + (borderCoordinateSymbolaIncoming.x - borderCoordinateSymbolOutgoing.x) / 2;
        firstLine.Y2 = borderCoordinateSymbolOutgoing.y;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^2];

        secondLine.X1 = borderCoordinateSymbolOutgoing.x + (borderCoordinateSymbolaIncoming.x - borderCoordinateSymbolOutgoing.x) / 2;
        secondLine.Y1 = borderCoordinateSymbolOutgoing.y;
        secondLine.X2 = borderCoordinateSymbolOutgoing.x + (borderCoordinateSymbolaIncoming.x - borderCoordinateSymbolOutgoing.x) / 2;
        secondLine.Y2 = borderCoordinateSymbolaIncoming.y;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^1];

        thirdLine.X1 = borderCoordinateSymbolOutgoing.x + (borderCoordinateSymbolaIncoming.x - borderCoordinateSymbolOutgoing.x) / 2;
        thirdLine.Y1 = borderCoordinateSymbolaIncoming.y;
        thirdLine.X2 = borderCoordinateSymbolaIncoming.x;
        thirdLine.Y2 = borderCoordinateSymbolaIncoming.y;
    }
}