using EdblockViewModel.Components.CanvasSymbols.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScaleBlockSymbol
{
    private const double minWidth = 40;
    private const double minHeight = 40;

    public static double GetWidthRigthPart(ScalePartBlockSymbol scalePartBlockSymbol, ICanvasSymbolsComponentVM canvasSymbolsVM)
    {
        double widthBlockSymbol = canvasSymbolsVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

        if (minWidth > widthBlockSymbol)
        {
            return minWidth;
        }

        return widthBlockSymbol;
    }

    public static double GetHeigthBottomPart(ScalePartBlockSymbol scalePartBlockSymbol, ICanvasSymbolsComponentVM canvasSymbolsVM)
    {
        double heigthBlockSymbol = canvasSymbolsVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

        if (minHeight > heigthBlockSymbol)
        {
            return minHeight;
        }

        return heigthBlockSymbol;
    }

    public static double GetWidthLeftPart(ScalePartBlockSymbol scalePartBlockSymbol, ICanvasSymbolsComponentVM canvasSymbolsVM)
    {
        int currentXCoordinateCursor = canvasSymbolsVM.XCoordinate;
        double initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
        double initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

        double widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

        if (minWidth > widthBlockSymbol)
        {
            return minWidth;
        }

        //scalePartBlockSymbol.ScalingBlockSymbol.XCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);

        return widthBlockSymbol;
    }

    public static double GetHeigthTopPart(ScalePartBlockSymbol scalePartBlockSymbol, ICanvasSymbolsComponentVM canvasSymbolsVM)
    {
        int currentYCoordinateCursor = canvasSymbolsVM.YCoordinate;
        double initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
        double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

        double heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

        if (minHeight > heigthBlockSymbol)
        {
            return minHeight;
        }

        //scalePartBlockSymbol.ScalingBlockSymbol.YCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);

        return heigthBlockSymbol;
    }
}