using System.Collections.ObjectModel;
using EdblockModel.EnumsModel;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols.LinesSymbolVM;

public class DrawnLineSymbolVM
{
    public ObservableCollection<LineSymbolVM> LinesSymbolVM { get; set; } = [];
    public ConnectionPointVM? OutgoingConnectionPoint { get; set; }
    public BlockSymbolVM? OutgoingBlockSymbol { get; set; }
    public BlockSymbolVM? IncommingConnectionPoint { get; set; }

    public void CreateFirstLine(ConnectionPointVM? outgoingConnectionPoint, double startXCoordinate, double startYCoordinate)
    {
        if (LinesSymbolVM.Count % 2 == 0)
        {
            OutgoingConnectionPoint = outgoingConnectionPoint;
            OutgoingBlockSymbol = outgoingConnectionPoint?.BlockSymbolVM;

            var firstLine = new LineSymbolVM()
            {
                X1 = startXCoordinate,
                Y1 = startYCoordinate,
                X2 = startXCoordinate,
                Y2 = startYCoordinate,
            };

            LinesSymbolVM.Add(firstLine);
        }
    }

    public void DrawnLine(double xCoordinate, double yCoordinate)
    {
        if (OutgoingConnectionPoint is null)
        {
            return;
        }

        if (OutgoingConnectionPoint.Position == SideSymbol.Right || OutgoingConnectionPoint.Position == SideSymbol.Left)
        {
            if (LinesSymbolVM.Count % 2 == 1)
            {
                var firstLine = LinesSymbolVM[^1];
                firstLine.X2 = xCoordinate;

                if (yCoordinate != firstLine.Y1)
                {
                    var secondLine = new LineSymbolVM()
                    {
                        X1 = firstLine.X2,
                        Y1 = firstLine.Y2,
                        X2 = firstLine.X2,
                        Y2 = firstLine.Y2
                    };
                    LinesSymbolVM.Add(secondLine);
                }
            }
            else if (LinesSymbolVM.Count % 2 == 0)
            {
                var firstLine = LinesSymbolVM[^2];
                firstLine.X2 = xCoordinate;

                if (yCoordinate == firstLine.Y1)
                {
                    LinesSymbolVM.Remove(LinesSymbolVM[^1]);
                }
                else
                {
                    var secondLine = LinesSymbolVM[^1];
                    secondLine.X1 = xCoordinate;
                    secondLine.X2 = xCoordinate;
                    secondLine.Y2 = yCoordinate;
                }
            }
        }
        else
        {
            if (LinesSymbolVM.Count % 2 == 1)
            {
                var firstLine = LinesSymbolVM[^1];
                firstLine.Y2 = yCoordinate;

                if (xCoordinate != firstLine.X1)
                {
                    var secondLine = new LineSymbolVM()
                    {
                        X1 = firstLine.X2,
                        Y1 = firstLine.Y2,
                        X2 = firstLine.X2,
                        Y2 = firstLine.Y2
                    };
                    LinesSymbolVM.Add(secondLine);
                }
            }
            else if (LinesSymbolVM.Count % 2 == 0)
            {
                var firstLine = LinesSymbolVM[^2];
                firstLine.Y2 = yCoordinate;

                if (xCoordinate == firstLine.X1)
                {
                    LinesSymbolVM.Remove(LinesSymbolVM[^1]);
                }
                else
                {
                    var secondLine = LinesSymbolVM[^1];
                    secondLine.Y1 = yCoordinate;
                    secondLine.Y2 = yCoordinate;
                    secondLine.X2 = xCoordinate;
                }
            }
        }
    }

    public void FinishDrawing(ConnectionPointVM incommingConnectionPoint, double xCoordinateDranLine, double yCoordinateDranLine)
    {
        IncommingConnectionPoint = incommingConnectionPoint.BlockSymbolVM;

        if (OutgoingConnectionPoint is null)
        {
            return;
        }


        if (LinesSymbolVM.Count % 2 == 0)
        {
            var lastLine = LinesSymbolVM[^1];
            var penultimateLine = LinesSymbolVM[^2];

            if (incommingConnectionPoint.Position == SideSymbol.Left || incommingConnectionPoint.Position == SideSymbol.Right)
            {
                if (lastLine.LineIsVertical())
                {
                    lastLine.Y2 = yCoordinateDranLine;
                    var line = new LineSymbolVM()
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = xCoordinateDranLine,
                        Y2 = yCoordinateDranLine,
                    };
                    LinesSymbolVM.Add(line);
                }
                else
                {
                    penultimateLine.Y2 = yCoordinateDranLine;

                    lastLine.Y1 = yCoordinateDranLine;
                    lastLine.X2 = xCoordinateDranLine;
                    lastLine.Y2 = yCoordinateDranLine;
                }
            }
            else
            {
                if (lastLine.LineIsHorizontal())
                {
                    lastLine.X2 = xCoordinateDranLine;
                    var line = new LineSymbolVM()
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = xCoordinateDranLine,
                        Y2 = yCoordinateDranLine,
                    };
                    LinesSymbolVM.Add(line);
                }
                else
                {
                    penultimateLine.X2 = xCoordinateDranLine;

                    lastLine.X1 = xCoordinateDranLine;
                    lastLine.X2 = xCoordinateDranLine;
                    lastLine.Y2 = yCoordinateDranLine;
                }
            }
        }
        else
        {
            var lastLine = LinesSymbolVM[^1];

            if (incommingConnectionPoint.Position == SideSymbol.Left || incommingConnectionPoint.Position == SideSymbol.Right)
            {
                if (lastLine.LineIsVertical())
                {
                    var firstLine = new LineSymbolVM()
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = xCoordinateDranLine,
                        Y2 = yCoordinateDranLine,
                    };
                    LinesSymbolVM.Add(firstLine);
                }
                else
                {
                    var secondLine = new LineSymbolVM()
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = lastLine.X2,
                        Y2 = yCoordinateDranLine
                    };

                    var firstLine = new LineSymbolVM()
                    {
                        X1 = lastLine.X2,
                        Y1 = yCoordinateDranLine,
                        X2 = xCoordinateDranLine,
                        Y2 = yCoordinateDranLine,
                    };

                    LinesSymbolVM.Add(secondLine);
                    LinesSymbolVM.Add(firstLine);
                }
            }
            else
            {
                if (lastLine.LineIsHorizontal())
                {
                    var firstLine = new LineSymbolVM()
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = xCoordinateDranLine,
                        Y2 = yCoordinateDranLine,
                    };
                    LinesSymbolVM.Add(firstLine);
                }
                else
                {
                    var secondLine = new LineSymbolVM()
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = xCoordinateDranLine,
                        Y2 = lastLine.Y2
                    };

                    var firstLine = new LineSymbolVM()
                    {
                        X1 = xCoordinateDranLine,
                        Y1 = lastLine.Y2,
                        X2 = xCoordinateDranLine,
                        Y2 = yCoordinateDranLine,
                    };

                    LinesSymbolVM.Add(secondLine);
                    LinesSymbolVM.Add(firstLine);
                }
            }
        }
    }
}