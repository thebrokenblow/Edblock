using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

internal class CoordinateScaleRectangle
{
    private readonly BlockSymbolVM _blockSymbolVM;
    private const int positionOffset = 4;

    internal CoordinateScaleRectangle(BlockSymbolVM blockSymbolVM) =>
        _blockSymbolVM = blockSymbolVM;

    internal (double, double) GetCoordinateLeftTopRectangle()
    {
        return (-positionOffset, -positionOffset);
    }

    internal (double, double) GetCoordinateLeftMiddleRectangle()
    {
        double yCoordinate = _blockSymbolVM.Height / 2 - positionOffset;

        return (-positionOffset, yCoordinate);
    }

    internal (double, double) GetCoordinateLeftBottomRectangle()
    {
        double yCoordinate = _blockSymbolVM.Height - positionOffset;

        return (-positionOffset, yCoordinate);
    }

    internal (double, double) GetCoordinateRightTopRectangle()
    {
        double xCoordinate = _blockSymbolVM.Width - positionOffset;

        return (xCoordinate, -positionOffset);
    }

    internal (double, double) GetCoordinateRightMiddleRectangle()
    {
        double xCoordinate = _blockSymbolVM.Width - positionOffset;
        double yCoordinate = _blockSymbolVM.Height / 2 - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (double, double) GetCoordinateRightBottomRectangle()
    {
        double xCoordinate = _blockSymbolVM.Width - positionOffset;
        double yCoordinate = _blockSymbolVM.Height - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (double, double) GetCoordinateMiddleBottomRectangle()
    {
        double xCoordinate = _blockSymbolVM.Width / 2 - positionOffset;
        double yCoordinate = _blockSymbolVM.Height - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (double, double) GetCoordinateMiddleTopRectangle()
    {
        double xCoordinate = _blockSymbolVM.Width / 2 - positionOffset;

        return (xCoordinate, -positionOffset);
    }
}