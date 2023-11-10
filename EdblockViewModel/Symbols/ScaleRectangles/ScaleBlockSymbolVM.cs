namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class ScaleBlockSymbolVM
{
    internal static int ChangeWidthLeftPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int widthBlockSymbol = scalePartBlockSymbolVM.InitialWidthBlockSymbol + 
            (scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol - canvasSymbolsVM.XCoordinate);

        scalePartBlockSymbolVM.ScalingBlockSymbol.XCoordinate = scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol - 
            (widthBlockSymbol - scalePartBlockSymbolVM.InitialWidthBlockSymbol);

        return widthBlockSymbol;
    }

    internal static int ChangeWidthRigthPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int widthBlockSymbol = canvasSymbolsVM.XCoordinate - scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol;

        return widthBlockSymbol;
    }

    internal static int ChangeHeigthBottomPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int heigthBlockSymbol = canvasSymbolsVM.YCoordinate - scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol;

        return heigthBlockSymbol;
    }

    internal static int ChangeHeigthTopPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int heigthBlockSymbol = scalePartBlockSymbolVM.InitialHeigthBlockSymbol +
            (scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol - canvasSymbolsVM.YCoordinate);

        scalePartBlockSymbolVM.ScalingBlockSymbol.YCoordinate = scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol - 
            (heigthBlockSymbol - scalePartBlockSymbolVM.InitialHeigthBlockSymbol);

        return heigthBlockSymbol;
    }
}