using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols;

public class StartEndSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ConnectionPointVM> ConnectionPointsVM { get; init; }
    public BuilderConnectionPointsVM BuilderConnectionPointsVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }
    public CoordinateConnectionPointVM CoordinateConnectionPointVM { get; init; }

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int offsetTextField = 25;

    private const string defaultText = "Начало / Конец";
    private const string defaultColor = "#FFF25252";

    public StartEndSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        TextFieldSymbolVM = new(edblockVM.CanvasSymbolsVM, this);

        BuilderConnectionPointsVM = new(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM);

        ConnectionPointsVM = BuilderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddBottomConnectionPoint()
            .AddLeftConnectionPoint()
            .Create();

        CoordinateConnectionPointVM = new(this);

        BuilderScaleRectangles = new(
            CanvasSymbolsVM,
            edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM,
            this);

        ScaleRectangles =
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

        Color = defaultColor;

        TextFieldSymbolVM.Text = defaultText;

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;

        TextFieldSymbolVM.Width = Width - offsetTextField; 
        TextFieldSymbolVM.LeftOffset = offsetTextField / 2; 

        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        TextFieldSymbolVM.Height = height;

        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();
    }
}