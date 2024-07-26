namespace EdblockModel.Lines;

public class ArrowLineModel
{
    private const int heightArrow = 10;
    private const int widthArrow = 6;
    public List<(double xCoordinate, double yCoordinate)> CurrentArrowCoordinates { get; set; } = [];

    private readonly Dictionary<LineDirection, Func<double, double, IEnumerable<(double, double)>>> coordinateArrowByLineDirection;

    public ArrowLineModel()
    {
        coordinateArrowByLineDirection = new()
        {
            { LineDirection.Bottom, RedrawBottomArrow },
            { LineDirection.Top, RedrawTopArrow },
            { LineDirection.Left, RedrawLeftArrow },
            { LineDirection.Right, RedrawRightArrow }
        };
    }

    public void Redraw(double xCoordinate, double yCoodinate, LineDirection lineDirection)
    {
        var coordinatesArrow = coordinateArrowByLineDirection[lineDirection].Invoke(xCoordinate, yCoodinate);

        if (CurrentArrowCoordinates.Count == 0)
        {
            CurrentArrowCoordinates.AddRange(coordinatesArrow);
        }
        else
        {
            int numberPointArrow = 0;

            foreach (var coordinateArrow in coordinatesArrow)
            {
                CurrentArrowCoordinates[numberPointArrow++] = coordinateArrow;
            }
        }
    }

    private static IEnumerable<(double, double)> RedrawBottomArrow(double xCoordinate, double yCoordinate) =>
        [
            (xCoordinate - widthArrow / 2, yCoordinate - heightArrow),
            (xCoordinate, yCoordinate),
            (xCoordinate + widthArrow / 2, yCoordinate - heightArrow)
        ];

    private static IEnumerable<(double, double)> RedrawTopArrow(double xCoordinate, double yCoordinate) =>
        [
            (xCoordinate - widthArrow / 2, yCoordinate + heightArrow),
            (xCoordinate, yCoordinate),
            (xCoordinate + widthArrow / 2, yCoordinate + heightArrow)
        ];

    private static IEnumerable<(double, double)> RedrawLeftArrow(double xCoordinate, double yCoordinate) =>
        [
            (xCoordinate + heightArrow, yCoordinate - widthArrow / 2),
            (xCoordinate, yCoordinate),
            (xCoordinate + heightArrow, yCoordinate + widthArrow / 2)
        ];

    private static IEnumerable<(double, double)> RedrawRightArrow(double xCoordinate, double yCoordinate) =>
        [
            (xCoordinate - heightArrow, yCoordinate - widthArrow / 2),
            (xCoordinate, yCoordinate),
            (xCoordinate - heightArrow, yCoordinate + widthArrow / 2)
        ];        
}