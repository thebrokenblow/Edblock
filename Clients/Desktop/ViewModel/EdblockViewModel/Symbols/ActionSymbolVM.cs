using System.Collections.Generic;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols;

[SymbolType("ActionSymbolVM")]
public class ActionSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; set; } = null!;
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";

    public ActionSymbolVM(
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