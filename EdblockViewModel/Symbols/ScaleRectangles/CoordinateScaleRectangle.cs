using System.Windows;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class CoordinateScaleRectangle
{
    public int WidthSymbol { get; set; }
    public int HeightSymbol { get; set; }

    public CoordinateScaleRectangle(int widthSymbol, int heightSymbol)
    {
        WidthSymbol = widthSymbol;
        HeightSymbol = heightSymbol;
    }

    public Point GetCoordinateLeftTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(-widthScaleRectangle, -heightScaleRectangle);
    }

    public Point GetCoordinateLeftMiddleRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(-widthScaleRectangle, HeightSymbol / 2 - heightScaleRectangle);
    }

    public Point GetCoordinateLeftBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(-widthScaleRectangle, HeightSymbol - heightScaleRectangle);
    }

    public Point GetCoordinateRightTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(WidthSymbol - widthScaleRectangle, -heightScaleRectangle);
    }

    public Point GetCoordinateRightMiddleRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(WidthSymbol - widthScaleRectangle, HeightSymbol / 2 - heightScaleRectangle);
    }

    public Point GetCoordinateRightBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(WidthSymbol - widthScaleRectangle, HeightSymbol - heightScaleRectangle);
    }

    public Point GetCoordinateMiddleBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(WidthSymbol / 2 - widthScaleRectangle, HeightSymbol - heightScaleRectangle);
    }

    public Point GetCoordinateMiddleTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(WidthSymbol / 2 - widthScaleRectangle, -heightScaleRectangle);
    }
}