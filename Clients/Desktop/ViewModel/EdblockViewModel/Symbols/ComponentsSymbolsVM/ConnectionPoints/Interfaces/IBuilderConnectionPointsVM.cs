using System;
using System.Collections.Generic;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints.Interfaces;

public interface IBuilderConnectionPointsVM
{
    IBuilderConnectionPointsVM AddTopConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null);
    IBuilderConnectionPointsVM AddRightConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null);
    IBuilderConnectionPointsVM AddBottomConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null);
    IBuilderConnectionPointsVM AddLeftConnectionPoint(Func<(double, double)>? getCoordinateConnectionPoint = null, Func<(double, double)>? getCoordinateStartDrawLine = null);

    public List<ConnectionPointVM> Build();
}