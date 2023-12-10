namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class ScaleBlockSymbol
{
    internal static int ChangeWidthRigthPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        int widthBlockSymbol = canvasSymbolsVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;
       
        return widthBlockSymbol;
    }

    internal static int ChangeHeigthBottomPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        int heigthBlockSymbol = canvasSymbolsVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

        return heigthBlockSymbol;
    }

    internal static int ChangeWidthLeftPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        int currentXCoordinateCursor = canvasSymbolsVM.XCoordinate;
        int initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
        int initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;
        int minWidth = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel.MinWidth;

        int widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

        if (widthBlockSymbol >= minWidth)
        {
            int xCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);

            scalePartBlockSymbol.ScalingBlockSymbol.XCoordinate = xCoordinate;
        }

        return widthBlockSymbol;
    }

    internal static int ChangeHeigthTopPart(ScalePartBlockSymbol scalePartBlockSymbolVM, CanvasSymbolsVM canvasSymbolsVM)
    {
        int currentYCoordinateCursor = canvasSymbolsVM.YCoordinate;
        int initialHeigth = scalePartBlockSymbolVM.InitialHeigthBlockSymbol;
        int initialYCoordinate = scalePartBlockSymbolVM.InitialYCoordinateBlockSymbol;
        int minHeight = scalePartBlockSymbolVM.ScalingBlockSymbol.BlockSymbolModel.MinHeight;

        int heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

        if (heigthBlockSymbol >= minHeight)
        {
            int yCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);

            scalePartBlockSymbolVM.ScalingBlockSymbol.YCoordinate = yCoordinate;
        }

        return heigthBlockSymbol;
    }
}