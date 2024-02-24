using System;
using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols;

public class LinkSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ConnectionPointVM> ConnectionPointsVM { get; init; }
    public BuilderConnectionPointsVM BuilderConnectionPointsVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }
    public CoordinateConnectionPointVM CoordinateConnectionPointVM { get; init; }

    private const int defaultWidth = 60;
    private const int defaultHeigth = 60;

    private const string defaultText = "Ссылка";
    private const string defaultColor = "#FF5761A8";

    public LinkSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        TextFieldSymbolVM = new(CanvasSymbolsVM, this);

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
                        .AddRightTopRectangle()
                        .AddRightBottomRectangle()
                        .AddLeftBottomRectangle()
                        .AddLeftTopRectangle()
                        .Build();

        Color = defaultColor;

        TextFieldSymbolVM.Text = defaultText;

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetHeight(double height)
    {
        SetSize(height);
    }

    public override void SetWidth(double width)
    {
        SetSize(width);
    }

    public void SetSize(double size)
    {
        Height = size;
        Width = size;

        var textFieldSize = Math.Sqrt(size * size / 2);
        var textFieldOffset = (size - Math.Sqrt(size * size / 2)) / 2;

        TextFieldSymbolVM.Width = textFieldSize;
        TextFieldSymbolVM.Height = textFieldSize;

        TextFieldSymbolVM.LeftOffset = textFieldOffset;
        TextFieldSymbolVM.TopOffset = textFieldOffset;

        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();
    }
}