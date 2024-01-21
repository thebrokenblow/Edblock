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

public class InputOutputSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
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

    private const int sideProjection = 20;

    private const string defaultText = "Ввод / Вывод";
    private const string defaultColor = "#FF008080";

    public InputOutputSymbolVM(EdblockVM edblockVM) : base(edblockVM)
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

        var inputOutputSymbolModel = new InputOutputSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color
        };

        return inputOutputSymbolModel;
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(sideProjection, 0),
            new Point(0, Height),
            new Point(Width - sideProjection, Height),
            new Point(Width, 0)
        };
    }

    public double GetTextFieldWidth()
    {
        return Width - sideProjection;
    }

    public double GetTextFieldHeight()
    {
        return Height;
    }

    public double GetTextFieldLeftOffset()
    {
        return sideProjection / 2;
    }

    public double GetTextFieldTopOffset()
    {
        return 0;
    }
}