using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class CoordinateScaleRectangle(BlockSymbolVM blockSymbolVM) : ICoordinateScaleRectangle
{
    private const int positionOffset = 4;

    public (double, double) GetCoordinateLeftTop() =>
        (-positionOffset, -positionOffset);

    public (double, double) GetCoordinateLeftMiddle() =>
        (-positionOffset, blockSymbolVM.Height / 2 - positionOffset);

    public (double, double) GetCoordinateLeftBottom() =>
        (-positionOffset, blockSymbolVM.Height - positionOffset);

    public (double, double) GetCoordinateRightTop() =>
        (blockSymbolVM.Width - positionOffset, -positionOffset);

    public (double, double) GetCoordinateRightMiddle() =>
        (blockSymbolVM.Width - positionOffset, blockSymbolVM.Height / 2 - positionOffset);

    public (double, double) GetCoordinateRightBottom() =>
        (blockSymbolVM.Width - positionOffset, blockSymbolVM.Height - positionOffset);

    public (double, double) GetCoordinateMiddleBottom() =>
        (blockSymbolVM.Width / 2 - positionOffset, blockSymbolVM.Height - positionOffset);

    public (double, double) GetCoordinateMiddleTop() =>
         (blockSymbolVM.Width / 2 - positionOffset, -positionOffset);
}