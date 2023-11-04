using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.LineSymbols;

internal class RedrawLineSymbol
{
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private RedrawLineBottomTop redrawLineBottomTop;
    private readonly PositionConnectionPoint _positionOutgoing;
    private readonly PositionConnectionPoint _positionIncoming;

    public RedrawLineSymbol(DrawnLineSymbolVM drawnLineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        redrawLineBottomTop = new(drawnLineSymbolVM);
        _positionOutgoing = drawnLineSymbolVM.PositionOutgoingConnectionPoint;
        _positionIncoming = drawnLineSymbolVM.PositionIncomingConnectionPoint;  
    }

    public void Redraw()
    {
        redrawLineBottomTop.Redraw();
        var lastLine = _drawnLineSymbolVM.LineSymbols[^1];
        _drawnLineSymbolVM.ArrowSymbol.ChangeOrientationArrow((lastLine.X2, lastLine.Y2), _positionIncoming);
    }
}