using EdblockViewModel;

namespace MVVM.ViewModel.SymbolsViewModel.ScaleRectangleViewModel;

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

    //public static int ChangeHeigthBottom(ScaleData scaleData, EdSymbolViewModel edSymbolViewModel)
    //{
    //    int heigth = scaleData.InitialHeigth + (edSymbolViewModel.PanelY - scaleData.InitialY) - 10;

    //    return heigth;
    //}

    //public static int ChangeHeigthTop(ScaleData scaleData, EdSymbolViewModel edSymbolViewModel)
    //{
    //    int heigth = scaleData.InitialHeigth + (scaleData.InitialY - edSymbolViewModel.PanelY) - 10;
    //    scaleData.Symbol.YCoordinate = scaleData.InitialY - (heigth - scaleData.InitialHeigth) + 10;

    //    return heigth;
    //}
}