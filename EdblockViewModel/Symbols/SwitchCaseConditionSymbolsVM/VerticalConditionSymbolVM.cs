using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.Enum;
using EdblockModel.SymbolsModel;
using EdblockModel.AbstractionsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

public class VerticalConditionSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ConnectionPointVM> ConnectionPoints { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }
    public LineSwitchCase VerticalLineSwitchCase { get; init; } = new();
    public List<LineSwitchCase> LinesSwitchCase { get; init; }

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

    private readonly int _countLines;

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int indentBetweenSymbol = 20;

    private const string defaultText = "Условие";
    private const string defaultColor = "#FF60B2D3";

    public VerticalConditionSymbolVM(EdblockVM edblockVM, int countLines) : base(edblockVM)
    {

        var canvasSymbolsVM = edblockVM.CanvasSymbolsVM;
        var scaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM;

        TextFieldSymbolVM = new(canvasSymbolsVM, this);
        BuilderScaleRectangles = new(canvasSymbolsVM, scaleAllSymbolVM, this);

        _countLines = countLines;

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

        LinesSwitchCase = new(countLines);
        ConnectionPoints = new(countLines);

        for (int i = 0; i < countLines; i++)
        {
            LinesSwitchCase.Add(new());

            var bottomConnectionPoint = new ConnectionPointVM(
                CanvasSymbolsVM,
                this,
                _checkBoxLineGostVM,
                SideSymbol.Right);

            ConnectionPoints.Add(bottomConnectionPoint);
        }

        Color = defaultColor;

        Width = defaultWidth;
        Height = defaultHeigth;

        TextFieldSymbolVM.Text = defaultText;

        BlockSymbolModel = CreateBlockSymbolModel();

        SetWidth(Width);
        SetHeight(Height);
    }

    public override void SetWidth(double width)
    {
        Width = width;

        var textFieldWidth = Width / 2;
        var textFieldLeftOffset = Width / 4;

        TextFieldSymbolVM.Width = textFieldWidth;
        TextFieldSymbolVM.LeftOffset = textFieldLeftOffset;

        SetCoordinatePolygonPoints();
        ChangeCoordinateAuxiliaryElements();

        SetCoordinateLinesCondition();
        SetCoordinateVerticalLineSwitchCase();

        for (int i = 0; i < ConnectionPoints.Count; i++)
        {
            ConnectionPoints[i].XCoordinate = Width;
            ConnectionPoints[i].XCoordinateLineDraw = Width;
        }
    }

    public override void SetHeight(double height)
    {
        Height = height;

        var textFieldHeight = Height / 2;
        var textFieldTopOffset = Height / 4;

        TextFieldSymbolVM.Height = textFieldHeight;
        TextFieldSymbolVM.TopOffset = textFieldTopOffset;

        SetCoordinatePolygonPoints();
        ChangeCoordinateAuxiliaryElements();

        SetCoordinateLinesCondition();
        SetCoordinateVerticalLineSwitchCase();

        for (int i = 1; i <= ConnectionPoints.Count ; i++)
        {
            double heightLine = Height / 2 + (Height + indentBetweenSymbol) * i;

            ConnectionPoints[i - 1].YCoordinate = heightLine;
            ConnectionPoints[i - 1].YCoordinateLineDraw = heightLine;
        }
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

    private void SetCoordinateVerticalLineSwitchCase()
    {
        VerticalLineSwitchCase.X1 = Width / 2;
        VerticalLineSwitchCase.Y1 = Height;
        VerticalLineSwitchCase.X2 = Width / 2;
        VerticalLineSwitchCase.Y2 = Height / 2 + (Height + indentBetweenSymbol) * _countLines;
    }

    private void SetCoordinateLinesCondition()
    {
        for (int i = 1; i <= _countLines; i++)
        {
            double heightLine = Height / 2 + (Height + indentBetweenSymbol) * i;

            LinesSwitchCase[i - 1].X1 = Width / 2;
            LinesSwitchCase[i - 1].Y1 = heightLine;
            LinesSwitchCase[i - 1].X2 = Width;
            LinesSwitchCase[i - 1].Y2 = heightLine;
        }
    }

    private void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(Width / 2, Height),
            new Point(Width, Height / 2),
            new Point(Width / 2, 0),
            new Point(0, Height / 2)
        };
    }
}