using System.Windows.Media;
using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols;

[SymbolType("ConditionSymbolVM")]
public class ConditionSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; set; } = null!;
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;
    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private PointCollection? points;
    public PointCollection? Points 
    {
        get => points;
        set
        {
            points = value;
            OnPropertyChanged();
        }
    }

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const string defaultText = "Условие";
    private const string defaultColor = "#FF60B2D3";

    public ConditionSymbolVM(
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

    public override void SetWidth(double width)
    {     
        Width = width;

        TextFieldSymbolVM.Width = width / 2; 
        TextFieldSymbolVM.LeftOffset = width / 4; 

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        TextFieldSymbolVM.Height = height / 2;
        TextFieldSymbolVM.TopOffset = height / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();
    }

    public void SetCoordinatePolygonPoints()
    {
        var halfWidth = Width / 2;
        var halfHeight = Height / 2;

        Points =
        [
            new(halfWidth, height),
            new(width, halfHeight),
            new(halfWidth, 0),
            new(0, halfHeight)
        ];
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
                        .AddMiddleTopRectangle()
                        .AddRightTopRectangle()
                        .AddRightMiddleRectangle()
                        .AddRightBottomRectangle()
                        .AddMiddleBottomRectangle()
                        .AddLeftBottomRectangle()
                        .AddLeftMiddleRectangle()
                        .AddLeftTopRectangle()
                        .Build();
    }
}