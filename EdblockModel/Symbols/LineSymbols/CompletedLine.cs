using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.LineSymbols;

public class CompletedLine
{
    private readonly List<SymbolLineModel> symbolLines;
    private readonly SymbolLineModel lastSymbolLine;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLine(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        symbolLines = drawnLineSymbolModel.LinesSymbolModel;
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;

        _finalCoordinate = finalCoordinate;

        lastSymbolLine = symbolLines[^1];
    }

    public List<SymbolLineModel> GetCompleteLines()
    {
        if (symbolLines.Count % 2 == 1)
        {
            FinishDrawingOddLines();
        }
        else
        {
            FinishDrawingEvenLines();
        }

        return symbolLines;
    }

    public List<SymbolLineModel> FinishDrawingOddLines()
    {
        if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var coordinatePenultimateLine = (_finalCoordinate.x, lastSymbolLine.Y2);
                FinishDrawingParallelSides(coordinatePenultimateLine, _finalCoordinate);
            }
            else
            {
                lastSymbolLine.Y2 = _finalCoordinate.y;
                var coordinateLineSymbolModel = (lastSymbolLine.X2, lastSymbolLine.Y2);
                FinishDrawLastLine(coordinateLineSymbolModel);
            }
        }
        else
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                lastSymbolLine.X2 = _finalCoordinate.x;
                var coordinateLineSymbolModel = (lastSymbolLine.X2, lastSymbolLine.Y2);
                FinishDrawLastLine(coordinateLineSymbolModel);
            }
            else
            {
                var coordinatePenultimateLine = (lastSymbolLine.X2, _finalCoordinate.y);
                FinishDrawingParallelSides(coordinatePenultimateLine, _finalCoordinate);
            }
        }

        return symbolLines;
    }

    public List<SymbolLineModel> FinishDrawingEvenLines()
    {
        if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                FinishLinesHorizontally(lastSymbolLine);
            }
            else
            {
                var penultimateSymbolLine = symbolLines[^2];
                FinishLinesVertically(penultimateSymbolLine, lastSymbolLine);
            }
        }
        else
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var penultimateSymbolLine = symbolLines[^2];
                FinishLinesHorizontally(penultimateSymbolLine, lastSymbolLine);
            }
            else
            {
                var penultimateSymbolLine = symbolLines[^2];
                FinishLinesVertically(penultimateSymbolLine);
            }
        }

        return symbolLines;
    }

    private void FinishLinesHorizontally(SymbolLineModel penultimateLine, SymbolLineModel? lastLineSymbol = null)
    {
        penultimateLine.X2 = _finalCoordinate.x;
        var coordinatePenultimateLine = (penultimateLine.X2, penultimateLine.Y2);
        FinishDrawLastLine(coordinatePenultimateLine, lastLineSymbol);
    }

    private void FinishLinesVertically(SymbolLineModel penultimateLine, SymbolLineModel? lastLineSymbol = null)
    {
        penultimateLine.Y2 = _finalCoordinate.y;
        var coordinatePenultimateLine = (penultimateLine.X2, penultimateLine.Y2);
        FinishDrawLastLine(coordinatePenultimateLine, lastLineSymbol);
    }

    private void FinishDrawingParallelSides((int x, int y) cordinatePenultimateLine, (int x, int y) finalCoordinate)
    {
        if (lastSymbolLine.X2 == finalCoordinate.x || lastSymbolLine.Y2 == finalCoordinate.y)
        {
            lastSymbolLine.X2 = finalCoordinate.x;
            lastSymbolLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new SymbolLineModel
            {
                X1 = lastSymbolLine.X2,
                Y1 = lastSymbolLine.Y2,
                X2 = cordinatePenultimateLine.x,
                Y2 = cordinatePenultimateLine.y
            };

            symbolLines.Add(firstLine);

            var coordinatelineSymbolModel = (firstLine.X2, firstLine.Y2);
            FinishDrawLastLine(coordinatelineSymbolModel);
        }
    }

    private void FinishDrawLastLine((int x, int y) penultimateLineCoordinate, SymbolLineModel? lineSymbolModel = null)
    {
        if (lineSymbolModel == null)
        {
            lineSymbolModel = new SymbolLineModel();
            symbolLines.Add(lineSymbolModel);
        }

        lineSymbolModel.X1 = penultimateLineCoordinate.x;
        lineSymbolModel.Y1 = penultimateLineCoordinate.y;
        lineSymbolModel.X2 = _finalCoordinate.x;
        lineSymbolModel.Y2 = _finalCoordinate.y;
    }
}