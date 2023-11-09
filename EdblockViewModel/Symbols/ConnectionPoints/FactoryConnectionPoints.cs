using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class FactoryConnectionPoints
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbol;
    private readonly ConnectionPointModel connectionPointModel;

    public FactoryConnectionPoints(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbol)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbol = blockSymbol;
        connectionPointModel = new(_blockSymbol.BlockSymbolModel);
    }

    public List<ConnectionPoint> Create()
    {
        return new List<ConnectionPoint>()
        {
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, connectionPointModel.GetCoordinateLeft, PositionConnectionPoint.Left),
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, connectionPointModel.GetCoordinateRight, PositionConnectionPoint.Right),
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, connectionPointModel.GetCoordinateTop, PositionConnectionPoint.Top),
            new ConnectionPoint(_canvasSymbolsVM, _blockSymbol, connectionPointModel.GetCoordinateBottomCP, PositionConnectionPoint.Bottom)
        };
    }
}