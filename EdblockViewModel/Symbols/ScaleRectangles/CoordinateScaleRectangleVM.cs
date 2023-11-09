using EdblockViewModel.Symbols.Abstraction;

namespace EdblockModel.Symbols.ScaleRectangles;

internal class CoordinateScaleRectangleVM
{
    private readonly BlockSymbolVM _blockSymbolVM;
    private const int positionOffset = 4;

    internal CoordinateScaleRectangleVM(BlockSymbolVM blockSymbolVM) =>
        _blockSymbolVM = blockSymbolVM;

    internal (int, int) GetCoordinateLeftTopRectangle()
    {
        return (-positionOffset, -positionOffset);
    }

    internal (int, int) GetCoordinateLeftMiddleRectangle()
    {
        return (-positionOffset, _blockSymbolVM.Height / 2 - positionOffset);
    }

    internal (int, int) GetCoordinateLeftBottomRectangle()
    {
        return (-positionOffset, _blockSymbolVM.Height - positionOffset);
    }

    internal (int, int) GetCoordinateRightTopRectangle()
    {
        return (_blockSymbolVM.Width - positionOffset, -positionOffset);
    }

    internal (int, int) GetCoordinateRightMiddleRectangle()
    {
        return (_blockSymbolVM.Width - positionOffset, _blockSymbolVM.Height / 2 - positionOffset);
    }

    internal (int, int) GetCoordinateRightBottomRectangle()
    {
        return (_blockSymbolVM.Width - positionOffset, _blockSymbolVM.Height - positionOffset);
    }

    internal (int, int) GetCoordinateMiddleBottomRectangle()
    {
        return (_blockSymbolVM.Width / 2 - positionOffset, _blockSymbolVM.Height - positionOffset);
    }

    internal (int, int) GetCoordinateMiddleTopRectangle()
    {
        return (_blockSymbolVM.Width / 2 - positionOffset, -positionOffset);
    }
}