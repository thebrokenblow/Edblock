using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class FactoryScaleRectangles
{
    private readonly BlockSymbolVM _blockSymbolModel;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public FactoryScaleRectangles(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolModel) =>
        (_canvasSymbolsVM, _blockSymbolModel) = (canvasSymbolsVM, blockSymbolModel); 

    public List<ScaleRectangle> Create()
    {
        var coordinateScaleRectangle = new CoordinateScaleRectangleVM(_blockSymbolModel);
        var scaleRectangles = new List<ScaleRectangle>
        {
            new(
                _canvasSymbolsVM,
                _blockSymbolModel, 
                Cursors.SizeNS, 
                null, 
                ScaleBlockSymbolVM.ChangeHeigthTopPart, 
                coordinateScaleRectangle.GetCoordinateMiddleTopRectangle),

            new(
                _canvasSymbolsVM,
                _blockSymbolModel, 
                Cursors.SizeNESW, 
                ScaleBlockSymbolVM.ChangeWidthRigthPart, 
                ScaleBlockSymbolVM.ChangeHeigthTopPart, 
                coordinateScaleRectangle.GetCoordinateRightTopRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeWE, 
                ScaleBlockSymbolVM.ChangeWidthRigthPart, 
                null, 
                coordinateScaleRectangle.GetCoordinateRightMiddleRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNWSE,
                ScaleBlockSymbolVM.ChangeWidthRigthPart, 
                ScaleBlockSymbolVM.ChangeHeigthBottomPart, 
                coordinateScaleRectangle.GetCoordinateRightBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNS, 
                null, 
                ScaleBlockSymbolVM.ChangeHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNESW, 
                ScaleBlockSymbolVM.ChangeWidthLeftPart,
                ScaleBlockSymbolVM.ChangeHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateLeftBottomRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeWE, 
                ScaleBlockSymbolVM.ChangeWidthLeftPart, 
                null, 
                coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle),

            new(
                _canvasSymbolsVM, 
                _blockSymbolModel, 
                Cursors.SizeNWSE, 
                ScaleBlockSymbolVM.ChangeWidthLeftPart, 
                ScaleBlockSymbolVM.ChangeHeigthTopPart, 
                coordinateScaleRectangle.GetCoordinateLeftTopRectangle)
        };

        return scaleRectangles;
    }
}