using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.LineSymbols;

public class CompletedLineModel
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly LineSymbolModel lastLine;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLineModel(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        lastLine = linesSymbolModel[^1];
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    public List<LineSymbolModel> GetCompleteLines()
    {
        if (linesSymbolModel.Count % 2 == 1)
        {
            if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
            {
                if (incomingPosition == PositionConnectionPoint.Top)
                {
                    FinishDrawingHorizontalToHorizontalLines(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Bottom)
                {
                    FinishDrawingHorizontalToHorizontalLines(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Left)
                {
                    FinishDrawingHorizontalToVerticalLines(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Right)
                {
                    FinishDrawingHorizontalToVerticalLines(lastLine, _finalCoordinate);
                }
            }
            else if (outgoingPosition == PositionConnectionPoint.Left || outgoingPosition == PositionConnectionPoint.Right)
            {
                if (incomingPosition == PositionConnectionPoint.Top)
                {
                    FinishDrawingVerticalToHorizontalLines(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Bottom)
                {;
                    FinishDrawingVerticalToHorizontalLines(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Left)
                {
                    FinishDrawingVerticalToVerticalLines(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Right)
                {
                    FinishDrawingVerticalToVerticalLines(lastLine, _finalCoordinate);
                }
            }
        }
        else
        {
            if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
            {
                if (incomingPosition == PositionConnectionPoint.Top)
                {
                    FinishDrawingHorizontalToHorizontalLines2(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Bottom)
                {
                    FinishDrawingHorizontalToHorizontalLines2(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Left)
                {
                    FinishDrawingHorizontalToVerticalLines2(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Right)
                {
                    FinishDrawingHorizontalToVerticalLines2(lastLine, _finalCoordinate);
                }
            }
            else if (outgoingPosition == PositionConnectionPoint.Left || outgoingPosition == PositionConnectionPoint.Right)
            {
                if (incomingPosition == PositionConnectionPoint.Top)
                {
                    FinishDrawingVerticalToHorizontalLines2(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Bottom)
                {
                    FinishDrawingVerticalToHorizontalLines2(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Left)
                {
                    FinishDrawingVerticalToVerticalLines2(lastLine, _finalCoordinate);
                }
                else if (incomingPosition == PositionConnectionPoint.Right)
                {
                    FinishDrawingVerticalToVerticalLines2(lastLine, _finalCoordinate);
                }
            }
        }

        return linesSymbolModel;
    }

    private void FinishDrawingHorizontalToHorizontalLines(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.X2 == finalCoordinate.x)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = lastLine.Y2
            };

            var secondLine = new LineSymbolModel
            {
                X1 = finalCoordinate.x,
                Y1 = lastLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            linesSymbolModel.Add(firstLine);
            linesSymbolModel.Add(secondLine);
        }
    }

    private void FinishDrawingHorizontalToVerticalLines(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.Y2 = finalCoordinate.y;

        var firstLine = new LineSymbolModel
        {
            X1 = lastLine.X2,
            Y1 = finalCoordinate.y,
            X2 = finalCoordinate.x,
            Y2 = finalCoordinate.y
        };

        linesSymbolModel.Add(firstLine);
    }

    private void FinishDrawingVerticalToHorizontalLines(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.X2 = finalCoordinate.x;

        var firstLine = new LineSymbolModel
        {
            X1 = finalCoordinate.x,
            Y1 = lastLine.Y2,
            X2 = finalCoordinate.x,
            Y2 = finalCoordinate.y
        };

        linesSymbolModel.Add(firstLine);
    }

    private void FinishDrawingVerticalToVerticalLines(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.Y2 == finalCoordinate.y)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = lastLine.X2,
                Y2 = finalCoordinate.y
            };

            var secondLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = finalCoordinate.y,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            linesSymbolModel.Add(firstLine);
            linesSymbolModel.Add(secondLine);
        }
    }

    private void FinishDrawingHorizontalToHorizontalLines2(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.X2 = finalCoordinate.x;

        var firstLine = new LineSymbolModel
        {
            X1 = finalCoordinate.x,
            Y1 = lastLine.Y2,
            Y2 = finalCoordinate.y,
            X2 = finalCoordinate.x
        };

        linesSymbolModel.Add(firstLine);
    }

    private void FinishDrawingHorizontalToVerticalLines2(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.Y2 == finalCoordinate.y)
        {
            lastLine.X2 = finalCoordinate.x;
        }
        else
        {
            var secondLine = linesSymbolModel[^2];

            secondLine.Y2 = finalCoordinate.y;

            lastLine.Y1 = finalCoordinate.y;
            lastLine.Y2 = finalCoordinate.y;
            lastLine.X2 = finalCoordinate.x;
        }
    }
    private void FinishDrawingVerticalToHorizontalLines2(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        if (finalCoordinate.x == lastLine.X2)
        {
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var secondLine = linesSymbolModel[^2];

            secondLine.X2 = finalCoordinate.x;

            lastLine.X1 = finalCoordinate.x;
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
    }

    private void FinishDrawingVerticalToVerticalLines2(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        lastLine.Y2 = finalCoordinate.y;

        var firstLine = new LineSymbolModel
        {
            X1 = lastLine.X2,
            Y1 = lastLine.Y2,
            X2 = finalCoordinate.x,
            Y2 = lastLine.Y2
        };

        linesSymbolModel.Add(firstLine);
    }
}