using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols.ScaleRectangles;

internal class FactoryScaleRectangles
{
    private readonly BlockSymbolVM _blockSymbolModel;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly ScaleAllSymbolVM _scaleAllSymbolVM;
    private readonly CoordinateScaleRectangle coordinateScaleRectangle;

    public FactoryScaleRectangles(CanvasSymbolsVM canvasSymbolsVM, ScaleAllSymbolVM scaleAllSymbolVM, BlockSymbolVM blockSymbolModel)
    {
        _blockSymbolModel = blockSymbolModel;
        _canvasSymbolsVM = canvasSymbolsVM;
        _scaleAllSymbolVM = scaleAllSymbolVM;
        coordinateScaleRectangle = new CoordinateScaleRectangle(_blockSymbolModel);
    }

    public List<ScaleRectangle> Create()
    {
        //TODO: упростить код задать сущности в переменные

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
                 _scaleAllSymbolVM,
                 _blockSymbolModel,
                 Cursors.SizeNS,
                 null,
                 ScaleBlockSymbol.GetHeigthTopPart,
                 coordinateScaleRectangle.GetCoordinateMiddleTopRectangle);

        return middleTopRectangle;
    }

    private ScaleRectangle CreateRightTopRectangle()
    {
        var rightTopRectangle = new ScaleRectangle(
                 _canvasSymbolsVM,
                 _scaleAllSymbolVM,
                _blockSymbolModel,
                Cursors.SizeNESW,
                ScaleBlockSymbol.GetWidthRigthPart,
                ScaleBlockSymbol.GetHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateRightTopRectangle);

        return rightTopRectangle;
    }

    private ScaleRectangle CreateRightMiddleRectangle()
    {
        var rightMiddleRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _scaleAllSymbolVM,
                _blockSymbolModel,
                Cursors.SizeWE,
                ScaleBlockSymbol.GetWidthRigthPart,
                null,
                coordinateScaleRectangle.GetCoordinateRightMiddleRectangle);

        return rightMiddleRectangle;
    }

    private ScaleRectangle CreateRightBottomRectangle()
    {
        var rightBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _scaleAllSymbolVM,
                _blockSymbolModel,
                Cursors.SizeNWSE,
                ScaleBlockSymbol.GetWidthRigthPart,
                ScaleBlockSymbol.GetHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateRightBottomRectangle);

        return rightBottomRectangle;
    }

    private ScaleRectangle CreateMiddleBottomRectangle()
    {
        var middleBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _scaleAllSymbolVM,
                _blockSymbolModel,
                Cursors.SizeNS,
                null,
                ScaleBlockSymbol.GetHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle);

        return middleBottomRectangle;
    }

    private ScaleRectangle CreateLeftBottomRectangle()
    {
        var leftBottomRectangle = new ScaleRectangle(
                _canvasSymbolsVM, 
                _scaleAllSymbolVM,
                _blockSymbolModel,
                Cursors.SizeNESW,
                ScaleBlockSymbol.GetWidthLeftPart,
                ScaleBlockSymbol.GetHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateLeftBottomRectangle);

        return leftBottomRectangle;
    }

    private ScaleRectangle CreateLeftMiddleRectangle()
    {
        var leftMiddleRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _scaleAllSymbolVM,
                _blockSymbolModel,
                Cursors.SizeWE,
                ScaleBlockSymbol.GetWidthLeftPart,
                null,
                coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle);

        return leftMiddleRectangle;
    }

    private ScaleRectangle CreateLeftTopRectangle()
    {
        var leftTopRectangle = new ScaleRectangle(
                _canvasSymbolsVM,
                _scaleAllSymbolVM,
                _blockSymbolModel,
                Cursors.SizeNWSE,
                ScaleBlockSymbol.GetWidthLeftPart,
                ScaleBlockSymbol.GetHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateLeftTopRectangle);

        return leftTopRectangle;
    }
}