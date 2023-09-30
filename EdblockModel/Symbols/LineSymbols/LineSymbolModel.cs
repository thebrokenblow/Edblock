using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols.LineSymbols;

public enum Orientation
{
    Vertical,
    Horizontal
}

public class LineSymbolModel
{
    public int X1 { get; set; }

    public int Y1 { get; set; }

    public int X2 { get; set; }

    public int Y2 { get; set; }

    public Orientation Orientation { get; init; }

    public LineSymbolModel(Orientation orientation) =>
        Orientation = orientation;

    public void SetStarCoordinate(int xCoordinateCP, int yCoordinateCP, BlockSymbolModel blockSymbolModel)
    {
        int x = xCoordinateCP + blockSymbolModel.X;
        int y = yCoordinateCP + blockSymbolModel.Y;

        if (Orientation == Orientation.Vertical)
        {
            x += (int)ConnectionPointModel.DIAMETR / 2;
            y -= ConnectionPointModel.OFFSET;
        }
        else
        {
            x -= (int)ConnectionPointModel.DIAMETR / 2;
            y += ConnectionPointModel.OFFSET;
        }

        X1 = CanvasSymbols.СorrectionCoordinateSymbol(x);
        Y1 = CanvasSymbols.СorrectionCoordinateSymbol(y);
        X2 = X1;
        Y2 = Y1;
    }
}