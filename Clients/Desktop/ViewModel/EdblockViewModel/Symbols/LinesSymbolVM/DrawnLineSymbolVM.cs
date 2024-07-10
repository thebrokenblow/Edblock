using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using EdblockModel.EnumsModel;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using Prism.Commands;
using EdblockViewModel.Symbols.LinesSymbolVM.Components;
using EdblockViewModel.Core;
using EdblockModel.Lines;

namespace EdblockViewModel.Symbols.LinesSymbolVM;

/**
 * View Model of one line 
 */
public class DrawnLineSymbolVM : ObservableObject
{
    public ObservableCollection<LineSymbolVM> LinesSymbolVM { get; } = [];
    public ObservableCollection<MovableRectangleLineVM> MovableRectanglesLineVM { get; } = [];
    public ConnectionPointVM? OutgoingConnectionPoint { get; set; }
    public ConnectionPointVM? IncommingConnectionPoint { get; set; }
    public BlockSymbolVM? OutgoingBlockSymbol { get; set; }
    public BlockSymbolVM? IncommingBlockSymbol { get; set; }
    private readonly DrawnLineSymbolModel drawnLineSymbolModel = new(); 

    public ICanvasSymbolsComponentVM CanvasSymbolsComponentVM { get; }

    public DelegateCommand HighlightDrawnLineCommand { get; }
    public DelegateCommand UnhighlightDrawnLineCommand { get; }

    private readonly Brush? selectedBrush;      //TODO: явно не принадлежит VM
    private const string highlightStroke = "#00FF00";
    public DrawnLineSymbolVM(ICanvasSymbolsComponentVM canvasSymbolsComponentVM)
    {
        var selectedBrushConverter = new BrushConverter().ConvertFrom(highlightStroke);

        if (selectedBrushConverter is not null)
        {
            selectedBrush = (Brush)selectedBrushConverter;
        }

        CanvasSymbolsComponentVM = canvasSymbolsComponentVM;

        HighlightDrawnLineCommand = new(HighlightDrawnLine);
        UnhighlightDrawnLineCommand = new(UnhighlightDrawnLine);
    }

    private const string defaultText = "да";

    private string? text;
    public string? Text
    {
        get => text;
        set
        {
            text = value;
            drawnLineSymbolModel.Text = text;
        }
    }

    private const int heightTextField = 20;
    public static int HeightTextField
    {
        get => heightTextField;
    }

    private double widthTextField = 20;
    public double WidthTextField
    {
        get => widthTextField;
        set
        {
            widthTextField = value;

            if (OutgoingConnectionPoint?.Position == SideSymbol.Left)
            {
                SetCoordinateTextField();
            }
        }
    }

    public void SetCoordinateTextField()
    {
        var firstLineSymbolModel = LinesSymbolVM[0];

        LeftOffsetTextField = firstLineSymbolModel.X1;
        TopOffsetTextField = firstLineSymbolModel.Y1;

        if (OutgoingConnectionPoint?.Position != SideSymbol.Bottom)
        {
            TopOffsetTextField -= heightTextField;
        }

        if (OutgoingConnectionPoint?.Position == SideSymbol.Left)
        {
            LeftOffsetTextField -= widthTextField;
        }
    }

    private double topOffsetTextField;
    public double TopOffsetTextField
    {
        get => topOffsetTextField;
        set
        {
            topOffsetTextField = value;
            OnPropertyChanged();
        }
    }

    private double leftOffsetTextField;
    public double LeftOffsetTextField
    {
        get => leftOffsetTextField;
        set
        {
            leftOffsetTextField = value;
            OnPropertyChanged();
        }
    }

    private bool isShowTextField;
    public bool IsShowTextField
    {
        get => isShowTextField;
        set
        {
            isShowTextField = value;
            OnPropertyChanged();
        }
    }


    public bool IsSelect { get; set; }

    private void HighlightDrawnLine()
    {
        if (selectedBrush is null || CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM == this || IsSelect)
        {
            return;
        }

        SetStroke(selectedBrush);

        foreach (var movableRectangleLineVM in MovableRectanglesLineVM)
        {
            if (!movableRectangleLineVM.LinesSymbolVM.IsZero())
            {
                movableRectangleLineVM.IsShow = true;
            }

        }

        if (CanvasSymbolsComponentVM.Cursor != Cursors.SizeWE && CanvasSymbolsComponentVM.Cursor != Cursors.SizeNS)
        {
            CanvasSymbolsComponentVM.Cursor = Cursors.Hand;
        }
    }

    private void UnhighlightDrawnLine()
    {
        if (CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM == this || IsSelect || CanvasSymbolsComponentVM.MovableRectangleLineVM is not null)
        {
            return;
        }

        SetStroke(Brushes.Black);
        
        foreach (var movableRectangleLineVM in MovableRectanglesLineVM)
        {
            movableRectangleLineVM.IsShow = false;
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
        IncommingBlockSymbol = incommingConnectionPoint.BlockSymbolVM;

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
                if (lastLine.IsVertical())
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
                if (lastLine.IsHorizontal())
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
                if (lastLine.IsVertical())
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
                if (lastLine.IsHorizontal())
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

        SetMovableRectangle();
    }

    private void SetMovableRectangle()
    {
        for (int i = 1; i < LinesSymbolVM.Count - 1; i++)
        {
            var lineSymbolVM = LinesSymbolVM[i];
            var movableRectangleLineVM = new MovableRectangleLineVM(lineSymbolVM);
            MovableRectanglesLineVM.Add(movableRectangleLineVM);
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
        SetStroke(Brushes.Black);

        foreach (var movableRectangleLineVM in MovableRectanglesLineVM)
        {
            movableRectangleLineVM.IsShow = false;
        }
    }

    private void SetStroke(Brush brushLine)
    {
        foreach (var linesSymbolVM in LinesSymbolVM)
        {
            linesSymbolVM.Stroke = brushLine;
        }
    }

    public void RemoveZeroLines()
    {
        for (int i = LinesSymbolVM.Count - 1; i != -1; i--)
        {
            if (LinesSymbolVM[i].IsZero())
            {
                LinesSymbolVM.RemoveAt(i);
            }

            if (i >= 0 && i < LinesSymbolVM.Count && i - 1 >= 0 && i - 1 < LinesSymbolVM.Count)
            {
                if (LinesSymbolVM[i].IsHorizontal() && LinesSymbolVM[i - 1].IsHorizontal())
                {
                    LinesSymbolVM[i - 1].X2 = LinesSymbolVM[i].X2;
                    LinesSymbolVM.RemoveAt(i);
                }   
            }

            if (i >= 0 && i < LinesSymbolVM.Count && i - 1 >= 0 && i - 1 < LinesSymbolVM.Count)
            {
                if (LinesSymbolVM[i].IsVertical() && LinesSymbolVM[i - 1].IsVertical())
                {
                    LinesSymbolVM[i - 1].Y2 = LinesSymbolVM[i].Y2;
                    LinesSymbolVM.RemoveAt(i);
                }
            }
        }
    }
}