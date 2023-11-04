using System;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class FactoryCoordinateLineSymbol
{
    private readonly Func<int, int> _calculationCoordinateX;
    private readonly Func<int, int> _coordinateCalculationY;

    public FactoryCoordinateLineSymbol(Func<int, int> calculationCoordinateX, Func<int, int> coordinateCalculationY)
    {
        _calculationCoordinateX = calculationCoordinateX;
        _coordinateCalculationY = coordinateCalculationY;
    }

    public CoordinateLineSymbol Create(IArrayDecorator boo)
    {
        return new CoordinateLineSymbol(boo, _calculationCoordinateX, _coordinateCalculationY);
    }
}