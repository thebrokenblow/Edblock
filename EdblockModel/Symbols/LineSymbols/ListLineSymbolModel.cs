namespace EdblockModel.Symbols.LineSymbols;

public class ListLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbols { get; set; } = new();

    public List<LineSymbolModel> GetLines(int x, int y)
    {
        var lastLineSymbol = LinesSymbols[^1];
       
        if (lastLineSymbol.PositionConnectionPoint == PositionConnectionPoint.Bottom ||
            lastLineSymbol.PositionConnectionPoint == PositionConnectionPoint.Top)
        {
            if (LinesSymbols.Count % 2 == 0)
            {
                lastLineSymbol = LinesSymbols[^2];
                var secondLine = LinesSymbols[^1];
                lastLineSymbol.Y2 = y;
                secondLine.X2 = x;
                secondLine.Y1 = lastLineSymbol.Y2;
                secondLine.Y2 = lastLineSymbol.Y2;

                if (secondLine.X2 == lastLineSymbol.X2)
                {
                    LinesSymbols.Remove(secondLine);
                }
            }
            else
            {
                lastLineSymbol.Y2 = y;

                if (lastLineSymbol.X2 != x)
                {
                    var lineSymbol = FactoryLineSymbolModel.CreateLineByLine(lastLineSymbol);
                    LinesSymbols.Add(lineSymbol);
                }
            }
        }
        else
        {
            if (LinesSymbols.Count % 2 == 0)
            {
                lastLineSymbol = LinesSymbols[^2];
                var secondLine = LinesSymbols[^1];
                lastLineSymbol.X2 = x;
                secondLine.Y2 = y;
                secondLine.X1 = lastLineSymbol.X2;
                secondLine.X2 = lastLineSymbol.X2;

                if (secondLine.Y2 == lastLineSymbol.Y2)
                {
                    LinesSymbols.Remove(secondLine);
                }
            }
            else
            {
                lastLineSymbol.X2 = x;
                if (lastLineSymbol.Y2 != y)
                {
                    if (LinesSymbols.Count % 2 != 0)
                    {
                        var lineSymbol = FactoryLineSymbolModel.CreateLineByLine(lastLineSymbol);
                        LinesSymbols.Add(lineSymbol);
                    }
                }
            }
        }

        return LinesSymbols;
    }
}