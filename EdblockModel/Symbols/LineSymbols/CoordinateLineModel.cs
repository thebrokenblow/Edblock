namespace EdblockModel.Symbols.LineSymbols;

public class CoordinateLineModel
{
    private readonly List<LineSymbolModel> _lineSymbolModel;
    public CoordinateLineModel(List<LineSymbolModel> lineSymbolModel)
    {
        _lineSymbolModel = lineSymbolModel;
    }

    public (int, int) GetStartCoordinate() //Получение начальных координат в зависимости от уже нарисованных линий
    {
        if (_lineSymbolModel.Count == 1 || _lineSymbolModel.Count == 2)
        {
            var firstLine = _lineSymbolModel[0];

            return (firstLine.X1, firstLine.Y1);
        }
        else if (_lineSymbolModel.Count % 2 == 1)
        {
            var lastLine = _lineSymbolModel[^1];

            return (lastLine.X1, lastLine.Y1);
        }
        else
        {
            var penultimateLine = _lineSymbolModel[^2];

            return (penultimateLine.X1, penultimateLine.Y1);
        }
    }

    public void ChangeCoordinatesVerticalLines((int x, int y) currentCoordinate)
    {
        var lastLineSymbol = _lineSymbolModel[^1];

        if (_lineSymbolModel.Count % 2 == 0)
        {
            var penultimateLine = _lineSymbolModel[^2];

            lastLineSymbol.X2 = currentCoordinate.x;
            penultimateLine.Y2 = currentCoordinate.y;

            lastLineSymbol.Y1 = penultimateLine.Y2;
            lastLineSymbol.Y2 = penultimateLine.Y2;

            if (lastLineSymbol.X2 == penultimateLine.X2)
            {
                _lineSymbolModel.Remove(lastLineSymbol);
            }
        }
        else
        {
            lastLineSymbol.Y2 = currentCoordinate.y;

            if (lastLineSymbol.X2 != currentCoordinate.x)
            {
                var lineSymbol = FactoryLineSymbolModel.CreateLineByPreviousLine(lastLineSymbol);
                _lineSymbolModel.Add(lineSymbol);
            }
        }
    }

    public void ChangeCoordinatesHorizontalLines((int x, int y) currentCoordinate)
    {
        var lastLineSymbol = _lineSymbolModel[^1];

        if (_lineSymbolModel.Count % 2 == 0)
        {
            var penultimateLine = _lineSymbolModel[^2];

            lastLineSymbol.Y2 = currentCoordinate.y;
            penultimateLine.X2 = currentCoordinate.x;

            lastLineSymbol.X1 = penultimateLine.X2;
            lastLineSymbol.X2 = penultimateLine.X2;

            if (lastLineSymbol.Y2 == penultimateLine.Y2)
            {
                _lineSymbolModel.Remove(lastLineSymbol);
            }
        }
        else
        {
            lastLineSymbol.X2 = currentCoordinate.x;

            if (lastLineSymbol.Y2 != currentCoordinate.y)
            {
                var lineSymbol = FactoryLineSymbolModel.CreateLineByPreviousLine(lastLineSymbol);
                _lineSymbolModel.Add(lineSymbol);
            }
        }
    }
}