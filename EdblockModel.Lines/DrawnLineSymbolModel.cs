using EdblockModel.Lines.Factories;
using EdblockModel.Lines.DecoratorLine;
using EdblockModel.Lines.DecoratorLine.Interfaces;
using EdblockModel.Lines.Extensions;

namespace EdblockModel.Lines;

public class DrawnLineSymbolModel
{
    public List<LineModel> Lines { get; set; } = [];
    public ArrowLineModel ArrowLineModel { get; set; } = new();
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
                var lastLine = Lines.Last();
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
                var lastLine = Lines.Last();
                var penultimateLine = Lines.Penultimate();
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
                var lastLine = Lines.Last();
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
                var lastLine = Lines.Last();
                var penultimateLine = Lines.Penultimate();
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


        ArrowLineModel.Redraw(
            Lines.Last().SecondCoordinate.X, 
            Lines.Last().SecondCoordinate.Y, 
            GetLineDirection(Lines.Last()));

        return Lines;
    }

    private LineDirection GetLineDirection(LineModel lastLine)
    {
        if (lastLine.IsZero())
        {
            if (OutgoingPosition == SideSymbol.Left)
            {
                return LineDirection.Left;
            }
            if (OutgoingPosition == SideSymbol.Right)
            {
                return LineDirection.Right;
            }
            if (OutgoingPosition == SideSymbol.Top)
            {
                return LineDirection.Top;
            }
            
            return LineDirection.Bottom;
        }

        if (lastLine.IsHorizontal())
        {
            if (lastLine.SecondCoordinate.X < lastLine.FirstCoordinate.X)
            {
                return LineDirection.Left;
            }

            return LineDirection.Right;
        }

        if (lastLine.SecondCoordinate.Y > lastLine.FirstCoordinate.Y)
        {
            return LineDirection.Bottom;
        }

        return LineDirection.Top;
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

        if (incommingPosition == SideSymbol.Left)
        {
            ArrowLineModel.Redraw(finishXCoordinate, finishYCoordinate, LineDirection.Right);
        }

        if (incommingPosition == SideSymbol.Right)
        {
            ArrowLineModel.Redraw(finishXCoordinate, finishYCoordinate, LineDirection.Left);
        }

        if (incommingPosition == SideSymbol.Top)
        {
            ArrowLineModel.Redraw(finishXCoordinate, finishYCoordinate, LineDirection.Bottom);
        }

        if (incommingPosition == SideSymbol.Bottom)
        {
            ArrowLineModel.Redraw(finishXCoordinate, finishYCoordinate, LineDirection.Top);
        }

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