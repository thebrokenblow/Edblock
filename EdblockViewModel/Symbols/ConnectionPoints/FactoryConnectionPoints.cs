using System;
using EdblockModel.Symbols.Enum;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class FactoryConnectionPoints
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbol;
    private readonly CoordinateConnectionPoint coordinateConnectionPoint;
    private readonly Dictionary<PositionConnectionPoint, Func<PositionConnectionPoint, ConnectionPoint>> instanceConnectionPointByPosition;

    public FactoryConnectionPoints(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbol)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbol = blockSymbol;

        instanceConnectionPointByPosition = new()
        {
             { PositionConnectionPoint.Top, _ => CreateTop() },
             { PositionConnectionPoint.Right, _ => CreateRight() },
             { PositionConnectionPoint.Bottom, _ => CreateBottom() },
             { PositionConnectionPoint.Left, _ => CreateLeft() },
        };

        coordinateConnectionPoint = new(_blockSymbol);
    }

    public List<ConnectionPoint> CreateConnectionPoints()
    {
        var topConnectionPoint = CreateTop();
        var rightConnectionPoint = CreateRight();
        var bottomConnectionPoint = CreateBottom();
        var leftConnectionPoint = CreateLeft();

        var connectionPoints = new List<ConnectionPoint>()
        {
            topConnectionPoint,
            rightConnectionPoint,
            bottomConnectionPoint,
            leftConnectionPoint,
        };

        return connectionPoints;
    }

    public ConnectionPoint CreateConnectionPoint(PositionConnectionPoint positionConnectionPoint)
    {
        var connectionPoint = instanceConnectionPointByPosition[positionConnectionPoint].Invoke(positionConnectionPoint);

        return connectionPoint;
    }

    private ConnectionPoint CreateTop()
    {
        var topConnectionPoint = new ConnectionPoint(
            _canvasSymbolsVM,
            _blockSymbol,
            coordinateConnectionPoint.GetCoordinateTop,
            PositionConnectionPoint.Top);

        return topConnectionPoint;
    }

    private ConnectionPoint CreateRight()
    {
        var rightConnectionPoint = new ConnectionPoint(
            _canvasSymbolsVM,
            _blockSymbol,
            coordinateConnectionPoint.GetCoordinateRight,
            PositionConnectionPoint.Right);

        return rightConnectionPoint;
    }

    private ConnectionPoint CreateBottom()
    {
        var bottomConnectionPoint = new ConnectionPoint(
            _canvasSymbolsVM,
            _blockSymbol,
            coordinateConnectionPoint.GetCoordinateBottom,
            PositionConnectionPoint.Bottom);

        return bottomConnectionPoint;
    }

    private ConnectionPoint CreateLeft()
    {
        var bottomConnectionPoint = new ConnectionPoint(
            _canvasSymbolsVM,
            _blockSymbol,
            coordinateConnectionPoint.GetCoordinateLeft,
            PositionConnectionPoint.Left);

        return bottomConnectionPoint;
    }
}