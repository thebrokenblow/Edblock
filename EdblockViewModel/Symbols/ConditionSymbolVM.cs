using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.SymbolsModel;
using EdblockModel.AbstractionsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace EdblockViewModel.Symbols;

public class ConditionSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public List<ConnectionPointVM> ConnectionPoints { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }
    public FactoryConnectionPoints FactoryConnectionPoints { get; init; }

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

    public ConditionSymbolVM(EdblockVM edblockVM) : base(edblockVM)
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

    public override void SetWidth(double width)
    {     
        Width = width;

        var textFieldWidth = GetTextFieldWidth();
        var textFieldLeftOffset = GetTextFieldLeftOffset();

        TextFieldSymbolVM.Width = textFieldWidth;
        TextFieldSymbolVM.LeftOffset = textFieldLeftOffset;

        SetCoordinatePolygonPoints();
        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        var textFieldHeight = GetTextFieldHeight();
        var textFieldTopOffset = GetTextFieldTopOffset();

        TextFieldSymbolVM.Height = textFieldHeight;
        TextFieldSymbolVM.TopOffset = textFieldTopOffset;

        SetCoordinatePolygonPoints();
        ChangeCoordinateAuxiliaryElements();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolVM = GetType().BaseType?.ToString();

        var conditionSymbolModel = new ConditionSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color,
        };

        return conditionSymbolModel;
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(Width / 2, Height),
            new Point(Width, Height / 2),
            new Point(Width / 2, 0),
            new Point(0, Height / 2)
        };
    }

    public double GetTextFieldWidth()
    {
        return Width / 2;
    }

    public double GetTextFieldHeight()
    {
        return Height / 2;
    }

    public double GetTextFieldLeftOffset()
    {
        return Width / 4;
    }

    public double GetTextFieldTopOffset()
    {
        return Height / 4;
    }
}