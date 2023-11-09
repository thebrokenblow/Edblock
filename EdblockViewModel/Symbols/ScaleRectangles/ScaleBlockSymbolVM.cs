namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class ScaleBlockSymbolVM
{
    internal static int ChangeWidthLeftPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM edSymbolViewModel)
    {
        int width = scalePartBlockSymbolVM.InitialWidthBlockSymbol + (scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol - edSymbolViewModel.X);
        scalePartBlockSymbolVM.ScalingBlockSymbol.XCoordinate = scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol - (width - scalePartBlockSymbolVM.InitialWidthBlockSymbol);

        return width;
    }

    internal static int ChangeWidthRigthPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM edSymbolViewModel)
    {
        int width = edSymbolViewModel.X - scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol;

        return width;
    }

    internal static int ChangeHeigthBottomPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM edSymbolViewModel)
    {
        int heigth = edSymbolViewModel.Y - scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol;

        return heigth;
    }

    internal static int ChangeHeigthTopPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM edSymbolViewModel)
    {
        int heigth = scalePartBlockSymbolVM.InitialHeigthBlockSymbol + (scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol - edSymbolViewModel.Y);
        scalePartBlockSymbolVM.ScalingBlockSymbol.YCoordinate = scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol - (heigth - scalePartBlockSymbolVM.InitialHeigthBlockSymbol);

        return heigth;
    }
}