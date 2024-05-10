using System.Windows.Input;
using Edblock.PagesViewModel.Components;
using Edblock.PagesViewModel.Components.PopupBox;
using Edblock.SymbolsViewModel.Core;

namespace Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class BuilderScaleRectangles(CanvasSymbolsViewModel canvasSymbolsViewModel, ScaleAllSymbolViewModel scaleAllSymbolVM, BlockSymbolViewModel blockSymbolViewModel)
{
    private readonly List<ScaleRectangle> scaleRectangles = [];
    private readonly CoordinateScaleRectangle coordinateScaleRectangle = new(blockSymbolViewModel);

    public BuilderScaleRectangles AddMiddleTopRectangle()
    {
        var middleTopRectangle = CreateMiddleTopRectangle();
        scaleRectangles.Add(middleTopRectangle);

        return this;
    }

    public BuilderScaleRectangles AddRightTopRectangle()
    {
        var rightTopRectangle = CreateRightTopRectangle();
        scaleRectangles.Add(rightTopRectangle);

        return this;
    }

    public BuilderScaleRectangles AddRightMiddleRectangle()
    {
        var rightMiddleRectangle = CreateRightMiddleRectangle();
        scaleRectangles.Add(rightMiddleRectangle);

        return this;
    }

    public BuilderScaleRectangles AddRightBottomRectangle()
    {
        var rightBottomRectangle = CreateRightBottomRectangle();
        scaleRectangles.Add(rightBottomRectangle);

        return this;
    }

    public BuilderScaleRectangles AddMiddleBottomRectangle()
    {
        var middleBottomRectangle = CreateMiddleBottomRectangle();
        scaleRectangles.Add(middleBottomRectangle);

        return this;
    }

    public BuilderScaleRectangles AddLeftBottomRectangle()
    {
        var leftBottomRectangle = CreateLeftBottomRectangle();
        scaleRectangles.Add(leftBottomRectangle);

        return this;
    }

    public BuilderScaleRectangles AddLeftMiddleRectangle()
    {
        var leftMiddleRectangle = CreateLeftMiddleRectangle();
        scaleRectangles.Add(leftMiddleRectangle);

        return this;
    }

    public BuilderScaleRectangles AddLeftTopRectangle()
    {
        var leftTopRectangle = CreateLeftTopRectangle();
        scaleRectangles.Add(leftTopRectangle);

        return this;
    }

    public List<ScaleRectangle> Build()
    {
        return scaleRectangles;
    }

    //TODO: Переделать Фабриик
    private ScaleRectangle CreateMiddleTopRectangle()
    {
        var middleTopRectangle = new ScaleRectangle(
                 canvasSymbolsViewModel,
                 scaleAllSymbolVM,
                 blockSymbolViewModel,
                 Cursors.SizeNS,
                 null,
                 ScaleBlockSymbol.GetHeigthTopPart,
                 coordinateScaleRectangle.GetCoordinateMiddleTopRectangle);

        return middleTopRectangle;
    }

    private ScaleRectangle CreateRightTopRectangle()
    {
        var rightTopRectangle = new ScaleRectangle(
                 canvasSymbolsViewModel,
                 scaleAllSymbolVM,
                blockSymbolViewModel,
                Cursors.SizeNESW,
                ScaleBlockSymbol.GetWidthRigthPart,
                ScaleBlockSymbol.GetHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateRightTopRectangle);

        return rightTopRectangle;
    }

    private ScaleRectangle CreateRightMiddleRectangle()
    {
        var rightMiddleRectangle = new ScaleRectangle(
                canvasSymbolsViewModel,
                scaleAllSymbolVM,
                blockSymbolViewModel,
                Cursors.SizeWE,
                ScaleBlockSymbol.GetWidthRigthPart,
                null,
                coordinateScaleRectangle.GetCoordinateRightMiddleRectangle);

        return rightMiddleRectangle;
    }

    private ScaleRectangle CreateRightBottomRectangle()
    {
        var rightBottomRectangle = new ScaleRectangle(
                canvasSymbolsViewModel,
                scaleAllSymbolVM,
                blockSymbolViewModel,
                Cursors.SizeNWSE,
                ScaleBlockSymbol.GetWidthRigthPart,
                ScaleBlockSymbol.GetHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateRightBottomRectangle);

        return rightBottomRectangle;
    }

    private ScaleRectangle CreateMiddleBottomRectangle()
    {
        var middleBottomRectangle = new ScaleRectangle(
                canvasSymbolsViewModel,
                scaleAllSymbolVM,
                blockSymbolViewModel,
                Cursors.SizeNS,
                null,
                ScaleBlockSymbol.GetHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle);

        return middleBottomRectangle;
    }

    private ScaleRectangle CreateLeftBottomRectangle()
    {
        var leftBottomRectangle = new ScaleRectangle(
                canvasSymbolsViewModel,
                scaleAllSymbolVM,
                blockSymbolViewModel,
                Cursors.SizeNESW,
                ScaleBlockSymbol.GetWidthLeftPart,
                ScaleBlockSymbol.GetHeigthBottomPart,
                coordinateScaleRectangle.GetCoordinateLeftBottomRectangle);

        return leftBottomRectangle;
    }

    private ScaleRectangle CreateLeftMiddleRectangle()
    {
        var leftMiddleRectangle = new ScaleRectangle(
                canvasSymbolsViewModel,
                scaleAllSymbolVM,
                blockSymbolViewModel,
                Cursors.SizeWE,
                ScaleBlockSymbol.GetWidthLeftPart,
                null,
                coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle);

        return leftMiddleRectangle;
    }

    private ScaleRectangle CreateLeftTopRectangle()
    {
        var leftTopRectangle = new ScaleRectangle(
                canvasSymbolsViewModel,
                scaleAllSymbolVM,
                blockSymbolViewModel,
                Cursors.SizeNWSE,
                ScaleBlockSymbol.GetWidthLeftPart,
                ScaleBlockSymbol.GetHeigthTopPart,
                coordinateScaleRectangle.GetCoordinateLeftTopRectangle);

        return leftTopRectangle;
    }
}