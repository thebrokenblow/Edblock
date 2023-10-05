using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols.LineSymbols;

public class LineSymbolModel
{
    public int X1 { get; set; }

    public int Y1 { get; set; }

    public int X2 { get; set; }

    public int Y2 { get; set; }

    public PositionConnectionPoint PositionConnectionPoint { get; init; }

    public LineSymbolModel(PositionConnectionPoint positionConnectionPoint) =>
        PositionConnectionPoint = positionConnectionPoint;

    public void SetStarCoordinate(int xCoordinateCP, int yCoordinateCP, BlockSymbolModel blockSymbolModel)
    {
        int x = xCoordinateCP + blockSymbolModel.X;
        int y = yCoordinateCP + blockSymbolModel.Y;

        (x, y) = ChangeStartCoordinateLine(x, y);

        X1 = CanvasSymbols.СorrectionCoordinateSymbol(x);
        Y1 = CanvasSymbols.СorrectionCoordinateSymbol(y);
        X2 = X1;
        Y2 = Y1;
    }

    private (int, int) ChangeStartCoordinateLine(int x, int y)
    {
        if (PositionConnectionPoint == PositionConnectionPoint.Bottom)
        {
            x += ConnectionPointModel.diametr / 2;
            y -= ConnectionPointModel.offsetPosition;
        }
        else if (PositionConnectionPoint == PositionConnectionPoint.Top)
        {
            x += ConnectionPointModel.diametr / 2;
            y += ConnectionPointModel.offsetPosition * 2;
        }
        else if (PositionConnectionPoint == PositionConnectionPoint.Right)
        {
            x -= ConnectionPointModel.diametr / 2;
            y += ConnectionPointModel.offsetPosition;
        }
        else
        {
            x += ConnectionPointModel.offsetPosition * 2;
            y += ConnectionPointModel.diametr;
        }

        return (x, y);
    }
}