using System;
using System.Collections.Generic;
using EdblockModel.SymbolsModel;
using EdblockModel.AbstractionsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace EdblockViewModel.Symbols;

public class LinkSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public List<ConnectionPointVM> ConnectionPoints { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }
    public FactoryConnectionPoints FactoryConnectionPoints { get; init; }

    private const int defaultWidth = 60;
    private const int defaultHeigth = 60;

    private const string defaultText = "Ссылка";
    private const string defaultColor = "#FF5761A8";

    public LinkSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        TextFieldSymbolVM = new(edblockVM.CanvasSymbolsVM, this);

        FactoryConnectionPoints = new(CanvasSymbolsVM, edblockVM.PopupBoxMenuVM.CheckBoxLineGostVM, this);
        ConnectionPoints = FactoryConnectionPoints.CreateConnectionPoints();

        BuilderScaleRectangles = new(CanvasSymbolsVM, edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM, this);

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

        Width = defaultWidth;
        Height = defaultHeigth;

        SetWidth(Width);
        SetHeight(Height);
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

        var textFieldWidth = GetTextFieldWidth();
        var textFieldLeftOffset = GetTextFieldLeftOffset();

        var textFieldHeight = GetTextFieldHeight();
        var textFieldTopOffset = GetTextFieldTopOffset();

        TextFieldSymbolVM.Width = textFieldWidth;
        TextFieldSymbolVM.Height = textFieldHeight;

        TextFieldSymbolVM.LeftOffset = textFieldLeftOffset;
        TextFieldSymbolVM.TopOffset = textFieldTopOffset;

        ChangeCoordinateAuxiliaryElements();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolVM = GetType().BaseType?.ToString();

        var linkSymbolModel = new LinkSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color
        };

        return linkSymbolModel;
    }

    public double GetTextFieldWidth()
    {
        return Math.Sqrt(Height * Height / 2);
    }

    public double GetTextFieldHeight()
    {
        return Math.Sqrt(Height * Height / 2);
    }

    public double GetTextFieldLeftOffset()
    {
        return (Height - Math.Sqrt(Height * Height / 2)) / 2;
    }

    public double GetTextFieldTopOffset()
    {
        return (Height - Math.Sqrt(Height * Height / 2)) / 2;
    }
}