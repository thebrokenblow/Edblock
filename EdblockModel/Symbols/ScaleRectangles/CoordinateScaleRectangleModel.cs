using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols.ScaleRectangles;

public class CoordinateScaleRectangleModel
{
    private readonly BlockSymbolModel _blockSymbol;
    public CoordinateScaleRectangleModel(BlockSymbolModel blockSymbol)
    {
        _blockSymbol = blockSymbol;
    }

    public (int, int) GetCoordinateLeftTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (-widthScaleRectangle, -heightScaleRectangle);
    }

    public (int, int) GetCoordinateLeftMiddleRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (-widthScaleRectangle, _blockSymbol.Height / 2 - heightScaleRectangle);
    }

    public (int, int) GetCoordinateLeftBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (-widthScaleRectangle, _blockSymbol.Height - heightScaleRectangle);
    }

    public (int, int) GetCoordinateRightTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (_blockSymbol.Width - widthScaleRectangle, -heightScaleRectangle);
    }

    public (int, int) GetCoordinateRightMiddleRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (_blockSymbol.Width - widthScaleRectangle, _blockSymbol.Height / 2 - heightScaleRectangle);
    }

    public (int, int) GetCoordinateRightBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (_blockSymbol.Width - widthScaleRectangle, _blockSymbol.Height - heightScaleRectangle);
    }

    public (int, int) GetCoordinateMiddleBottomRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (_blockSymbol.Width / 2 - widthScaleRectangle, _blockSymbol.Height - heightScaleRectangle);
    }

    public (int, int) GetCoordinateMiddleTopRectangle(int widthScaleRectangle, int heightScaleRectangle)
    {
        return (_blockSymbol.Width / 2 - widthScaleRectangle, -heightScaleRectangle);
    }
}