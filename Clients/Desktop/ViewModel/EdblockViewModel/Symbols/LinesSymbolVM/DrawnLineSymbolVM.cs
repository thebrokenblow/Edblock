using System.Collections.ObjectModel;
using EdblockModel.EnumsModel;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols.LinesSymbolVM;

public class DrawnLineSymbolVM
{
    public ObservableCollection<LineSymbolVM> LinesSymbolVM { get; set; } = [];
    public ConnectionPointVM? OutgoingConnectionPoint { get; set; }
    public void CreateFirstLine(ConnectionPointVM? outgoingConnectionPoint, int startXCoordinate, int startYCoordinate)
    {
        OutgoingConnectionPoint = outgoingConnectionPoint;
        var firstLine = new LineSymbolVM()
        {
            X1 = startXCoordinate,
            Y1 = startYCoordinate,
            X2 = startXCoordinate,
            Y2 = startYCoordinate,
        };

        LinesSymbolVM.Add(firstLine);
    }

    public void DrawnLine(int xCoordinate, int yCoordinate)
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

    public void FinishDrawing(SideSymbol position, int xCoordinateDranLine, int yCoordinateDranLine)
    {
        if (OutgoingConnectionPoint is null)
        {
            return;
        }

        var lastLine = LinesSymbolVM[^1];

        if (LinesSymbolVM.Count > 1)
        {
            if (OutgoingConnectionPoint.Position == SideSymbol.Left || OutgoingConnectionPoint.Position == SideSymbol.Right)
            {
                var penultimateLine = LinesSymbolVM[^2];
                lastLine.X1 = xCoordinateDranLine;
                penultimateLine.X2 = xCoordinateDranLine;
            }
            else
            {
                var penultimateLine = LinesSymbolVM[^2];
                lastLine.Y1 = yCoordinateDranLine;
                penultimateLine.Y2 = yCoordinateDranLine;
            }
        }

        lastLine.X2 = xCoordinateDranLine;
        lastLine.Y2 = yCoordinateDranLine;
    }
}