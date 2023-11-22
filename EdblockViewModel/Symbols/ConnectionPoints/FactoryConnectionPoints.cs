using EdblockModel.Symbols.Enum;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class FactoryConnectionPoints
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbol;
    private readonly CoordinateConnectionPoint coordinateConnectionPoint;

    public FactoryConnectionPoints(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbol)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbol = blockSymbol;
        coordinateConnectionPoint = new(_blockSymbol);
    }

    public List<ConnectionPoint> Create()
    {
        return new List<ConnectionPoint>()
        {
            new ConnectionPoint(
                _canvasSymbolsVM, 
                _blockSymbol, 
                coordinateConnectionPoint.GetCoordinateLeft, 
                PositionConnectionPoint.Left),

            new ConnectionPoint(
                _canvasSymbolsVM, 
                _blockSymbol, 
                coordinateConnectionPoint.GetCoordinateRight, 
                PositionConnectionPoint.Right),

            new ConnectionPoint(
                _canvasSymbolsVM, 
                _blockSymbol, 
                coordinateConnectionPoint.GetCoordinateTop, 
                PositionConnectionPoint.Top),

            new ConnectionPoint(
                _canvasSymbolsVM, 
                _blockSymbol, 
                coordinateConnectionPoint.GetCoordinateBottom, 
                PositionConnectionPoint.Bottom)
        };
    }
}