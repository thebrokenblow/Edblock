using EdblockViewModel.Symbols.Abstraction;
using System;

namespace EdblockViewModel.Symbols.LineSymbols;




public interface IArrayDecorator
{
    int this[int index]
    {
        get;
        set;
    }
}

public class Coordinate1 : IArrayDecorator
{
    private readonly LineSymbolVM _lineSymbolVM;
    public Coordinate1(LineSymbolVM lineSymbolVM)
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
            else
            {
                return _lineSymbolVM.Y1;
            }
        }
        set
        {
            if (index == 0)
            {
                _lineSymbolVM.X1 = value;
            }
            else
            {
                _lineSymbolVM.Y1 = value;
            }
        }
    }
}


public class Coordinate2 : IArrayDecorator
{
    private readonly LineSymbolVM _lineSymbolVM;
    public Coordinate2(LineSymbolVM lineSymbolVM)
    {
        _lineSymbolVM = lineSymbolVM;
    }

    public int this[int index]
    {
        get
        {
            if (index == 0)
            {
                return _lineSymbolVM.X2;
            }
            else
            {
                return _lineSymbolVM.Y2;
            }
        }
        set
        {
            if (index == 0)
            {
                _lineSymbolVM.X2 = value;
            }
            else
            {
                _lineSymbolVM.Y2 = value;
            }
        }
    }
}


public class FactoryFoo
{
    private readonly Func<int, int> _bar1;
    private readonly Func<int, int> _bar2;

    public FactoryFoo(Func<int, int> bar1, Func<int, int> bar2)
    {
        _bar1 = bar1;
        _bar2 = bar2;
    }

    public Foo Create(IArrayDecorator boo)
    {
        return new Foo(boo, _bar1, _bar2);
    }
}

public class Foo : IArrayDecorator
{
    private readonly IArrayDecorator _boo1;
    private readonly Func<int, int> _boo2;
    private readonly Func<int, int> _boo3;
    public Foo(IArrayDecorator boo1, Func<int, int> boo2, Func<int, int> boo3)
    {
        _boo1 = boo1;
        _boo2 = boo2;
        _boo3 = boo3;
    }
    public int this[int index]
    {
        get => _boo3(_boo1[_boo2(index)]);
        set
        {
            _boo1[_boo2(index)] = _boo3(value);
        }
    }

}



public class FooLineSymbolVM
{
    private readonly LineSymbolVM _lineSymbolVM;
    public Coordinate1 Coordinate1 { get; set; }
    public Coordinate2 Coordinate2 { get; set; }

    public FooLineSymbolVM(LineSymbolVM lineSymbolVM)
    {
        _lineSymbolVM = lineSymbolVM;
        Coordinate1 = new(lineSymbolVM);
        Coordinate2 = new(lineSymbolVM);
    }




}

public class LineSymbolVM : Symbol
{
    private int x1;
    public int X1
    {
        get => x1;
        set
        {
            x1 = value;
            OnPropertyChanged();
        }
    }

    private int y1;
    public int Y1
    {
        get => y1;
        set
        {
            y1 = value;
            OnPropertyChanged();
        }
    }

    private int x2;
    public int X2
    {
        get => x2;
        set
        {
            x2 = value;
            OnPropertyChanged();
        }
    }

    private int y2;

    public int Y2
    {
        get => y2;
        set
        {
            y2 = value;
            OnPropertyChanged();
        }
    }
}
