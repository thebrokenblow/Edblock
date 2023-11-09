using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class FactoryScaleRectangles
{
    private readonly BlockSymbolVM _blockSymbolModel;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CoordinateScaleRectangleVM coordinateScaleRectangle;
    public FactoryScaleRectangles(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolModel)
    {
        _blockSymbolModel = blockSymbolModel;
        _canvasSymbolsVM = canvasSymbolsVM;
        coordinateScaleRectangle = new CoordinateScaleRectangleVM(_blockSymbolModel);
    }

    public List<ScaleRectangle> Create()
    {
        var scaleRectangles = new List<ScaleRectangle>
        {
            CreateMiddleTopRectangle(),
            CreateRightTopRectangle(),
            CreateRightMiddleRectangle(),
            CreateRightBottomRectangle(),
            CreateMiddleBottomRectangle(),
            CreateLeftBottomRectangle(),
            CreateLeftMiddleRectangle(),
            CreateLeftTopRectangle()
        };

        return scaleRectangles;
    }

    private ScaleRectangle CreateMiddleTopRectangle()
    {
        var middleTopRectangle = new ScaleRectangle(
                 _canvasSymbolsVM,
                 _blockSymbolModel,
                 Cursors.SizeNS,
                 null,
                 ScaleBlockSymbolVM.ChangeHeigthTopPart,
                 coordinateScaleRectangle.GetCoordinateMiddleTopRectangle);

        return middleTopRectangle;
    }

    private ScaleRectangle CreateRightTopRectangle()
    {
        var middleTopRectangle = new ScaleRectangle(
                 _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNESW,
                ScaleBlockSymbolVM.ChangeWidthRigthPart,
                ScaleBlockSymbolVM.ChangeHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateRightTopRectangle);

        return middleTopRectangle;
    }

    private ScaleRectangle CreateRightMiddleRectangle()
    {
        var rightMiddleRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeWE,
                ScaleBlockSymbolVM.ChangeWidthRigthPart,
                null,
                coordinateScaleRectangle.GetCoordinateRightMiddleRectangle);

        return rightMiddleRectangle;
    }

    private ScaleRectangle CreateRightBottomRectangle()
    {
        var rightBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNWSE,
                ScaleBlockSymbolVM.ChangeWidthRigthPart,
                ScaleBlockSymbolVM.ChangeHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateRightBottomRectangle);

        return rightBottomRectangle;
    }

    private ScaleRectangle CreateMiddleBottomRectangle()
    {
        var middleBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNS,
                null,
                ScaleBlockSymbolVM.ChangeHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle);

        return middleBottomRectangle;
    }

    private ScaleRectangle CreateLeftBottomRectangle()
    {
        var leftBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNESW,
                ScaleBlockSymbolVM.ChangeWidthLeftPart,
                ScaleBlockSymbolVM.ChangeHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateLeftBottomRectangle);

        return leftBottomRectangle;
    }

    private ScaleRectangle CreateLeftMiddleRectangle()
    {
        var leftBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeWE,
                ScaleBlockSymbolVM.ChangeWidthLeftPart,
                null,
                coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle);

        return leftBottomRectangle;
    }

    private ScaleRectangle CreateLeftTopRectangle()
    {
        var leftTopRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNWSE,
                ScaleBlockSymbolVM.ChangeWidthLeftPart,
                ScaleBlockSymbolVM.ChangeHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateLeftTopRectangle);

        return leftTopRectangle;
    }
}