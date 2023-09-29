using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class FactoryConnectionPoints
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbol _blockSymbol;
    private readonly CoordinateConnectionPoint coordinateConnectionPoint;
    public FactoryConnectionPoints(CanvasSymbolsVM canvasSymbolsVM, BlockSymbol blockSymbol)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbol = blockSymbol;
        coordinateConnectionPoint = new(_blockSymbol);
    }

    public List<ConnectionPoint> Create()
    {
        return new List<ConnectionPoint>()
        {
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, coordinateConnectionPoint.GetCoordinateTopCP, OrientationConnectionPoint.Vertical),
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, coordinateConnectionPoint.GetCoordinateRightCP, OrientationConnectionPoint.Horizontal),
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, coordinateConnectionPoint.GetCoordinateBottomCP, OrientationConnectionPoint.Vertical),
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, coordinateConnectionPoint.GetCoordinateLeftCP, OrientationConnectionPoint.Horizontal)
        };
    }
}
