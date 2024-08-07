﻿using System;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;
using EdblockModel.Lines;

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