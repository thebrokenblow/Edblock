using EdblockViewModel.ComponentsVM;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class ScaleBlockSymbol
{
    internal static double GetWidthRigthPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        var blockSymbolModel = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel;

        double minWidth = blockSymbolModel.MinWidth;
        double widthBlockSymbol = canvasSymbolsVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

        if (minWidth > widthBlockSymbol)
        {
            return minWidth;
        }

        return widthBlockSymbol;
    }

    internal static double GetHeigthBottomPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        var blockSymbolModel = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel;

        double minHeight = blockSymbolModel.MinHeight;
        double heigthBlockSymbol = canvasSymbolsVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

        if (minHeight > heigthBlockSymbol)
        {
            return minHeight;
        }

        return heigthBlockSymbol;
    }

    internal static double GetWidthLeftPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        var blockSymbolModel = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel;

        int currentXCoordinateCursor = canvasSymbolsVM.XCoordinate;
        double initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
        double initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;
        double minWidth = blockSymbolModel.MinWidth;

        double widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

        if (minWidth > widthBlockSymbol)
        {
            return minWidth;
        }

        double xCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);
        scalePartBlockSymbol.ScalingBlockSymbol.XCoordinate = xCoordinate;

        return widthBlockSymbol;
    }

    internal static double GetHeigthTopPart(ScalePartBlockSymbol scalePartBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        var blockSymbolModel = scalePartBlockSymbol.ScalingBlockSymbol.BlockSymbolModel;

        int currentYCoordinateCursor = canvasSymbolsVM.YCoordinate;
        double initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
        double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;
        double minHeight = blockSymbolModel.MinHeight;

        double heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

        if (minHeight > heigthBlockSymbol)
        {
            return minHeight;
        }

        double yCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);
        scalePartBlockSymbol.ScalingBlockSymbol.YCoordinate = yCoordinate;

        return heigthBlockSymbol;
    }
}