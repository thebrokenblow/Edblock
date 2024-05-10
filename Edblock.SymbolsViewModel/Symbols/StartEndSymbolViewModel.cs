using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Attributes;
using Edblock.PagesViewModel.Pages;

namespace Edblock.SymbolsViewModel.Symbols;

[SymbolType("StartEndSymbolViewModel")]

public class StartEndSymbolViewModel : BlockSymbolViewModel, IHasTextField, IHasConnection, IHasScaleRectangles
{
    public TextFieldSymbol TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; set; } = null!;
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int offsetTextField = 25;

    private const string defaultText = "Начало / Конец";
    private const string defaultColor = "#FFF25252";

    public StartEndSymbolViewModel(EditorViewModel editorViewModel) : base(editorViewModel)
    {
        TextFieldSymbolVM = new(editorViewModel.CanvasSymbolsVM, this)
        {
            Text = defaultText
        };

        Color = defaultColor;
        coordinateConnectionPointVM = new(this);

        AddScaleRectangles();
        AddConnectionPoints();

        coordinateConnectionPointVM = new(this);

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;
        TextFieldSymbolVM.Width = width - offsetTextField;
        TextFieldSymbolVM.LeftOffset = offsetTextField / 2;

        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();
    }

    public override void SetHeight(double height)
    {
        Height = height;
        TextFieldSymbolVM.Height = height;

        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();
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