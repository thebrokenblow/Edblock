using System;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class CoordinateLineSymbol : IArrayDecorator
{
    private readonly IArrayDecorator _arrayDecorator;
    private readonly Func<int, int> _calculationCoordinateX;
    private readonly Func<int, int> _calculationCoordinateY;
    public CoordinateLineSymbol(IArrayDecorator arrayDecorator, Func<int, int> calculationCoordinateX, Func<int, int> calculationCoordinateY)
    {
        _arrayDecorator = arrayDecorator;
        _calculationCoordinateX = calculationCoordinateX;
        _calculationCoordinateY = calculationCoordinateY;
    }
    public int this[int index]
    {
        get => _calculationCoordinateY(_arrayDecorator[_calculationCoordinateX(index)]);
        set
        {
            _arrayDecorator[_calculationCoordinateX(index)] = _calculationCoordinateY(value);
        }
    }
}