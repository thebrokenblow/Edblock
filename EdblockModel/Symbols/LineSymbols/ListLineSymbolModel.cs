using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class ListLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbols { get; set; } = new();

    public List<LineSymbolModel> GetLines(int currentX, int currentY)
    {
        var lastLineSymbol = LinesSymbols[^1];

        if (lastLineSymbol.PositionConnectionPoint == PositionConnectionPoint.Bottom ||
            lastLineSymbol.PositionConnectionPoint == PositionConnectionPoint.Top)
        {
            ChangeCoordinatesVerticalLines(lastLineSymbol, currentX, currentY);
        }
        else
        {
            ChangeCoordinatesHorizontalLines(lastLineSymbol, currentX, currentY);
        }

        return LinesSymbols;
    }

    private void ChangeCoordinatesVerticalLines(LineSymbolModel lastLineSymbol, int currentX, int currentY)
    {
        if (LinesSymbols.Count % 2 == 0)
        {
            var penultimateLine = LinesSymbols[^2];
            penultimateLine.Y2 = currentY;

            lastLineSymbol = LinesSymbols[^1];
            lastLineSymbol.X2 = currentX;
            lastLineSymbol.Y1 = penultimateLine.Y2;
            lastLineSymbol.Y2 = penultimateLine.Y2;

            RemoveNewLine(lastLineSymbol, lastLineSymbol.X2, penultimateLine.X2);
        }
        else
        {
            lastLineSymbol.Y2 = currentY;
            AddNewLine(lastLineSymbol, lastLineSymbol.X2, currentY);
        }
    }

    private void ChangeCoordinatesHorizontalLines(LineSymbolModel lastLineSymbol, int currentX, int currentY)
    {
        if (LinesSymbols.Count % 2 == 0)
        {
            var penultimateLine = LinesSymbols[^2];
            penultimateLine.X2 = currentX;

            lastLineSymbol = LinesSymbols[^1];
            lastLineSymbol.Y2 = currentY;
            lastLineSymbol.X1 = penultimateLine.X2;
            lastLineSymbol.X2 = penultimateLine.X2;

            RemoveNewLine(lastLineSymbol, lastLineSymbol.Y2, penultimateLine.Y2);
        }
        else
        {
            lastLineSymbol.X2 = currentX;
            AddNewLine(lastLineSymbol, lastLineSymbol.Y2, currentY);
        }
    }

    private void RemoveNewLine(LineSymbolModel lastLineSymbol, int coordinateLastLine, int coordinatePenultimateLine)
    {
        if (coordinateLastLine == coordinatePenultimateLine)
        {
            LinesSymbols.Remove(lastLineSymbol);
        }
    }

    private void AddNewLine(LineSymbolModel lastLineSymbol, int coordinateCurrentLine, int coordinate)
    {
        if (coordinateCurrentLine != coordinate)
        {
            var lineSymbol = FactoryLineSymbolModel.CreateCloneLine(lastLineSymbol);
            LinesSymbols.Add(lineSymbol);
        }
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