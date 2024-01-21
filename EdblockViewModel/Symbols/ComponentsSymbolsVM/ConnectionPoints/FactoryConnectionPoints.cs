using System;
using System.Collections.Generic;
using EdblockModel.Enum;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.ConnectionPoints;

public class FactoryConnectionPoints
{
    private readonly BlockSymbolVM _blockSymbolCM;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CheckBoxLineGostVM _checkBoxLineGostVM;
    private readonly CoordinateConnectionPoint coordinateConnectionPoint;
    private readonly Dictionary<SideSymbol, Func<SideSymbol, ConnectionPointVM>> instanceConnectionPointByPosition;

    public FactoryConnectionPoints(CanvasSymbolsVM canvasSymbolsVM, CheckBoxLineGostVM checkBoxLineGostVM, BlockSymbolVM blockSymbolVM)
    {
        _blockSymbolCM = blockSymbolVM;
        _canvasSymbolsVM = canvasSymbolsVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;

        instanceConnectionPointByPosition = new()
        {
             { SideSymbol.Top, _ => CreateTop() },
             { SideSymbol.Right, _ => CreateRight() },
             { SideSymbol.Bottom, _ => CreateBottom() },
             { SideSymbol.Left, _ => CreateLeft() },
        };

        coordinateConnectionPoint = new(blockSymbolVM);
    }

    public List<ConnectionPointVM> CreateConnectionPoints()
    {
        var topConnectionPoint = CreateTop();
        var rightConnectionPoint = CreateRight();
        var bottomConnectionPoint = CreateBottom();
        var leftConnectionPoint = CreateLeft();

        var connectionPoints = new List<ConnectionPointVM>()
        {
            topConnectionPoint,
            rightConnectionPoint,
            bottomConnectionPoint,
            leftConnectionPoint,
        };

        return connectionPoints;
    }

    public ConnectionPointVM CreateConnectionPoint(SideSymbol positionConnectionPoint)
    {
        var connectionPoint = instanceConnectionPointByPosition[positionConnectionPoint].Invoke(positionConnectionPoint);

        return connectionPoint;
    }

    private ConnectionPointVM CreateTop()
    {
        var topConnectionPoint = new ConnectionPointVM(
            _canvasSymbolsVM,
            _blockSymbolCM,
            _checkBoxLineGostVM,
            coordinateConnectionPoint.GetCoordinateTop,
            SideSymbol.Top);

        return topConnectionPoint;
    }

    private ConnectionPointVM CreateRight()
    {
        var rightConnectionPoint = new ConnectionPointVM(
            _canvasSymbolsVM,
            _blockSymbolCM,
            _checkBoxLineGostVM,
            coordinateConnectionPoint.GetCoordinateRight,
            SideSymbol.Right);

        return rightConnectionPoint;
    }

    private ConnectionPointVM CreateBottom()
    {
        var bottomConnectionPoint = new ConnectionPointVM(
            _canvasSymbolsVM,
            _blockSymbolCM,
            _checkBoxLineGostVM,
            coordinateConnectionPoint.GetCoordinateBottom,
            SideSymbol.Bottom);

        return bottomConnectionPoint;
    }

    private ConnectionPointVM CreateLeft()
    {
        var bottomConnectionPoint = new ConnectionPointVM(
            _canvasSymbolsVM,
            _blockSymbolCM,
            _checkBoxLineGostVM,
            coordinateConnectionPoint.GetCoordinateLeft,
            SideSymbol.Left);

        return bottomConnectionPoint;
    }
}