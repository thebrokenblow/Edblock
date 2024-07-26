using System;
using EdblockModel.Lines;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints.Interfaces;

public interface IFactoryConnectionPoint
{
    ConnectionPointVM Create(SideSymbol sideSymbol, Func<(double, double)> getCoordinateConnectionPoint, Func<(double, double)> getCoordinateDrawLine);
}