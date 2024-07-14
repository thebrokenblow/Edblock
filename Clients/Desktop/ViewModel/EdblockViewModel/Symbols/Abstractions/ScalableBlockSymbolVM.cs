using System.Collections.Generic;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;

namespace EdblockViewModel.Symbols.Abstractions;

public abstract class ScalableBlockSymbolVM : BlockSymbolVM
{
    public List<ScaleRectangle> ScaleRectangles { get; set; }
    public IBuilderScaleRectangles BuilderScaleRectangles { get; }

    public ScalableBlockSymbolVM(
        IBuilderScaleRectangles builderScaleRectangles,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM, 
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM, 
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM, 
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : base(canvasSymbolsComponentVM, listCanvasSymbolsComponentVM, topSettingsMenuComponentVM, popupBoxMenuComponentVM)
    {
        BuilderScaleRectangles = builderScaleRectangles;
        BuilderScaleRectangles.ScalableBlockSymbolVM = this;
        ScaleRectangles = CreateScaleRectangles();
    }

    public abstract void SetWidth(double width);
    public abstract void SetHeight(double height);

    protected virtual List<ScaleRectangle> CreateScaleRectangles() =>
        BuilderScaleRectangles
        .AddMiddleTopRectangle()
        .AddRightTopRectangle()
        .AddRightMiddleRectangle()
        .AddRightBottomRectangle()
        .AddMiddleBottomRectangle()
        .AddLeftBottomRectangle()
        .AddLeftMiddleRectangle()
        .AddLeftTopRectangle()
        .Build();

    protected void ChangeCoordinateScaleRectangle()
    {
        foreach (var scaleRectangle in ScaleRectangles)
        {
            scaleRectangle.ChangeCoordination();
        }
    }

    protected void SetWidthRigthPart(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        double widthBlockSymbol = _canvasSymbolsComponentVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

        if (widthBlockSymbol < 40)
        {
            widthBlockSymbol = 40;
        }

        SetWidth(widthBlockSymbol);
    }

    protected void SetHeigthBottomPart(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        double heigthBlockSymbol = _canvasSymbolsComponentVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

        if (heigthBlockSymbol < 40)
        {
            heigthBlockSymbol = 40;
        }

        SetHeight(heigthBlockSymbol);
    }

    protected void SetWidthLeftPart(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        int currentXCoordinateCursor = _canvasSymbolsComponentVM.XCoordinate;
        double initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
        double initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

        double widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

        if (widthBlockSymbol < 40)
        {
            widthBlockSymbol = 40;
        }

        SetWidth(widthBlockSymbol);
        XCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);
    }

    protected void SetHeigthTopPart(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        int currentYCoordinateCursor = _canvasSymbolsComponentVM.YCoordinate;
        double initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
        double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

        double heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

        if (heigthBlockSymbol < 40)
        {
            heigthBlockSymbol = 40;
        }

        SetHeight(heigthBlockSymbol);
        YCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);
    }

    public virtual void SetSize(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightMiddle)
        {
           SetWidthRigthPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightBottom)
        {
            SetWidthRigthPart(scalePartBlockSymbol);
            SetHeigthBottomPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.MiddleBottom)
        {
            SetHeigthBottomPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftBottom)
        {
            SetWidthLeftPart(scalePartBlockSymbol);
            SetHeigthBottomPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftMiddle)
        {
            SetWidthLeftPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftTop)
        {
            SetWidthLeftPart(scalePartBlockSymbol);
            SetHeigthTopPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.MiddleTop)
        {
            SetHeigthTopPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightTop)
        {
            SetWidthRigthPart(scalePartBlockSymbol);
            SetHeigthTopPart(scalePartBlockSymbol);
        }
    }
}