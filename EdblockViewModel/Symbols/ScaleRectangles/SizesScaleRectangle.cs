namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class SizesScaleRectangle
{
    internal static int ChangeWidthLeft(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int width = scaleData.InitialWidth + (scaleData.InitialX - edSymbolViewModel.X);
        scaleData.BlockSymbol.XCoordinate = scaleData.InitialX - (width - scaleData.InitialWidth);

        return width;
    }

    internal static int ChangeWidthRigth(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int width = edSymbolViewModel.X - scaleData.InitialX;

        return width;
    }

    internal static int ChangeHeigthBottom(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int heigth = edSymbolViewModel.Y - scaleData.InitialY;

        return heigth;
    }

    internal static int ChangeHeigthTop(ScaleData scaleData, CanvasSymbolsVM edSymbolViewModel)
    {
        int heigth = scaleData.InitialHeigth + (scaleData.InitialY - edSymbolViewModel.Y);
        scaleData.BlockSymbol.YCoordinate = scaleData.InitialY - (heigth - scaleData.InitialHeigth);

        return heigth;
    }
}