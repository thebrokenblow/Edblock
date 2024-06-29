namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;

public interface ICoordinateScaleRectangle
{
    public (double, double) GetCoordinateLeftTop();
    public (double, double) GetCoordinateLeftMiddle();
    public (double, double) GetCoordinateLeftBottom();
    public (double, double) GetCoordinateRightTop();
    public (double, double) GetCoordinateRightMiddle();
    public (double, double) GetCoordinateRightBottom();
    public (double, double) GetCoordinateMiddleBottom();
    public (double, double) GetCoordinateMiddleTop();
}