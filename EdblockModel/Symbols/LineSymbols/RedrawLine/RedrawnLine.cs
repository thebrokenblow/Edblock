using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.RedrawLine;

public class RedrawnLine
{
    public List<CoordinateLine> DecoratedCoordinatesLines { get; set; }
    private List<CoordinateLine> coordinatesLines;
    private readonly RedrawnLineParallelSides redrawnParallelSides;
    private readonly RedrawnLineIdenticalSides redrawnLineIdenticalSides;
    private readonly RedrawnLineNoParallelSides redrawnLineNoParallelSides;
    private List<LineSymbolModel> _linesSymbolModel;
    private const int baseLineOffset = 20;

    public RedrawnLine(DrawnLineSymbolModel drawnLineSymbolModel)
    {
        coordinatesLines = new();
        DecoratedCoordinatesLines = new();

        redrawnParallelSides = new(drawnLineSymbolModel, this, baseLineOffset);
        redrawnLineIdenticalSides = new(drawnLineSymbolModel, this, baseLineOffset);
        redrawnLineNoParallelSides = new(drawnLineSymbolModel, this, baseLineOffset);

        _linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
    }

    public List<LineSymbolModel> GetRedrawLine()
    {
        redrawnParallelSides.RedrawLine();
        redrawnLineIdenticalSides.RedrawLine();
        redrawnLineNoParallelSides.RedrawLine();

        return _linesSymbolModel;
    }

    public void SetReverseCoordinateLineModel()
    {
        var countDecoratedCoordinatesLines = DecoratedCoordinatesLines.Count;
        ChangeCountLinesModel(countDecoratedCoordinatesLines);

        for (int i = 0; i < coordinatesLines.Count; i++)
        {
            var lineSymbol = _linesSymbolModel[i];

            lineSymbol.X1 = coordinatesLines[i].SecondCoordinate.X;
            lineSymbol.Y1 = coordinatesLines[i].SecondCoordinate.Y;

            lineSymbol.X2 = coordinatesLines[i].FirstCoordinate.X;
            lineSymbol.Y2 = coordinatesLines[i].FirstCoordinate.Y;
        }

        _linesSymbolModel = Enumerable.Reverse(_linesSymbolModel).ToList();
    }

    public void Reverse()
    {
        foreach (var item in coordinatesLines)
        {
            (item.FirstCoordinate, item.SecondCoordinate) = (item.SecondCoordinate, item.FirstCoordinate);
        }

        coordinatesLines = Enumerable.Reverse(coordinatesLines).ToList();
    }

    public void SetCoordinateLineModel()
    {
        var countDecoratedCoordinatesLines = DecoratedCoordinatesLines.Count;
        ChangeCountLinesModel(countDecoratedCoordinatesLines);

        for (int i = 0; i < coordinatesLines.Count; i++)
        {
            var lineSymbol = _linesSymbolModel[i];

            lineSymbol.X1 = coordinatesLines[i].FirstCoordinate.X;
            lineSymbol.Y1 = coordinatesLines[i].FirstCoordinate.Y;

            lineSymbol.X2 = coordinatesLines[i].SecondCoordinate.X;
            lineSymbol.Y2 = coordinatesLines[i].SecondCoordinate.Y;
        }
    }

    private void ChangeCountLinesModel(int currentCountLines)
    {
        if (_linesSymbolModel.Count == currentCountLines)
        {
            return;
        }

        _linesSymbolModel.Clear();

        for (int i = 0; i < currentCountLines; i++)
        {
            var lineSymbol = new LineSymbolModel();
            _linesSymbolModel.Add(lineSymbol);
        }
    }

    public void ChangeCountDecoratedLines(int currentCountLines, BuilderCoordinateDecorator? builderCoordinateDecorator = null)
    {
        if (DecoratedCoordinatesLines.Count == currentCountLines)
        {
            return;
        }

        DecoratedCoordinatesLines.Clear();

        for (int i = 0; i < currentCountLines; i++)
        {
            ICoordinateDecorator firstLineCoordinate = new CoordinateDecorator();
            ICoordinateDecorator secondCoordinate = new CoordinateDecorator();

            var coordinateLine = new CoordinateLine(firstLineCoordinate, secondCoordinate);

            coordinatesLines.Add(coordinateLine);

            if (builderCoordinateDecorator is not null)
            {
                firstLineCoordinate = builderCoordinateDecorator.Build(firstLineCoordinate);
                secondCoordinate = builderCoordinateDecorator.Build(secondCoordinate);
            }

            var decoratedCoordinateLine = new CoordinateLine(firstLineCoordinate, secondCoordinate);
            DecoratedCoordinatesLines.Add(decoratedCoordinateLine);
        }
    }
}