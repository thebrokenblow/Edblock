namespace EdblockModel.Symbols.LineSymbols;

public class CoordinateLineModel
{
    public static (int, int) GetStartCoordinate(List<SymbolLineModel> linesSymbols) //Получение начальных координат в зависимости от уже нарисованных линий
    {
        if (linesSymbols.Count == 1 || linesSymbols.Count == 2)
        {
            var firstLine = linesSymbols[0];

            return (firstLine.X1, firstLine.Y1);
        }
        else if (linesSymbols.Count % 2 == 1)
        {
            var lastLine = linesSymbols[^1];

            return (lastLine.X1, lastLine.Y1);
        }
        else
        {
            var penultimateLine = linesSymbols[^2];

            return (penultimateLine.X1, penultimateLine.Y1);
        }
    }

    public static void ChangeCoordinatesVerticalLines(List<SymbolLineModel> linesSymbols, SymbolLineModel lastLineSymbol, int currentX, int currentY)
    {
        if (linesSymbols.Count % 2 == 0)
        {
            var penultimateLine = linesSymbols[^2];

            penultimateLine.Y2 = currentY;

            lastLineSymbol = linesSymbols[^1];

            lastLineSymbol.X2 = currentX;
            lastLineSymbol.Y1 = penultimateLine.Y2;
            lastLineSymbol.Y2 = penultimateLine.Y2;

            RemoveNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.X2, penultimateLine.X2);
        }
        else
        {
            lastLineSymbol.Y2 = currentY;
            AddNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.X2, currentX);
        }
    }

    public static void ChangeCoordinatesHorizontalLines(List<SymbolLineModel> linesSymbols, SymbolLineModel lastLineSymbol, int currentX, int currentY)
    {
        if (linesSymbols.Count % 2 == 0)
        {
            var penultimateLine = linesSymbols[^2];

            penultimateLine.X2 = currentX;

            lastLineSymbol = linesSymbols[^1];

            lastLineSymbol.Y2 = currentY;
            lastLineSymbol.X1 = penultimateLine.X2;
            lastLineSymbol.X2 = penultimateLine.X2;

            RemoveNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.Y2, penultimateLine.Y2);
        }
        else
        {
            lastLineSymbol.X2 = currentX;
            AddNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.Y2, currentY);
        }
    }

    private static void RemoveNewLine(List<SymbolLineModel> linesSymbols, SymbolLineModel lastLineSymbol, int coordinateLastLine, int coordinatePenultimateLine)
    {
        if (coordinateLastLine == coordinatePenultimateLine)
        {
            linesSymbols.Remove(lastLineSymbol);
        }
    }

    private static void AddNewLine(List<SymbolLineModel> linesSymbols, SymbolLineModel lastLineSymbol, int coordinateCurrentLine, int coordinate)
    {
        if (coordinateCurrentLine != coordinate)
        {
            var lineSymbol = FactoryLineSymbolModel.CreateCloneLine(lastLineSymbol);
            linesSymbols.Add(lineSymbol);
        }
    }
}
