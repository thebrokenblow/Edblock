using EdblockModel.EnumsModel;

namespace EdblockModel.SymbolsModel.LineSymbolsModel;

public class DrawnLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbolModel { get; set; }
    public BlockSymbolModel? SymbolOutgoingLine { get; set; }
    public BlockSymbolModel? SymbolIncomingLine { get; set; }
    public CoordinateLineModel CoordinateLineModel { get; set; }
    public SideSymbol? OutgoingPosition { get; set; }
    public SideSymbol? IncomingPosition { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;

    private readonly int offsetLine = 10;

    public DrawnLineSymbolModel()
    {
        LinesSymbolModel = new();
        CoordinateLineModel = new(LinesSymbolModel);
    }

    public (double x, double y) RoundingCoordinatesLines((double x, double y) startCoordinate, (double x, double y) currentCoordinate)
    {
        if (OutgoingPosition == SideSymbol.Left || OutgoingPosition == SideSymbol.Right)
        {
            currentCoordinate = HorizontalRounding(startCoordinate, currentCoordinate);
        }
        else
        {
            currentCoordinate = VerticallyRounding(startCoordinate, currentCoordinate);
        }

        return currentCoordinate;
    }

    public void ChangeCoordinateLine((double x, double y) currentCoordinte)
    {
        if (OutgoingPosition == SideSymbol.Bottom || OutgoingPosition == SideSymbol.Top)
        {
            CoordinateLineModel.ChangeCoordinatesVerticalLines(currentCoordinte);
        }
        else
        {
            CoordinateLineModel.ChangeCoordinatesHorizontalLines(currentCoordinte);
        }
    }

    public void AddFirstLine(double xCoordinateLineDraw, double yCoordinateLineDrawe)
    {
        if (SymbolOutgoingLine != null)
        {
            var firstLineSymbolModel = new LineSymbolModel
            {
                X1 = xCoordinateLineDraw,
                Y1 = yCoordinateLineDrawe,
                X2 = xCoordinateLineDraw,
                Y2 = yCoordinateLineDrawe,
            };

            LinesSymbolModel.Add(firstLineSymbolModel);
        }
    }

    public LineSymbolModel GetNewLine()
    {
        var lastLineSymbol = LinesSymbolModel[^1];
        var newLineSymbolModel = FactoryLineSymbolModel.CreateLineByPreviousLine(lastLineSymbol);

        LinesSymbolModel.Add(newLineSymbolModel);

        return newLineSymbolModel;
    }

    private (double x, double y) HorizontalRounding((double x, double y) startCoordinate, (double x, double y) currentCoordinate)
    {
        if (LinesSymbolModel.Count % 2 == 1)
        {
            if (startCoordinate.x > currentCoordinate.x)
            {
                currentCoordinate.x += offsetLine;
            }
        }
        else
        {
            if (startCoordinate.y - offsetLine > currentCoordinate.y)
            {
                currentCoordinate.y += offsetLine;
            }
        }

        return currentCoordinate;
    }

    private (double x, double y) VerticallyRounding((double x, double y) startCoordinate, (double x, double y) currentCoordinate)
    {
        if (LinesSymbolModel.Count % 2 == 1)
        {
            if (startCoordinate.y > currentCoordinate.y)
            {
                currentCoordinate.y += offsetLine;
            }
        }
        else
        {
            if (startCoordinate.x - offsetLine > currentCoordinate.x)
            {
                currentCoordinate.x += offsetLine;
            }
        }

        return currentCoordinate;
    }

    public bool IsLineOutputAccordingGOST()
    {
        if (OutgoingPosition is SideSymbol.Bottom or SideSymbol.Right)
        {
            return true;
        }

        return false;
    }

    public bool IsLineIncomingAccordingGOST()
    {
        if (IncomingPosition is SideSymbol.Top or SideSymbol.Left)
        {
            return true;
        }

        return false;
    }
}