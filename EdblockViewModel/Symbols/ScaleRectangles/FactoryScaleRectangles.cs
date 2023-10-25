using System.Windows.Input;
using System.Collections.Generic;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class FactoryScaleRectangles
{
    private readonly BlockSymbol _blockSymbolModel;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public FactoryScaleRectangles(CanvasSymbolsVM canvasSymbolsVM, BlockSymbol blockSymbolModel) =>
        (_canvasSymbolsVM, _blockSymbolModel) = (canvasSymbolsVM, blockSymbolModel); 

    public List<ScaleRectangle> Create()
    {
        var coordinateScaleRectangle = new CoordinateScaleRectangleModel(_blockSymbolModel.BlockSymbolModel);
        var scaleRectangles = new List<ScaleRectangle>
        {
            new(
                _canvasSymbolsVM,
                _blockSymbolModel, 
                Cursors.SizeNS, 
                null, 
                SizesScaleRectangle.ChangeHeigthTop, 
                coordinateScaleRectangle.GetCoordinateMiddleTopRectangle),

            new(
                _canvasSymbolsVM,
                _blockSymbolModel, 
                Cursors.SizeNESW, 
                SizesScaleRectangle.ChangeWidthRigth, 
                SizesScaleRectangle.ChangeHeigthTop, 
                coordinateScaleRectangle.GetCoordinateRightTopRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeWE, 
                SizesScaleRectangle.ChangeWidthRigth, 
                null, 
                coordinateScaleRectangle.GetCoordinateRightMiddleRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNWSE,
                SizesScaleRectangle.ChangeWidthRigth, 
                SizesScaleRectangle.ChangeHeigthBottom, 
                coordinateScaleRectangle.GetCoordinateRightBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNS, 
                null, 
                SizesScaleRectangle.ChangeHeigthBottom,
                coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNESW, 
                SizesScaleRectangle.ChangeWidthLeft,
                SizesScaleRectangle.ChangeHeigthBottom,
                coordinateScaleRectangle.GetCoordinateLeftBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeWE, 
                SizesScaleRectangle.ChangeWidthLeft, 
                null, 
                coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNWSE, 
                SizesScaleRectangle.ChangeWidthLeft, 
                SizesScaleRectangle.ChangeHeigthTop, 
                coordinateScaleRectangle.GetCoordinateLeftTopRectangle)
        };

        return scaleRectangles;
    }
}
