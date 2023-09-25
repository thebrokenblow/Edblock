namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class SizesScaleRectangle
{
    public static int ChangeWidthLeft(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int width = scaleData.InitialWidth + (scaleData.InitialX - edSymbolViewModel.X) - 10;
        scaleData.BlockSymbol.XCoordinate = scaleData.InitialX - (width - scaleData.InitialWidth);

        return width;
    }

    public static int ChangeWidthRigth(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int width = edSymbolViewModel.X - scaleData.InitialX;

        return width;
    }

    public static int ChangeHeigthBottom(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int heigth = edSymbolViewModel.Y - scaleData.InitialY;

        return heigth;
    }

    public static int ChangeHeigthTop(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int heigth = scaleData.InitialHeigth + (scaleData.InitialY - edSymbolViewModel.Y) - 10;
        scaleData.BlockSymbol.YCoordinate = scaleData.InitialY - (heigth - scaleData.InitialHeigth);

        return heigth;
    }
}