using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM;
using Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Attributes;
using Edblock.PagesViewModel.Pages;

namespace Edblock.SymbolsViewModel.Symbols;

[SymbolType("ActionSymbolViewModel")]

public class ActionSymbolViewModel : BlockSymbolViewModel, IHasTextField, IHasConnection, IHasScaleRectangles
{
    public TextFieldSymbol TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; set; } = null!;
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";

    public ActionSymbolViewModel(EditorViewModel editorViewModel) : base(editorViewModel)
    {
        TextFieldSymbolVM = new(CanvasSymbolsVM, this)
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
        TextFieldSymbolVM.Width = width;

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