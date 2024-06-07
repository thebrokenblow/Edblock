using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

internal class CoordinateScaleRectangle(BlockSymbolVM blockSymbolVM)
{
    private const int positionOffset = 4;

    internal (double, double) GetCoordinateLeftTopRectangle()
    {
        return (-positionOffset, -positionOffset);
    }

    internal (double, double) GetCoordinateLeftMiddleRectangle()
    {
        double yCoordinate = blockSymbolVM.Height / 2 - positionOffset;

        return (-positionOffset, yCoordinate);
    }

    internal (double, double) GetCoordinateLeftBottomRectangle()
    {
        double yCoordinate = blockSymbolVM.Height - positionOffset;

        return (-positionOffset, yCoordinate);
    }

    internal (double, double) GetCoordinateRightTopRectangle()
    {
        double xCoordinate = blockSymbolVM.Width - positionOffset;

        return (xCoordinate, -positionOffset);
    }

    internal (double, double) GetCoordinateRightMiddleRectangle()
    {
        double xCoordinate = blockSymbolVM.Width - positionOffset;
        double yCoordinate = blockSymbolVM.Height / 2 - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (double, double) GetCoordinateRightBottomRectangle()
    {
        double xCoordinate = blockSymbolVM.Width - positionOffset;
        double yCoordinate = blockSymbolVM.Height - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (double, double) GetCoordinateMiddleBottomRectangle()
    {
        double xCoordinate = blockSymbolVM.Width / 2 - positionOffset;
        double yCoordinate = blockSymbolVM.Height - positionOffset;

        return (xCoordinate, yCoordinate);
    }

    internal (double, double) GetCoordinateMiddleTopRectangle()
    {
        double xCoordinate = blockSymbolVM.Width / 2 - positionOffset;

        return (xCoordinate, -positionOffset);
    }
}