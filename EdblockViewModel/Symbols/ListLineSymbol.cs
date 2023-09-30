using System.Collections.ObjectModel;
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
        var lastLine = LineSymbols[^1];
        if (lastLine.Orientation == Orientation.Vertical)
        {
            if (LineSymbols.Count % 2 == 0)
            {
                lastLine = LineSymbols[^2];
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
                            Orientation = lastLine.Orientation
                        };
                        LineSymbols.Add(line);
                    }
                }
            }
        }
        else
        {
            if (LineSymbols.Count % 2 == 0)
            {
                lastLine = LineSymbols[^2];
                var secondLine = LineSymbols[^1];
                lastLine.X2 = x;
                secondLine.Y2 = y;
                secondLine.X1 = lastLine.X2;
                secondLine.X2 = lastLine.X2;

                if (secondLine.Y2 == lastLine.Y2)
                {
                    LineSymbols.Remove(secondLine);
                }
            }
            else
            {
                lastLine.X2 = x;
                if (lastLine.Y2 != y)
                {
                    if (LineSymbols.Count % 2 != 0)
                    {
                        var line = new LineSymbol
                        {
                            X1 = lastLine.X1,
                            X2 = lastLine.X1,
                            Y1 = lastLine.Y2,
                            Y2 = lastLine.Y2,
                            Orientation = lastLine.Orientation
                        };
                        LineSymbols.Add(line);
                    }
                }
            }
        }
    }
}