namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class ScaleBlockSymbol
{
    internal static int GetWidthRigthPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        int minWidth = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel.MinWidth;
        int widthBlockSymbol = canvasSymbolsVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

        if (minWidth > widthBlockSymbol)
        {
            return minWidth;
        }
       
        return widthBlockSymbol;
    }

    internal static int GetHeigthBottomPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        int minHeight = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel.MinHeight;
        int heigthBlockSymbol = canvasSymbolsVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

        if (minHeight > heigthBlockSymbol)
        {
            return minHeight;
        }

        return heigthBlockSymbol;
    }

    internal static int GetWidthLeftPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        int currentXCoordinateCursor = canvasSymbolsVM.XCoordinate;
        int initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
        int initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;
        int minWidth = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel.MinWidth;

        int widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

        if (minWidth > widthBlockSymbol)
        {
            return minWidth;
        }

        int xCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);
        scalePartBlockSymbol.ScalingBlockSymbol.XCoordinate = xCoordinate;

        return widthBlockSymbol;
    }

    internal static int GetHeigthTopPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        int currentYCoordinateCursor = canvasSymbolsVM.YCoordinate;
        int initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
        int initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;
        int minHeight = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel.MinHeight;

        int heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

        if (minHeight > heigthBlockSymbol)
        {
            return minHeight;
        }

        int yCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);
        scalePartBlockSymbol.ScalingBlockSymbol.YCoordinate = yCoordinate;

        return heigthBlockSymbol;
    }
}