using System;
using System.Collections.Generic;
using EdblockModel.EnumsModel;
using EdblockViewModel.Symbols.Abstractions;

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
            { SideSymbol.Top, SetCoordinateTop },
            { SideSymbol.Right, SetCoordinateRight },
            { SideSymbol.Bottom, SetCoordinateBottom },
            { SideSymbol.Left, SetCoordinateLeft }
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

    private void SetCoordinateTop(ConnectionPointVM topConnectionPoint)
    {
        topConnectionPoint.XCoordinate = _blockSymbolVM.Width / 2;
        topConnectionPoint.YCoordinate = -offsetPositionConnectionPoint;
    }

    private void SetCoordinateRight(ConnectionPointVM rightConnectionPoint)
    {
        rightConnectionPoint.XCoordinate = _blockSymbolVM.Width + offsetPositionConnectionPoint;
        rightConnectionPoint.YCoordinate = _blockSymbolVM.Height / 2;
    }

    private void SetCoordinateBottom(ConnectionPointVM bottomConnectionPoint)
    {
        bottomConnectionPoint.XCoordinate = _blockSymbolVM.Width / 2;
        bottomConnectionPoint.YCoordinate = _blockSymbolVM.Height + offsetPositionConnectionPoint;
    }

    private void SetCoordinateLeft(ConnectionPointVM leftConnectionPoint)
    {
        leftConnectionPoint.XCoordinate = -offsetPositionConnectionPoint;
        leftConnectionPoint.YCoordinate = _blockSymbolVM.Height / 2; ;
    }

    public (double, double) GetCoordinateLineDrawTop() =>
        (_blockSymbolVM.XCoordinate + _blockSymbolVM.Width / 2, _blockSymbolVM.YCoordinate);

    public (double, double) GetCoordinateLineDrawRight() =>
        (_blockSymbolVM.XCoordinate + _blockSymbolVM.Width, _blockSymbolVM.YCoordinate + _blockSymbolVM.Height / 2);

    public (double, double) GetCoordinateLineDrawBottom() =>
        (_blockSymbolVM.XCoordinate + _blockSymbolVM.Width / 2, _blockSymbolVM.YCoordinate + _blockSymbolVM.Height);

    public (double, double) GetCoordinateLineDrawLeft() =>
        (_blockSymbolVM.XCoordinate, _blockSymbolVM.YCoordinate + _blockSymbolVM.Height / 2);
}
