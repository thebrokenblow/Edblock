using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

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

        (x, y) = ChangeStartCoordinateLine(x, y, PositionConnectionPoint);

        X1 = x;
        Y1 = y;
        X2 = X1;
        Y2 = Y1;
    }

    public static (int, int) ChangeStartCoordinateLine(int x, int y, PositionConnectionPoint positionConnectionPoint)
    {
        if (positionConnectionPoint == PositionConnectionPoint.Bottom)
        {
            x += ConnectionPointModel.diametr / 2;
            y -= ConnectionPointModel.offsetPosition;
        }
        else if (positionConnectionPoint == PositionConnectionPoint.Top)
        {
            x += ConnectionPointModel.diametr / 2;
            y += ConnectionPointModel.offsetPosition * 2;
        }
        else if (positionConnectionPoint == PositionConnectionPoint.Right)
        {
            x -= ConnectionPointModel.diametr / 2;
            y += ConnectionPointModel.offsetPosition;
        }
        else
        {
            x += ConnectionPointModel.offsetPosition * 2;
            y += ConnectionPointModel.diametr;
        }

        x = CanvasSymbols.СorrectionCoordinateSymbol(x);
        y = CanvasSymbols.СorrectionCoordinateSymbol(y);

        return (x, y);
    }
}