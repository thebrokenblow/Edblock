using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class CoordinateScaleRectangle(BlockSymbolVM blockSymbolVM)
{
    private const int positionOffset = 4;

    public (double, double) GetCoordinateLeftTopRectangle() =>
        (-positionOffset, -positionOffset);

    public (double, double) GetCoordinateLeftMiddleRectangle() =>
        (-positionOffset, blockSymbolVM.Height / 2 - positionOffset);

    public (double, double) GetCoordinateLeftBottomRectangle() =>
        (-positionOffset, blockSymbolVM.Height - positionOffset);

    public (double, double) GetCoordinateRightTopRectangle() =>
        (blockSymbolVM.Width - positionOffset, -positionOffset);

    public (double, double) GetCoordinateRightMiddleRectangle() =>
        (blockSymbolVM.Width - positionOffset, blockSymbolVM.Height / 2 - positionOffset);

    public (double, double) GetCoordinateRightBottomRectangle() =>
        (blockSymbolVM.Width - positionOffset, blockSymbolVM.Height - positionOffset);

    public (double, double) GetCoordinateMiddleBottomRectangle() =>
        (blockSymbolVM.Width / 2 - positionOffset, blockSymbolVM.Height - positionOffset);

    public (double, double) GetCoordinateMiddleTopRectangle() =>
         (blockSymbolVM.Width / 2 - positionOffset, -positionOffset);
}