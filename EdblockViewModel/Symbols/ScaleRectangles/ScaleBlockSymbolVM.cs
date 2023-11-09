namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class ScaleBlockSymbolVM
{
    internal static int ChangeWidthLeftPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int widthBlockSymbol = scalePartBlockSymbolVM.InitialWidthBlockSymbol + 
            (scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol - canvasSymbolsVM.X);

        scalePartBlockSymbolVM.ScalingBlockSymbol.XCoordinate = scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol - 
            (widthBlockSymbol - scalePartBlockSymbolVM.InitialWidthBlockSymbol);

        return widthBlockSymbol;
    }

    internal static int ChangeWidthRigthPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int widthBlockSymbol = canvasSymbolsVM.X - scalePartBlockSymbolVM.InitialXCoordinateBlockSymbol;

        return widthBlockSymbol;
    }

    internal static int ChangeHeigthBottomPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int heigthBlockSymbol = canvasSymbolsVM.Y - scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol;

        return heigthBlockSymbol;
    }

    internal static int ChangeHeigthTopPart(ScalePartBlockSymbolVM scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int heigthBlockSymbol = scalePartBlockSymbolVM.InitialHeigthBlockSymbol +
            (scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol - canvasSymbolsVM.Y);

        scalePartBlockSymbolVM.ScalingBlockSymbol.YCoordinate = scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol - 
            (heigthBlockSymbol - scalePartBlockSymbolVM.InitialHeigthBlockSymbol);

        return heigthBlockSymbol;
    }
}