using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.LineSymbols;

public class CompletedLine
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLine(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    public List<LineSymbolModel> GetCompleteLines()
    {
        var lastLine = linesSymbolModel[^1];

        if (linesSymbolModel.Count % 2 == 0)
        {
            CompletedEvenLine(lastLine);
        }
        else
        {
            CompletedOddLine(lastLine);
        }

        return linesSymbolModel;
    }

    public void CompletedEvenLine(LineSymbolModel lastLine)
    {
        if (outgoingPosition == PositionConnectionPoint.Top || outgoingPosition == PositionConnectionPoint.Bottom)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                FinishDrawingLinesVerticalSides(lastLine);
            }
            else
            {
                var coordinatePenultimateLine = (lastLine.X1, _finalCoordinate.y);
                FinishLinesNotParallelSides(lastLine, coordinatePenultimateLine);
            }
        }
        else
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var coordinatePenultimateLine = (_finalCoordinate.x, lastLine.Y1);
                FinishLinesNotParallelSides(lastLine, coordinatePenultimateLine);
            }
            else
            {
                FinishDrawingLinesHorizontalSides(lastLine);
            }
        }
    }

    public void CompletedOddLine(LineSymbolModel lastLine)
    {
        if (outgoingPosition == PositionConnectionPoint.Top || outgoingPosition == PositionConnectionPoint.Bottom)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var coordinatePenultimateLine = (_finalCoordinate.x, lastLine.Y2);
                FinishLinesParallelSides(lastLine, coordinatePenultimateLine);
            }
            else
            {
                FinishDrawingLinesHorizontalSides(lastLine);
            }
        }
        else
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                FinishDrawingLinesVerticalSides(lastLine);
            }
            else
            {
                var coordinatePenultimateLine = (lastLine.X2, _finalCoordinate.y);
                FinishLinesParallelSides(lastLine, coordinatePenultimateLine);
            }
        }
    }


    private void FinishDrawingLinesVerticalSides(LineSymbolModel lastLine)
    {
        lastLine.X2 = _finalCoordinate.x;
        lastLine = FactoryLineSymbolModel.CreateLineByPenulteLine(lastLine, _finalCoordinate);
        linesSymbolModel.Add(lastLine);
    }

    private void FinishDrawingLinesHorizontalSides(LineSymbolModel lastLine)
    {
        lastLine.Y2 = _finalCoordinate.y;
        lastLine = FactoryLineSymbolModel.CreateLineByPenulteLine(lastLine, _finalCoordinate);
        linesSymbolModel.Add(lastLine);
    }

    private void FinishLinesParallelSides(LineSymbolModel lastLine, (int X2, int Y2) coordinatePenultimateLine)
    {
        if (lastLine.X2 == _finalCoordinate.x || lastLine.Y2 == _finalCoordinate.y)
        {
            var startCoordinateLastLine = (lastLine.X1, lastLine.Y1);
            FinishDrawingLastLines(startCoordinateLastLine);
        }
        else
        {
            var penultimateLine = FactoryLineSymbolModel.CreateLineByPenulteLine(lastLine, coordinatePenultimateLine);
            linesSymbolModel.Add(penultimateLine);

            lastLine = FactoryLineSymbolModel.CreateLineByPenulteLine(penultimateLine, _finalCoordinate);
            linesSymbolModel.Add(lastLine);
        }
    }

    private void FinishLinesNotParallelSides(LineSymbolModel lastLine, (int x2, int y2) coordinatePenultimateLine)
    {
        if (lastLine.X2 == _finalCoordinate.x || lastLine.Y2 == _finalCoordinate.y)
        {
            var startCoordinateLastLine = (lastLine.X1, lastLine.Y1);
            FinishDrawingLastLines(startCoordinateLastLine);
        }
        else
        {
            var penultimateLine = linesSymbolModel[^2];

            penultimateLine.Y2 = coordinatePenultimateLine.y2;
            penultimateLine.X2 = coordinatePenultimateLine.x2;

            FinishDrawingLastLines(coordinatePenultimateLine);
        }
    }

    private void FinishDrawingLastLines((int x1, int y1) startLineCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        lastLine.X1 = startLineCoordinate.x1;
        lastLine.Y1 = startLineCoordinate.y1;

        lastLine.X2 = _finalCoordinate.x;
        lastLine.Y2 = _finalCoordinate.y;
    }
}