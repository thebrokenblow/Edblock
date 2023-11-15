using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols;

public class CompletedLineModel
{
    private readonly DrawnLineSymbolModel _drawnLineSymbolModel;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLineModel(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        _drawnLineSymbolModel = drawnLineSymbolModel;
        _finalCoordinate = finalCoordinate;
    }

    public void Complete()
    {
        var lastLine = _drawnLineSymbolModel.LinesSymbolModel[^1];
        ICoordinateDecorator coordinateLine = new CoordinateDecorator((lastLine.X2, lastLine.Y2));
        ICoordinateDecorator coordinateBlockSymbol = new CoordinateDecorator((_finalCoordinate.x, _finalCoordinate.y));

        var builderCoordinateDecorator = new BuilderCoordinateDecorator().SetSwap();
        
        coordinateLine = builderCoordinateDecorator.Build(coordinateLine);
        coordinateBlockSymbol = builderCoordinateDecorator.Build(coordinateBlockSymbol);


        FinishDrawingParallelBorders(lastLine, coordinateLine, coordinateBlockSymbol);
    }

    private void FinishDrawingParallelBorders(LineSymbolModel lastLineSymbolModel, ICoordinateDecorator coordinateLine, ICoordinateDecorator coordinateBlockSymbol)
    {
        if (coordinateLine.X == coordinateBlockSymbol.X)
        {
            lastLineSymbolModel.X2 = coordinateLine.X;
            lastLineSymbolModel.Y2 = coordinateLine.Y;
        }
        else
        {
            var firstLine = new LineSymbolModel
            {
                X1 = lastLineSymbolModel.X2,
                Y1 = lastLineSymbolModel.Y2,
                X2 = coordinateBlockSymbol.X,
                Y2 = coordinateBlockSymbol.Y
            };

            var secondLine = new LineSymbolModel
            {
                X1 = coordinateBlockSymbol.X,
                Y1 = lastLineSymbolModel.Y2,
                X2 = coordinateBlockSymbol.X,
                Y2 = coordinateBlockSymbol.Y,
            };

            _drawnLineSymbolModel.LinesSymbolModel.Add(firstLine);
            _drawnLineSymbolModel.LinesSymbolModel.Add(secondLine);
        }
    }
}