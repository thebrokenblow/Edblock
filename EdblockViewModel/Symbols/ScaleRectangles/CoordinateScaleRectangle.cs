using System.Windows;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class CoordinateScaleRectangle
{
    private readonly BlockSymbol _blockSymbol;
    public CoordinateScaleRectangle(BlockSymbol blockSymbol)
    {
        _blockSymbol = blockSymbol; 
    }

    public Point GetCoordinateLeftTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(-widthScaleRectangle, -heightScaleRectangle);
    }

    public Point GetCoordinateLeftMiddleRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(-widthScaleRectangle, _blockSymbol.Height / 2 - heightScaleRectangle);
    }

    public Point GetCoordinateLeftBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(-widthScaleRectangle, _blockSymbol.Height - heightScaleRectangle);
    }

    public Point GetCoordinateRightTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(_blockSymbol.Width - widthScaleRectangle, -heightScaleRectangle);
    }

    public Point GetCoordinateRightMiddleRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(_blockSymbol.Width - widthScaleRectangle, _blockSymbol.Height / 2 - heightScaleRectangle);
    }

    public Point GetCoordinateRightBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(_blockSymbol.Width - widthScaleRectangle, _blockSymbol.Height - heightScaleRectangle);
    }

    public Point GetCoordinateMiddleBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(_blockSymbol.Width / 2 - widthScaleRectangle, _blockSymbol.Height - heightScaleRectangle);
    }

    public Point GetCoordinateMiddleTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return new Point(_blockSymbol.Width / 2 - widthScaleRectangle, -heightScaleRectangle);
    }
}