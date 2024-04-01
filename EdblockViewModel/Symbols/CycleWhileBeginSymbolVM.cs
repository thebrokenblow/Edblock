using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockViewModel.AttributesVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols;

[SymbolType("CycleWhileBeginSymbolVM")]

public class CycleWhileBeginSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
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
    
    private const int sideProjection = 10;

    private const string defaultText = "Начало цикла";
    private const string defaultColor = "#CCCCFF";

    public CycleWhileBeginSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        TextFieldSymbolVM = new(CanvasSymbolsVM, this)
        {
            Text = defaultText,
            LeftOffset = sideProjection
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
        TextFieldSymbolVM.Width = width - sideProjection * 2;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();
    }

    public override void SetHeight(double height)
    {
        Height = height;
        TextFieldSymbolVM.Height = height;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(0, Height),
            new Point(0, sideProjection),
            new Point(sideProjection, 0),
            new Point(Width - sideProjection, 0),
            new Point(Width, sideProjection),
            new Point(Width, Height),
        };
    }

    private void AddConnectionPoints()
    {
        var builderConnectionPointsVM = new BuilderConnectionPointsVM(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM);

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
            CanvasSymbolsVM,
           _scaleAllSymbolVM,
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
