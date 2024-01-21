using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class CoordinateConnectionPoint
{
    private const int width = 8;
    private const int height = 8;
    private const int offsetPosition = 10;

    private readonly BlockSymbolVM _blockSymbolVM;
    
    public CoordinateConnectionPoint(BlockSymbolVM blockSymbolVM) =>
        _blockSymbolVM = blockSymbolVM;

    public (double, double) GetCoordinateLeft()
    {
        double pointsX = -offsetPosition - width;
        double pointsY = _blockSymbolVM.Height / 2 - height / 2;

        return (pointsX, pointsY);
    }

    public (double, double) GetCoordinateRight()
    {
        double pointsX = _blockSymbolVM.Width + offsetPosition;
        double pointsY = _blockSymbolVM.Height / 2 - height / 2;

        return (pointsX, pointsY);
    }

    public (double, double) GetCoordinateTop()
    {
        double pointsX = _blockSymbolVM.Width / 2 - width / 2;
        double pointsY = -offsetPosition - height;

        return (pointsX, pointsY);
    }

    public (double, double) GetCoordinateBottom()
    {
        double pointsX = _blockSymbolVM.Width / 2 - width / 2;
        double pointsY = _blockSymbolVM.Height + offsetPosition;

        return (pointsX, pointsY);
    }
}