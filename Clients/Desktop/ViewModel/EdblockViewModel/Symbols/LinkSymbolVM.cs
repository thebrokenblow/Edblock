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
using EdblockViewModel.Components.CanvasSymbols;

namespace EdblockViewModel.Symbols;

[SymbolType("LinkSymbolVM")]
public class LinkSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; set; } = null!;
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private const int defaultWidth = 60;
    private const int defaultHeigth = 60;

    private const string defaultText = "Ссылка";
    private const string defaultColor = "#FF5761A8";

    public LinkSymbolVM(
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : base(canvasSymbolsComponentVM, listCanvasSymbolsComponentVM, topSettingsMenuComponentVM, popupBoxMenuComponentVM)
    {
        TextFieldSymbolVM = new(base._canvasSymbolsComponentVM, this)
        {
            Text = defaultText
        };

        Color = defaultColor;

        AddScaleRectangles();
        AddConnectionPoints();

        coordinateConnectionPointVM = new(this);

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetSize(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftBottom)
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
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftTop)
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

            int currentYCoordinateCursor = _canvasSymbolsComponentVM.YCoordinate;
            double initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
            double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            YCoordinate = initialYCoordinate - (widthBlockSymbol - initialWidth);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightTop)
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
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightBottom)
        {
            double heigthBlockSymbol = _canvasSymbolsComponentVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            if (heigthBlockSymbol < 40)
            {
                heigthBlockSymbol = 40;
            }

            SetHeight(heigthBlockSymbol);
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

    private void AddScaleRectangles()
    {
        var builderScaleRectangles = new BuilderScaleRectangles(
            _canvasSymbolsComponentVM,
           scaleAllSymbolComponentVM,
           this);

        ScaleRectangles =
            builderScaleRectangles
                        .AddRightTopRectangle()
                        .AddRightBottomRectangle()
                        .AddLeftBottomRectangle()
                        .AddLeftTopRectangle()
                        .Build();
    }
}