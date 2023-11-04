using System;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class AdapterCoordinateLineSymbol : IArrayDecorator
{
    private readonly LineSymbolVM _lineSymbolVM;
    public AdapterCoordinateLineSymbol(LineSymbolVM lineSymbolVM)
    {
        _lineSymbolVM = lineSymbolVM;
    }

    public int this[int index]
    {
        get
        {
            if (index == 0)
            {
                return _lineSymbolVM.X1;
            }
            else if (index == 1)
            {
                return _lineSymbolVM.Y1;
            }
            else if (index == 2)
            {
                return _lineSymbolVM.X2;
            }
            else if (index == 3)
            {
                return _lineSymbolVM.Y2;
            }

            throw new Exception($"Нет координаты по индексу {index}");
        }
        set
        {
            if (index == 0)
            {
                _lineSymbolVM.X1 = value;
            }
            else if (index == 1)
            {
                _lineSymbolVM.Y1 = value;
            }
            else if (index == 2)
            {
                _lineSymbolVM.X2 = value;
            }
            else if (index == 3)
            {
                _lineSymbolVM.Y2 = value;
            }
        }
    }
}