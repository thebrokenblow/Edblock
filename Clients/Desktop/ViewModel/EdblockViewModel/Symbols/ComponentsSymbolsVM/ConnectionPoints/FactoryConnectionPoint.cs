using EdblockModel.EnumsModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using System;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class FactoryConnectionPoint(
    ICanvasSymbolsComponentVM canvasSymbolsVM, 
    ILineStateStandardComponentVM lineStateStandardComponentVM,
    BlockSymbolVM blockSymbolVM)
{
    public ConnectionPointVM Create(SideSymbol sideSymbol, Func<(double, double)> getCoordinateDrawLine) =>
        new(canvasSymbolsVM,
            lineStateStandardComponentVM,
            blockSymbolVM,
            sideSymbol,
            getCoordinateDrawLine);
}