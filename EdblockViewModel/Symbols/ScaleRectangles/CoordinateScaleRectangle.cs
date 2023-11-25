using EdblockViewModel.Symbols.Abstraction;

namespace EdblockModel.Symbols.ScaleRectangles;

internal class CoordinateScaleRectangle
{
    private readonly BlockSymbolVM _blockSymbolVM;
    private const int positionOffset = 4;

    internal CoordinateScaleRectangle(BlockSymbolVM blockSymbolVM) =>
        _blockSymbolVM = blockSymbolVM;

    internal (int, int) GetCoordinateLeftTopRectangle()
    {
        return (-positionOffset, -positionOffset);
    }

    internal (int, int) GetCoordinateLeftMiddleRectangle()
    {
        int yCoordinate = _blockSymbolVM.Height / 2 - positionOffset;

        return (-positionOffset, yCoordinate);
    }

    internal (int, int) GetCoordinateLeftBottomRectangle()
    {
        int yCoordinate = _blockSymbolVM.Height - positionOffset;

        return (-positionOffset, yCoordinate);
    }

    internal (int, int) GetCoordinateRightTopRectangle()
    {
        int xCoordinate = _blockSymbolVM.Width - positionOffset;

        return (xCoordinate, -positionOffset);
    }

    internal (int, int) GetCoordinateRightMiddleRectangle()
    {
        int xCoordinate = _blockSymbolVM.Width - positionOffset;
        int yCoordinate = _blockSymbolVM.Height / 2 - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (int, int) GetCoordinateRightBottomRectangle()
    {
        int xCoordinate = _blockSymbolVM.Width - positionOffset;
        int yCoordinate = _blockSymbolVM.Height - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (int, int) GetCoordinateMiddleBottomRectangle()
    {
        int xCoordinate = _blockSymbolVM.Width / 2 - positionOffset;
        int yCoordinate = _blockSymbolVM.Height - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (int, int) GetCoordinateMiddleTopRectangle()
    {
        int xCoordinate = _blockSymbolVM.Width / 2 - positionOffset;

        return (xCoordinate, -positionOffset);
    }
}