using System.Collections.ObjectModel;
using System.Windows.Shapes;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols;

public class ListLineSymbol : Symbol
{
    public ObservableCollection<LineSymbol> LineSymbols { get; init; }

    public ListLineSymbol()
    {
        LineSymbols = new();
    }

    internal void DrawLine(int x, int y)
    {
        if (LineSymbols.Count % 2 == 0)
        {
            var lastLine = LineSymbols[^2];
            var secondLine = LineSymbols[^1];
            lastLine.Y2 = y;
            secondLine.X2 = x;
            secondLine.Y1 = lastLine.Y2;
            secondLine.Y2 = lastLine.Y2;

            if (secondLine.X2 == lastLine.X2)
            {
                LineSymbols.Remove(secondLine);
            }
        }
        else
        {
            var lastLine = LineSymbols[^1];
            lastLine.Y2 = y;
            if (lastLine.X2 != x)
            {
                if (LineSymbols.Count % 2 != 0)
                {
                    var line = new LineSymbol
                    {
                        X1 = lastLine.X1,
                        X2 = lastLine.X1,
                        Y1 = lastLine.Y2,
                        Y2 = lastLine.Y2,
                    };
                    LineSymbols.Add(line);
                }
            }
        }
    }
}