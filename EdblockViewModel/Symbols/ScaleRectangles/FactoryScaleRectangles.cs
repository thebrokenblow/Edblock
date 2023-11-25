using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class FactoryScaleRectangles
{
    private readonly BlockSymbolVM _blockSymbolModel;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CoordinateScaleRectangle coordinateScaleRectangle;
    public FactoryScaleRectangles(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolModel)
    {
        _blockSymbolModel = blockSymbolModel;
        _canvasSymbolsVM = canvasSymbolsVM;
        coordinateScaleRectangle = new CoordinateScaleRectangle(_blockSymbolModel);
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
                 ScaleBlockSymbol.ChangeHeigthTopPart,
                 coordinateScaleRectangle.GetCoordinateMiddleTopRectangle);

        return middleTopRectangle;
    }

    private ScaleRectangle CreateRightTopRectangle()
    {
        var rightTopRectangle = new ScaleRectangle(
                 _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNESW,
                ScaleBlockSymbol.ChangeWidthRigthPart,
                ScaleBlockSymbol.ChangeHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateRightTopRectangle);

        return rightTopRectangle;
    }

    private ScaleRectangle CreateRightMiddleRectangle()
    {
        var rightMiddleRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeWE,
                ScaleBlockSymbol.ChangeWidthRigthPart,
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
                ScaleBlockSymbol.ChangeWidthRigthPart,
                ScaleBlockSymbol.ChangeHeigthBottomPart,
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
                ScaleBlockSymbol.ChangeHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle);

        return middleBottomRectangle;
    }

    private ScaleRectangle CreateLeftBottomRectangle()
    {
        var leftBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNESW,
                ScaleBlockSymbol.ChangeWidthLeftPart,
                ScaleBlockSymbol.ChangeHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateLeftBottomRectangle);

        return leftBottomRectangle;
    }

    private ScaleRectangle CreateLeftMiddleRectangle()
    {
        var leftMiddleRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeWE,
                ScaleBlockSymbol.ChangeWidthLeftPart,
                null,
                coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle);

        return leftMiddleRectangle;
    }

    private ScaleRectangle CreateLeftTopRectangle()
    {
        var leftTopRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _blockSymbolModel,
                Cursors.SizeNWSE,
                ScaleBlockSymbol.ChangeWidthLeftPart,
                ScaleBlockSymbol.ChangeHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateLeftTopRectangle);

        return leftTopRectangle;
    }
}