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

public class HorizontalConditionSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; } = new();
    public List<ConnectionPointVM> ConnectionPoints { get; init; } = new();
    public List<LineSwitchCase> LinesSwitchCase { get; init; } = new();
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }

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

    private readonly double displacementCoefficient;
    private readonly int _countLines;

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int baselineLength = 20;
    private const int indentBetweenSymbol = 20;
    private const int conditionLineLength = 20;

    private const string defaultText = "Условие";
    private const string defaultColor = "#FF60B2D3";

    public HorizontalConditionSymbolVM(EdblockVM edblockVM, int countLines) : base(edblockVM)
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

        Color = defaultColor;

        TextFieldSymbolVM.Text = defaultText;
        BlockSymbolModel = CreateBlockSymbolModel();

        Width = defaultWidth;
        Height = defaultHeigth;

        displacementCoefficient = (Width + indentBetweenSymbol) * (countLines - 1) / 2;

        SetWidth(Width);
        SetHeight(Height);

        AddVerticalLine();
        AddHorizontalLine();
        AddLinesCondition();
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

    private void AddVerticalLine()
    {
        double xCoordinateVerticalLine = Width / 2;

        var verticalLine = new LineSwitchCase
        {
            X1 = xCoordinateVerticalLine,
            Y1 = Height,
            X2 = xCoordinateVerticalLine,
            Y2 = Height + baselineLength,
        };

        LinesSwitchCase.Add(verticalLine);
    }

    private void AddHorizontalLine()
    {
        var horizontalLine = new LineSwitchCase
        {
            X1 = -displacementCoefficient + Width / 2,
            Y1 = Height + baselineLength,
            X2 = displacementCoefficient + Width / 2,
            Y2 = Height + baselineLength,
        };

        LinesSwitchCase.Add(horizontalLine);
    }

    private void AddLinesCondition()
    {
        double y1LineSwitchCase = Height + baselineLength;
        double y2LineSwitchCase = y1LineSwitchCase + conditionLineLength;

        for (int i = 0; i < _countLines; i++)
        {
            double xCoordinateLine = -displacementCoefficient + (Width + indentBetweenSymbol) * i + Width / 2;

            var lineSwitchCase = AddConditionLine(xCoordinateLine, y1LineSwitchCase, y2LineSwitchCase);
            AddConnectionPoint(lineSwitchCase, xCoordinateLine, y2LineSwitchCase);
        }
    }

    private LineSwitchCase AddConditionLine(double xCoordinateLine, double y1LineSwitchCase, double y2LineSwitchCase)
    {
        var LineSwitchCase = new LineSwitchCase()
        {
            X1 = xCoordinateLine,
            Y1 = y1LineSwitchCase,
            X2 = xCoordinateLine,
            Y2 = y2LineSwitchCase,
        };

        LinesSwitchCase.Add(LineSwitchCase);

        return LineSwitchCase;
    }

    private void AddConnectionPoint(LineSwitchCase lineSwitchCase, double xCoordinateLine, double y2LineSwitchCase)
    {
        var coordinateConnectionPoint = () => 
            (xCoordinateLine - 4, y2LineSwitchCase - 4);

        var coordinateConnectionPoint1 = () =>
           (xCoordinateLine + XCoordinate, y2LineSwitchCase + YCoordinate);

        var bottomConnectionPoint = new ConnectionPointVM(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM,
            coordinateConnectionPoint,
            coordinateConnectionPoint1,
            SideSymbol.Bottom);


        ConnectionPoints.Add(bottomConnectionPoint);
    }
}
