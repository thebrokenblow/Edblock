using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class CoordinateConnectionPoint
{
    private const int offsetPosition = 10;
    private const int width = 8;
    private const int height = 8;
    private readonly BlockSymbolVM _blockSymbolVM;
    public CoordinateConnectionPoint(BlockSymbolVM blockSymbolVM) =>
        _blockSymbolVM = blockSymbolVM;

    public (int, int) GetCoordinateLeft()
    {
        int pointsX = -offsetPosition - width;
        int pointsY = _blockSymbolVM.Height / 2 - height / 2;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateRight()
    {
        int pointsX = _blockSymbolVM.Width + offsetPosition;
        int pointsY = _blockSymbolVM.Height / 2 - height / 2;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateTop()
    {
        int pointsX = _blockSymbolVM.Width / 2 - width / 2;
        int pointsY = -offsetPosition - height;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateBottom()
    {
        int pointsX = _blockSymbolVM.Width / 2 - width / 2;
        int pointsY = _blockSymbolVM.Height + offsetPosition;

        return (pointsX, pointsY);
    }
}