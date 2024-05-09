using System;
using System.Collections.Generic;
using Edblock.SymbolsModel.EnumsModel;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class CoordinateConnectionPointVM
{
    private readonly BlockSymbolVM _blockSymbolVM;
    private readonly List<ConnectionPointVM> сonnectionPointsVM;
    private const int offsetPositionConnectionPoint = 15;
    private readonly Dictionary<SideSymbol, Action<ConnectionPointVM>> sideSymbolByCoordinate;

    public CoordinateConnectionPointVM(BlockSymbolVM blockSymbolVM)
    {
        _blockSymbolVM = blockSymbolVM;

        var blockSymbolHasConnectionPoint = (IHasConnectionPoint)blockSymbolVM;
        сonnectionPointsVM = blockSymbolHasConnectionPoint.ConnectionPointsVM;

        sideSymbolByCoordinate = new()
        {
            { SideSymbol.Top, SetCoordinateTopConnectionPoint },
            { SideSymbol.Right, SetCoordinateRightConnectionPoint },
            { SideSymbol.Bottom, SetCoordinateBottomConnectionPoint },
            { SideSymbol.Left, SetCoordinateLeftConnectionPoint }
        };
    }

    public void SetCoordinate()
    {
        foreach (var сonnectionPointVM in сonnectionPointsVM)
        {
            var position = сonnectionPointVM.Position;
            sideSymbolByCoordinate[position].Invoke(сonnectionPointVM);
        }
    }

    private void SetCoordinateTopConnectionPoint(ConnectionPointVM topConnectionPoint)
    {
        var xCoordinate = _blockSymbolVM.Width / 2;

        topConnectionPoint.XCoordinate = xCoordinate;
        topConnectionPoint.YCoordinate = -offsetPositionConnectionPoint;

        topConnectionPoint.XCoordinateLineDraw = xCoordinate;
        topConnectionPoint.YCoordinateLineDraw = 0;
    }

    private void SetCoordinateRightConnectionPoint(ConnectionPointVM rightConnectionPoint)
    {
        var yCoordinate = _blockSymbolVM.Height / 2;

        rightConnectionPoint.XCoordinate = _blockSymbolVM.Width + offsetPositionConnectionPoint;
        rightConnectionPoint.YCoordinate = yCoordinate;

        rightConnectionPoint.XCoordinateLineDraw = _blockSymbolVM.Width;
        rightConnectionPoint.YCoordinateLineDraw = yCoordinate;
    }

    private void SetCoordinateBottomConnectionPoint(ConnectionPointVM bottomConnectionPoint)
    {
        var xCoordinate = _blockSymbolVM.Width / 2;

        bottomConnectionPoint.XCoordinate = xCoordinate;
        bottomConnectionPoint.YCoordinate = _blockSymbolVM.Height + offsetPositionConnectionPoint;

        bottomConnectionPoint.XCoordinateLineDraw = xCoordinate;
        bottomConnectionPoint.YCoordinateLineDraw = _blockSymbolVM.Height;
    }

    private void SetCoordinateLeftConnectionPoint(ConnectionPointVM leftConnectionPoint)
    {
        var yCoordinate = _blockSymbolVM.Height / 2;

        leftConnectionPoint.XCoordinate = -offsetPositionConnectionPoint;
        leftConnectionPoint.YCoordinate = yCoordinate;

        leftConnectionPoint.XCoordinateLineDraw = 0;
        leftConnectionPoint.YCoordinateLineDraw = yCoordinate;
    }
}
