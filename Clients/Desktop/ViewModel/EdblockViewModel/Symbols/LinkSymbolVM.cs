using System;
using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;

namespace EdblockViewModel.Symbols;

[SymbolType("LinkSymbolVM")]
public sealed class LinkSymbolVM : ScalableBlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; }
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private const int defaultWidth = 60;
    private const int defaultHeigth = 60;

    private const string defaultText = "Ссылка";
    private const string defaultColor = "#FF5761A8";

    public LinkSymbolVM(
        IBuilderScaleRectangles builderScaleRectangles,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : base(
            builderScaleRectangles, 
            canvasSymbolsComponentVM, 
            listCanvasSymbolsComponentVM, 
            topSettingsMenuComponentVM, 
            popupBoxMenuComponentVM)
    {
        TextFieldSymbolVM = new(_canvasSymbolsComponentVM, this)
        {
            Text = defaultText
        };

        Color = defaultColor;

        AddConnectionPoints();

        coordinateConnectionPointVM = new(this);

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }


    public override void SetSize(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftBottom)
        {
            SetWidthLeftPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftTop)
        {
            int currentXCoordinateCursor = _canvasSymbolsComponentVM.XCoordinate;
            double initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
            double initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;
            double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;
            double widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

            if (widthBlockSymbol < 40)
            {
                widthBlockSymbol = 40;
            }

            SetWidth(widthBlockSymbol);

            XCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);
            YCoordinate = initialYCoordinate - (widthBlockSymbol - initialWidth);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightTop)
        {
            SetHeigthTopPart(scalePartBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightBottom)
        {
            SetHeigthBottomPart(scalePartBlockSymbol);
        }
    }

    public override void SetHeight(double height)
    {
        SetSize(height);
    }

    public override void SetWidth(double width)
    {
        SetSize(width);
    }

    private void SetSize(double size)
    {
        Height = size;
        Width = size;

        var textFieldSize = Math.Sqrt(size * size / 2);
        var textFieldOffset = (size - textFieldSize) / 2;

        TextFieldSymbolVM.Width = textFieldSize;
        TextFieldSymbolVM.Height = textFieldSize;

        TextFieldSymbolVM.LeftOffset = textFieldOffset;
        TextFieldSymbolVM.TopOffset = textFieldOffset;

        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();
    }

    protected override List<ScaleRectangle> CreateScaleRectangles() =>
        BuilderScaleRectangles
        .AddRightTopRectangle()
        .AddRightBottomRectangle()
        .AddLeftBottomRectangle()
        .AddLeftTopRectangle()
        .Build();

    private void AddConnectionPoints()
    {
        var builderConnectionPointsVM = new BuilderConnectionPointsVM(
            _canvasSymbolsComponentVM,
            this,
            lineStateStandardComponentVM);

        ConnectionPointsVM = builderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddBottomConnectionPoint()
            .AddLeftConnectionPoint()
            .Build();
    }
}