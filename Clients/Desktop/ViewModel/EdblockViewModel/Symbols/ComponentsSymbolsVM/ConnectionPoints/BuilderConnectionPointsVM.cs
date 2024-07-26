using System;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;
using EdblockModel.Lines;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class BuilderConnectionPointsVM(ICanvasSymbolsComponentVM canvasSymbolsComponentVM, ILineStateStandardComponentVM lineStateStandardComponentVM, BlockSymbolVM blockSymbolVM) : IBuilderConnectionPointsVM
{
    private readonly FactoryConnectionPoint _factoryConnectionPoint = new(canvasSymbolsComponentVM, lineStateStandardComponentVM, blockSymbolVM);
    private readonly List<ConnectionPointVM> connectionPointsVM = [];
    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM = new(blockSymbolVM);

    public IBuilderConnectionPointsVM AddTopConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null)
    {
        getCoordinateConnectionPoint ??= coordinateConnectionPointVM.GetCoordinateTop;
        getCoordinateStartDrawLine ??= coordinateConnectionPointVM.GetCoordinateStartDrawLineTop;
        var topConnectionPointVM = _factoryConnectionPoint.Create(SideSymbol.Top, getCoordinateConnectionPoint, getCoordinateStartDrawLine);
        connectionPointsVM.Add(topConnectionPointVM);

        return this;
    }

    public IBuilderConnectionPointsVM AddRightConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null)
    {
        getCoordinateConnectionPoint ??= coordinateConnectionPointVM.GetCoordinateRight;
        getCoordinateStartDrawLine ??= coordinateConnectionPointVM.GetCoordinateStartDrawLineRight;
        var rightConnectionPointVM = _factoryConnectionPoint.Create(SideSymbol.Right, getCoordinateConnectionPoint, getCoordinateStartDrawLine);
        connectionPointsVM.Add(rightConnectionPointVM);

        return this;
    }

    public IBuilderConnectionPointsVM AddBottomConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null)
    {
        getCoordinateConnectionPoint ??= coordinateConnectionPointVM.GetCoordinateBottom;
        getCoordinateStartDrawLine ??= coordinateConnectionPointVM.GetCoordinateStartDrawLineBottom;
        var bottomConnectionPointVM = _factoryConnectionPoint.Create(SideSymbol.Bottom, getCoordinateConnectionPoint, getCoordinateStartDrawLine);
        connectionPointsVM.Add(bottomConnectionPointVM);

        return this;
    }

    public IBuilderConnectionPointsVM AddLeftConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null)
    {
        getCoordinateConnectionPoint ??= coordinateConnectionPointVM.GetCoordinateLeft;
        getCoordinateStartDrawLine ??= coordinateConnectionPointVM.GetCoordinateStartDrawLineLeft;
        var leftConnectionPointVM = _factoryConnectionPoint.Create(SideSymbol.Left, getCoordinateConnectionPoint, getCoordinateStartDrawLine);
        connectionPointsVM.Add(leftConnectionPointVM);

        return this;
    }

    public List<ConnectionPointVM> Build() =>
        connectionPointsVM;
}