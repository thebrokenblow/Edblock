using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using EdblockModel.EnumsModel;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using Prism.Commands;

namespace EdblockViewModel.Symbols.LinesSymbolVM;

public class DrawnLineSymbolVM
{
    public ObservableCollection<LineSymbolVM> LinesSymbolVM { get; set; } = [];
    public ConnectionPointVM? OutgoingConnectionPoint { get; set; }
    public ConnectionPointVM? IncommingConnectionPoint { get; set; }
    public BlockSymbolVM? OutgoingBlockSymbol { get; set; }

    public DelegateCommand HighlightDrawnLineCommand { get; }
    public DelegateCommand UnhighlightDrawnLineCommand { get; }
    public ICanvasSymbolsComponentVM CanvasSymbolsComponentVM { get; }

    private readonly Brush? selectedBrush;
    public DrawnLineSymbolVM(ICanvasSymbolsComponentVM canvasSymbolsComponentVM)
    {
        var selectedBrushConverter = new BrushConverter().ConvertFrom("#00FF00");

        if (selectedBrushConverter is not null)
        {
            selectedBrush = (Brush)selectedBrushConverter;
        }

        CanvasSymbolsComponentVM = canvasSymbolsComponentVM;

        HighlightDrawnLineCommand = new(HighlightDrawnLine);
        UnhighlightDrawnLineCommand = new(UnhighlightDrawnLine);
    }

    public bool IsSelect { get; set; }

    private void HighlightDrawnLine()
    {
        if (selectedBrush is null || CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM == this || IsSelect)
        {
            return;
        }

        foreach (var lineSymbolVM in LinesSymbolVM)
        {
            lineSymbolVM.Stroke = selectedBrush;
        }

        CanvasSymbolsComponentVM.Cursor = Cursors.Hand;
    }

    private void UnhighlightDrawnLine()
    {
        if (CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM == this || IsSelect)
        {
            return;
        }

        foreach (var lineSymbolVM in LinesSymbolVM)
        {
            lineSymbolVM.Stroke = Brushes.Black;
        }

        CanvasSymbolsComponentVM.Cursor = Cursors.Arrow;
    }

    public void CreateFirstLine(ConnectionPointVM? outgoingConnectionPoint, double startXCoordinate, double startYCoordinate)
    {
        OutgoingConnectionPoint = outgoingConnectionPoint;
        OutgoingBlockSymbol = outgoingConnectionPoint?.BlockSymbolVM;

        var firstLine = new LineSymbolVM(this)
        {
            X1 = startXCoordinate,
            Y1 = startYCoordinate,
            X2 = startXCoordinate,
            Y2 = startYCoordinate,
        };

        LinesSymbolVM.Add(firstLine);
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
                    var secondLine = new LineSymbolVM(this)
                    {
                        X1 = firstLine.X2,
                        Y1 = firstLine.Y2,
                        X2 = firstLine.X2,
                        Y2 = firstLine.Y2
                    };
                    LinesSymbolVM.Add(secondLine);
                }
            }
            else
            {
                var lastLine = LinesSymbolVM[^1];
                var penultimateLine = LinesSymbolVM[^2];

                penultimateLine.X2 = xCoordinate;

                if (yCoordinate == lastLine.Y1)
                {
                    LinesSymbolVM.Remove(lastLine);
                }
                else
                {
                    lastLine.X1 = xCoordinate;
                    lastLine.X2 = xCoordinate;
                    lastLine.Y2 = yCoordinate;
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
                    var secondLine = new LineSymbolVM(this)
                    {
                        X1 = firstLine.X2,
                        Y1 = firstLine.Y2,
                        X2 = firstLine.X2,
                        Y2 = firstLine.Y2
                    };
                    LinesSymbolVM.Add(secondLine);
                }
            }
            else
            {
                var lastLine = LinesSymbolVM[^1];
                var penultimateLine = LinesSymbolVM[^2];
                penultimateLine.Y2 = yCoordinate;

                if (xCoordinate == lastLine.X1)
                {
                    LinesSymbolVM.Remove(lastLine);
                }
                else
                {
                    lastLine.Y1 = yCoordinate;
                    lastLine.Y2 = yCoordinate;
                    lastLine.X2 = xCoordinate;
                }
            }
        }
    }

    public void FinishDrawing(ConnectionPointVM incommingConnectionPoint, double xCoordinateDranLine, double yCoordinateDranLine)
    {
        IncommingConnectionPoint = incommingConnectionPoint;

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
                    var line = new LineSymbolVM(this)
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
                    var line = new LineSymbolVM(this)
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
                    var firstLine = new LineSymbolVM(this)
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
                    var secondLine = new LineSymbolVM(this)
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = lastLine.X2,
                        Y2 = yCoordinateDranLine
                    };

                    var firstLine = new LineSymbolVM(this)
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
                    var firstLine = new LineSymbolVM(this)
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
                    var secondLine = new LineSymbolVM(this)
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = xCoordinateDranLine,
                        Y2 = lastLine.Y2
                    };

                    var firstLine = new LineSymbolVM(this)
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

    public void Select()
    {
        CanvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.SelectedDrawnLinesVM.Add(this);
        IsSelect = true;
    }

    public void Unselect()
    {
        IsSelect = false;

        foreach(var linesSymbolVM in LinesSymbolVM)
        {
            linesSymbolVM.Stroke = Brushes.Black;
        }
    }
}