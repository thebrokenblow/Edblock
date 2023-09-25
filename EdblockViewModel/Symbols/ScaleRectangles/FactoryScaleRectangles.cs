using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class FactoryScaleRectangles
{
    private readonly BlockSymbol _blockSymbol;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public FactoryScaleRectangles(CanvasSymbolsVM canvasSymbolsVM, BlockSymbol blockSymbol) =>
        (_canvasSymbolsVM, _blockSymbol) = (canvasSymbolsVM, blockSymbol); 

    public List<ScaleRectangle> Create()
    {
        var coordinateScaleRectangle = new CoordinateScaleRectangle(_blockSymbol);
        var scaleRectangles = new List<ScaleRectangle>
        {
            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeNS, 
                null, 
                SizesScaleRectangle.ChangeHeigthTop, 
                coordinateScaleRectangle.GetCoordinateMiddleTopRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeNESW, 
                SizesScaleRectangle.ChangeWidthRigth, 
                SizesScaleRectangle.ChangeHeigthTop, 
                coordinateScaleRectangle.GetCoordinateRightTopRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeWE, 
                SizesScaleRectangle.ChangeWidthRigth, 
                null, 
                coordinateScaleRectangle.GetCoordinateRightMiddleRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeNWSE,
                SizesScaleRectangle.ChangeWidthRigth, 
                SizesScaleRectangle.ChangeHeigthBottom, 
                coordinateScaleRectangle.GetCoordinateRightBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeNS, 
                null, 
                SizesScaleRectangle.ChangeHeigthBottom,
                coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeNESW, 
                SizesScaleRectangle.ChangeWidthLeft,
                SizesScaleRectangle.ChangeHeigthBottom,
                coordinateScaleRectangle.GetCoordinateLeftBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeWE, 
                SizesScaleRectangle.ChangeWidthLeft, 
                null, 
                coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbol, 
                Cursors.SizeNWSE, 
                SizesScaleRectangle.ChangeWidthLeft, 
                SizesScaleRectangle.ChangeHeigthTop, 
                coordinateScaleRectangle.GetCoordinateLeftTopRectangle)
        };

        return scaleRectangles;
    }
}
