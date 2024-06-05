﻿using EdblockModel.EnumsModel;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class FactoryConnectionPoint(ICanvasSymbolsComponentVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM, ILineStateStandardComponentVM lineStateStandardComponentVM)
{
    public ConnectionPointVM Create(SideSymbol sideSymbol)
    {
        var connectionPointVM = new ConnectionPointVM(
                canvasSymbolsVM,
                lineStateStandardComponentVM,
                blockSymbolVM,
                sideSymbol);

        return connectionPointVM;
    }
}