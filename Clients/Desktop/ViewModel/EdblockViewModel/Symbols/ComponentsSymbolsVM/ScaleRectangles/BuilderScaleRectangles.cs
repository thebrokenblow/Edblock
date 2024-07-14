using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class BuilderScaleRectangles(
    ICanvasSymbolsComponentVM canvasSymbolsComponentVM, 
    IScaleAllSymbolComponentVM scaleAllSymbolComponentVM) : IBuilderScaleRectangles
{
    private ICoordinateScaleRectangle coordinateScaleRectangle = null!;
    private readonly List<ScaleRectangle> scaleRectangles = [];

    private ScalableBlockSymbolVM scalableBlockSymbolVM = null!;

    public ScalableBlockSymbolVM ScalableBlockSymbolVM
    {
        get => scalableBlockSymbolVM;
        set
        {
            scalableBlockSymbolVM = value;
            coordinateScaleRectangle = new CoordinateScaleRectangle(scalableBlockSymbolVM);
        }
    }

    public IBuilderScaleRectangles AddMiddleTopRectangle()
    {
        var middleTopRectangle = CreateMiddleTopRectangle();
        scaleRectangles.Add(middleTopRectangle);

        return this;
    }

    public IBuilderScaleRectangles AddRightTopRectangle()
    {
        var rightTopRectangle = CreateRightTopRectangle();
        scaleRectangles.Add(rightTopRectangle);

        return this;
    }

    public IBuilderScaleRectangles AddRightMiddleRectangle()
    {
        var rightMiddleRectangle = CreateRightMiddleRectangle();
        scaleRectangles.Add(rightMiddleRectangle);

        return this;
    }

    public IBuilderScaleRectangles AddRightBottomRectangle()
    {
        var rightBottomRectangle = CreateRightBottomRectangle();
        scaleRectangles.Add(rightBottomRectangle);

        return this;
    }

    public IBuilderScaleRectangles AddMiddleBottomRectangle()
    {
        var middleBottomRectangle = CreateMiddleBottomRectangle();
        scaleRectangles.Add(middleBottomRectangle);

        return this;
    }

    public IBuilderScaleRectangles AddLeftBottomRectangle()
    {
        var leftBottomRectangle = CreateLeftBottomRectangle();
        scaleRectangles.Add(leftBottomRectangle);

        return this;
    }

    public IBuilderScaleRectangles AddLeftMiddleRectangle()
    {
        var leftMiddleRectangle = CreateLeftMiddleRectangle();
        scaleRectangles.Add(leftMiddleRectangle);

        return this;
    }

    public IBuilderScaleRectangles AddLeftTopRectangle()
    {
        var leftTopRectangle = CreateLeftTopRectangle();
        scaleRectangles.Add(leftTopRectangle);

        return this;
    }

    public List<ScaleRectangle> Build() =>
        scaleRectangles;

    private ScaleRectangle CreateMiddleTopRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeNS,
            coordinateScaleRectangle.GetCoordinateMiddleTop,
            PositionScaleRectangle.MiddleTop);

    private ScaleRectangle CreateRightTopRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeNESW,
            coordinateScaleRectangle.GetCoordinateRightTop,
            PositionScaleRectangle.RightTop);

    private ScaleRectangle CreateRightMiddleRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeWE,
            coordinateScaleRectangle.GetCoordinateRightMiddle,
            PositionScaleRectangle.RightMiddle);

    private ScaleRectangle CreateRightBottomRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeNWSE,
            coordinateScaleRectangle.GetCoordinateRightBottom,
            PositionScaleRectangle.RightBottom);

    private ScaleRectangle CreateMiddleBottomRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeNS,
            coordinateScaleRectangle.GetCoordinateMiddleBottom,
            PositionScaleRectangle.MiddleBottom);

    private ScaleRectangle CreateLeftBottomRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeNESW,
            coordinateScaleRectangle.GetCoordinateLeftBottom,
            PositionScaleRectangle.LeftBottom);

    private ScaleRectangle CreateLeftMiddleRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeWE,
            coordinateScaleRectangle.GetCoordinateLeftMiddle,
            PositionScaleRectangle.LeftMiddle);

    private ScaleRectangle CreateLeftTopRectangle() =>
        new(
            canvasSymbolsComponentVM,
            scaleAllSymbolComponentVM,
            scalableBlockSymbolVM,
            Cursors.SizeNWSE,
            coordinateScaleRectangle.GetCoordinateLeftTop,
            PositionScaleRectangle.LeftTop);
}