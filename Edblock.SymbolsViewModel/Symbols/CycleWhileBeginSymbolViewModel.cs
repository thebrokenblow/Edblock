using System.Windows.Media;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Attributes;
using Edblock.PagesViewModel.Pages;

namespace Edblock.SymbolsViewModel.Symbols;

[SymbolType("CycleWhileBeginSymbolVM")]

public class CycleWhileBeginSymbolViewModel : BlockSymbolViewModel, IHasTextField, IHasConnection, IHasScaleRectangles
{
    public TextFieldSymbol TextFieldSymbolVM { get; init; }
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

    public CycleWhileBeginSymbolViewModel(EditorViewModel editorViewModel) : base(editorViewModel)
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
        Points =
        [
            new(0, Height),
            new(0, sideProjection),
            new(sideProjection, 0),
            new(Width - sideProjection, 0),
            new(Width, sideProjection),
            new(Width, Height),
        ];
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
