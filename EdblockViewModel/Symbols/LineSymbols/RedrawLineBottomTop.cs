using EdblockViewModel.Symbols.Abstraction;
using System;

namespace EdblockViewModel.Symbols.LineSymbols;


//public class FactoryFoo
//{
//    private readonly Func<int, int> _bar1;
//    private readonly Func<int, int> _bar2;

//    public FactoryFoo(Func<int, int> bar1, Func<int, int> bar2)
//    {
//        _bar1 = bar1;
//        _bar2 = bar2;
//    }

//    public Foo Create(int[] boo)
//    {
//        return new Foo(boo, _bar1, _bar2);
//    }
//}

//public class Foo
//{
//    private readonly int[] _boo1;
//    private readonly Func<int, int> _boo2;
//    private readonly Func<int, int> _boo3;
//    public Foo(int[] boo1, Func<int, int> boo2, Func<int, int> boo3)
//    {
//        _boo1 = boo1;
//        _boo2 = boo2;
//        _boo3 = boo3;
//    }

//    public int this[int index]
//    {
//        get => _boo3(_boo1[_boo2(index)]);
//    }
//}

public class Adapter : IArrayDecorator
{
    private readonly int[] _foo;
    public Adapter(int[] foo)
    {
        _foo = foo;
    }

    public int this[int index] { get => _foo[index]; set => _foo[index] = value; }
}

internal class RedrawLineBottomTop
{
    private readonly BlockSymbol? _symbolaIncomingLine;
    private readonly BlockSymbol? _symbolOutgoingLine;
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private const int oneLine = 1;
    private const int treeLine = 3;
    private const int fiveLine = 5;
    private const int baseLineOffset = 20;

    public RedrawLineBottomTop(DrawnLineSymbolVM drawnLineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;

        _symbolaIncomingLine = drawnLineSymbolVM.SymbolaIncomingLine;
        _symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;
    }

    public void Redraw()
    {
        var factory = new FactoryFoo((int x) =>  x, (int x) => x);

        var sizeSymbolOutgoingLine = factory.Create(new Adapter(_symbolOutgoingLine!.GetSize()));
        var coordinateSymbolOutgoingLine = factory.Create(new Adapter(_symbolOutgoingLine!.GetCoordinate()));

        var szieSymbolaIncomingLine = factory.Create(new Adapter(_symbolOutgoingLine!.GetSize()));
        var coordinateSymbolaIncomingLine = factory.Create(new Adapter(_symbolOutgoingLine!.GetCoordinate()));

        //    var f = (int x) => x;
        //  var f = (int x) => 1-x;

        int ySymbolOutgoingLine = coordinateSymbolOutgoingLine[1] + sizeSymbolOutgoingLine[1];
        int xSymbolOutgoingLine = coordinateSymbolOutgoingLine[0] + sizeSymbolOutgoingLine[1] / 2;

        int xSymbolaIncomingLine = coordinateSymbolaIncomingLine[0] + szieSymbolaIncomingLine[0] / 2;


        if (ySymbolOutgoingLine < szieSymbolaIncomingLine[0])
        {
            if (xSymbolOutgoingLine == xSymbolaIncomingLine)
            {
                SetCoordnateOneLine(factory, sizeSymbolOutgoingLine, coordinateSymbolOutgoingLine, szieSymbolaIncomingLine, coordinateSymbolaIncomingLine);
            }
            else
            {
                SetCoordnateTreeLine();
            }
        }
        else
        {
            SetCoordnateFive();
        }
    }

    private void SetCoordnateOneLine(FactoryFoo factory, Foo sizeSymbolOutgoingLine, Foo coordinateSymbolOutgoingLine,
        Foo szieSymbolaIncomingLine, Foo coordinateSymbolaIncomingLine)
    {
        InitLines(oneLine);

        var firstLine = new FooLineSymbolVM(_drawnLineSymbolVM.LineSymbols[^1]);
        var coordinates1 = factory.Create(firstLine.Coordinate1);
        var coordinates2 = factory.Create(firstLine.Coordinate2);

        coordinates1[0] = coordinateSymbolOutgoingLine[0]! + sizeSymbolOutgoingLine[0] / 2;
        coordinates1[1] = coordinateSymbolOutgoingLine[1] + sizeSymbolOutgoingLine[1];
        coordinates2[0] = coordinateSymbolaIncomingLine[0] + szieSymbolaIncomingLine[0] / 2;
        coordinates2[1] = coordinateSymbolaIncomingLine[1];
    }

    private void InitLines(int countLine)
    {
        if (_drawnLineSymbolVM.LineSymbols.Count != countLine)
        {
            _drawnLineSymbolVM.LineSymbols.Clear();
            for (int i = 0; i < countLine; i++)
            {
                var lineSymbolVM = new LineSymbolVM();
                _drawnLineSymbolVM.LineSymbols.Add(lineSymbolVM);
            }
        }
    }

    private void SetCoordnateTreeLine()
    {
        InitLines(treeLine);

        var firstLine = _drawnLineSymbolVM.LineSymbols[^3];

        firstLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
        firstLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine!.Height;
        firstLine.X2 = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine!.Width / 2;
        firstLine.Y2 = (_symbolOutgoingLine.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^2];

        secondLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine!.Width / 2;
        secondLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
        secondLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
        secondLine.Y2 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^1];

        thirdLine.X1 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
        thirdLine.Y1 = (_symbolOutgoingLine!.YCoordinate + _symbolaIncomingLine!.Height + _symbolaIncomingLine!.YCoordinate) / 2;
        thirdLine.X2 = _symbolaIncomingLine!.XCoordinate + _symbolaIncomingLine!.Width / 2;
        thirdLine.Y2 = _symbolaIncomingLine!.YCoordinate;
    }

    private void SetCoordnateFive()
    {
        InitLines(fiveLine);

        var firstLine = _drawnLineSymbolVM.LineSymbols[^5];

        firstLine.X1 = _symbolOutgoingLine!.XCoordinate + _symbolOutgoingLine.Width / 2;
        firstLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height;
        firstLine.X2 = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width / 2;
        firstLine.Y2 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;

        var secondLine = _drawnLineSymbolVM.LineSymbols[^4];

        secondLine.X1 = _symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width / 2;
        secondLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;
        secondLine.X2 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine!.XCoordinate) / 2;
        secondLine.Y2 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;

        var thirdLine = _drawnLineSymbolVM.LineSymbols[^3];

        thirdLine.X1 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine!.XCoordinate) / 2;
        thirdLine.Y1 = _symbolOutgoingLine.YCoordinate + _symbolOutgoingLine.Height + baseLineOffset;
        thirdLine.X2 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine.XCoordinate) / 2;
        thirdLine.Y2 = _symbolaIncomingLine.YCoordinate - baseLineOffset;

        var fourthLine = _drawnLineSymbolVM.LineSymbols[^2];

        fourthLine.X1 = (_symbolOutgoingLine.XCoordinate + _symbolOutgoingLine.Width + _symbolaIncomingLine.XCoordinate) / 2;
        fourthLine.Y1 = _symbolaIncomingLine.YCoordinate - baseLineOffset;
        fourthLine.X2 = _symbolaIncomingLine.XCoordinate + _symbolaIncomingLine.Width / 2;
        fourthLine.Y2 = _symbolaIncomingLine.YCoordinate - baseLineOffset;

        var fifthLine = _drawnLineSymbolVM.LineSymbols[^1];

        fifthLine.X1 = _symbolaIncomingLine.XCoordinate + _symbolaIncomingLine.Width / 2;
        fifthLine.Y1 = _symbolaIncomingLine.YCoordinate - baseLineOffset;
        fifthLine.X2 = _symbolaIncomingLine.XCoordinate + _symbolaIncomingLine.Width / 2;
        fifthLine.Y2 = _symbolaIncomingLine.YCoordinate;
    }
}