using EdblockModel.Lines.Factories;
using EdblockModel.Lines.DecoratorLine;
using EdblockModel.Lines.DecoratorLine.Interfaces;

namespace EdblockModel.Lines;

public class DrawnLineSymbolModel
{
    public List<LineModel> Lines { get; set; } = [];
    public string Text { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public SideSymbol OutgoingPosition { get; set; }
    public SideSymbol IncommingPosition { get; set; }

    private readonly FactoryLineModel factoryLineModel = new(new FactoryCoordinateDecorator());

    public List<LineModel> Redraw(ICoordinateDecorator currentCoordinate)
    {
        if (OutgoingPosition == SideSymbol.Left || OutgoingPosition == SideSymbol.Right)
        {
            if (Lines.Count % 2 == 1)
            {
                var lastLine = Lines[^1];
                lastLine.SecondCoordinate.X = currentCoordinate.X;

                if (currentCoordinate.Y != lastLine.FirstCoordinate.Y)
                {
                    var currentLastLine = factoryLineModel.Create(
                        lastLine.SecondCoordinate.X,
                        lastLine.SecondCoordinate.Y,
                        lastLine.SecondCoordinate.X,
                        lastLine.SecondCoordinate.Y);

                    Lines.Add(currentLastLine);
                }
            }
            else
            {
                var lastLine = Lines[^1];
                var penultimateLine = Lines[^2];
                penultimateLine.SecondCoordinate.X = currentCoordinate.X;

                if (currentCoordinate.Y == lastLine.FirstCoordinate.Y)
                {
                    Lines.Remove(lastLine);
                }
                else
                {
                    lastLine.FirstCoordinate.X = currentCoordinate.X;
                    lastLine.SecondCoordinate.X = currentCoordinate.X;
                    lastLine.SecondCoordinate.Y = currentCoordinate.Y;
                }
            }
        }
        else
        {
            if (Lines.Count % 2 == 1)
            {
                var lastLine = Lines[^1];
                lastLine.SecondCoordinate.Y = currentCoordinate.Y;

                if (currentCoordinate.X != lastLine.FirstCoordinate.X)
                {
                    var currentLastLine = factoryLineModel.Create(
                        lastLine.SecondCoordinate.X,
                        lastLine.SecondCoordinate.Y,
                        lastLine.SecondCoordinate.X,
                        lastLine.SecondCoordinate.Y);

                    Lines.Add(currentLastLine);
                }
            }
            else
            {
                var lastLine = Lines[^1];
                var penultimateLine = Lines[^2];
                penultimateLine.SecondCoordinate.Y = currentCoordinate.Y;

                if (currentCoordinate.X == lastLine.FirstCoordinate.X)
                {
                    Lines.Remove(lastLine);
                }
                else
                {
                    lastLine.FirstCoordinate.Y = currentCoordinate.Y;
                    lastLine.SecondCoordinate.Y = currentCoordinate.Y;
                    lastLine.SecondCoordinate.X = currentCoordinate.X;
                }
            }
        }

        return Lines;
    }

    public List<LineModel> FinishDrawing(SideSymbol incommingPosition, double finishXCoordinate, double finishYCoordinate)
    {
        IncommingPosition = incommingPosition;
        if (Lines.Count % 2 == 0)
        {
            var lastLine = Lines[^1];
            var penultimateLine = Lines[^2];

            if (incommingPosition == SideSymbol.Left || incommingPosition == SideSymbol.Right)
            {
                if (lastLine.IsVertical())
                {
                    lastLine.SecondCoordinate.Y = finishYCoordinate;
                    var currentLastLine = factoryLineModel.Create(lastLine.SecondCoordinate.X, lastLine.SecondCoordinate.Y, finishXCoordinate, finishYCoordinate);
                    Lines.Add(currentLastLine);
                }
                else
                {
                    penultimateLine.SecondCoordinate.Y = finishYCoordinate;

                    lastLine.FirstCoordinate.Y = finishYCoordinate;
                    lastLine.SecondCoordinate.X = finishXCoordinate;
                    lastLine.SecondCoordinate.Y = finishYCoordinate;
                }
            }
            else
            {
                if (lastLine.IsHorizontal())
                {
                    lastLine.SecondCoordinate.X = finishXCoordinate;
                    var currentLastLine = factoryLineModel.Create(lastLine.SecondCoordinate.X, lastLine.SecondCoordinate.Y, finishXCoordinate, finishYCoordinate);
                    Lines.Add(currentLastLine);
                }
                else
                {
                    penultimateLine.SecondCoordinate.X = finishXCoordinate;

                    lastLine.FirstCoordinate.X = finishXCoordinate;
                    lastLine.SecondCoordinate.X = finishXCoordinate;
                    lastLine.SecondCoordinate.Y = finishYCoordinate;
                }
            }
        }
        else
        {
            var lastLine = Lines[^1];
            if (incommingPosition == SideSymbol.Left || incommingPosition == SideSymbol.Right)
            {
                if (lastLine.IsVertical())
                {
                    var currentLastLine = factoryLineModel.Create(lastLine.SecondCoordinate.X, lastLine.SecondCoordinate.Y, finishXCoordinate, finishYCoordinate);
                    Lines.Add(currentLastLine);
                }
                else
                {
                    var currentPenultimateLine = factoryLineModel.Create(lastLine.SecondCoordinate.X, lastLine.SecondCoordinate.Y, lastLine.SecondCoordinate.X, finishYCoordinate);
                    var currentLastLine = factoryLineModel.Create(lastLine.SecondCoordinate.X, finishYCoordinate, finishXCoordinate, finishYCoordinate);

                    Lines.Add(currentPenultimateLine);
                    Lines.Add(currentLastLine);
                }
            }
            else
            {
                if (lastLine.IsHorizontal())
                {
                    var currentLastLine = factoryLineModel.Create(lastLine.SecondCoordinate.X, lastLine.SecondCoordinate.Y, finishXCoordinate, finishYCoordinate);
                    Lines.Add(currentLastLine);
                }
                else
                {
                    var currentPenultimateLine = factoryLineModel.Create(lastLine.SecondCoordinate.X, lastLine.SecondCoordinate.Y, finishXCoordinate, lastLine.SecondCoordinate.Y);
                    var currentLastLine = factoryLineModel.Create(finishXCoordinate, lastLine.SecondCoordinate.Y, finishXCoordinate, finishYCoordinate);

                    Lines.Add(currentPenultimateLine);
                    Lines.Add(currentLastLine);
                }
            }
        }

        RemoveZeroLines();

        return Lines;
    }

    public void CreateFirstLine(double startXCoordinate, double startYCoordinate)
    {
        Lines.Add(new LineModel(new CoordinateDecorator(startXCoordinate, startYCoordinate), new CoordinateDecorator(startXCoordinate, startYCoordinate)));
    }

    private void RemoveZeroLines()
    {
        for (int i = Lines.Count - 1; i != -1; i--)
        {
            if (Lines[i].IsZero())
            {
                Lines.RemoveAt(i);
            }

            if (i >= 0 && i < Lines.Count && i - 1 >= 0 && i - 1 < Lines.Count)
            {
                if (Lines[i].IsHorizontal() && Lines[i - 1].IsHorizontal())
                {
                    Lines[i - 1].SecondCoordinate.X = Lines[i].SecondCoordinate.X;
                    Lines.RemoveAt(i);
                }
            }

            if (i >= 0 && i < Lines.Count && i - 1 >= 0 && i - 1 < Lines.Count)
            {
                if (Lines[i].IsVertical() && Lines[i - 1].IsVertical())
                {
                    Lines[i - 1].SecondCoordinate.Y = Lines[i].SecondCoordinate.Y;
                    Lines.RemoveAt(i);
                }
            }
        }
    }
}