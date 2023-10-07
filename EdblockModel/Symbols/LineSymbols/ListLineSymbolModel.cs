using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class ListLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbols { get; set; } = new();

    public List<LineSymbolModel> GetLines(int x, int y)
    {
        var lastLineSymbol = LinesSymbols[^1];
       
        //TODO: улучшить код и сделать оптимизацию
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

    public (int, int) GetStartCoordinate() //Получение начальных координат в зависимости от уже нарисованных линий
    {
        if (LinesSymbols.Count == 1 || LinesSymbols.Count == 2)
        {
            var firstLine = LinesSymbols[0];

            return (firstLine.X1, firstLine.Y1);
        }
        else if (LinesSymbols.Count % 2 == 1)
        {
            var lastLine = LinesSymbols[^1];

            return (lastLine.X1, lastLine.Y1);
        }
        else
        {
            var penultimateLine = LinesSymbols[^2];

            return (penultimateLine.X1, penultimateLine.Y1);
        }
    }
}