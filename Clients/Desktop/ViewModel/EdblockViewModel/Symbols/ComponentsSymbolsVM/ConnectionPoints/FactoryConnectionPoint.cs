using System;
using EdblockModel.EnumsModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class FactoryConnectionPoint(
    ICanvasSymbolsComponentVM canvasSymbolsVM, 
    ILineStateStandardComponentVM lineStateStandardComponentVM,
    BlockSymbolVM blockSymbolVM) : IFactoryConnectionPoint
{
    public ConnectionPointVM Create(
        SideSymbol sideSymbol, 
        Func<(double, double)> getCoordinateConnectionPoint, 
        Func<(double, double)> getCoordinateDrawLine) =>
            new(canvasSymbolsVM,
                lineStateStandardComponentVM,
                blockSymbolVM,
                sideSymbol,
                getCoordinateConnectionPoint,
                getCoordinateDrawLine);
}