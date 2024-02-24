using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.EnumsModel;
using EdblockViewModel.AttributeVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

[SymbolType("HorizontalConditionSymbolVM")]

public class HorizontalConditionSymbolVM : SwitchCaseSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<LineSwitchCase> LinesSwitchCase { get; set; } = null!;
    public List<ScaleRectangle> ScaleRectangles { get; set; } = null!;
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;
    public LineSwitchCase VerticalLineSwitchCase { get; init; } = new();
    public LineSwitchCase HorizontalLineSwitchCase { get; init; } = new();

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

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private double displacementCoefficient;

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int baselineLength = 20;
    private const int indentBetweenSymbol = 20;
    private const int conditionLineLength = 20;

    private const string defaultText = "Условие";
    private const string defaultColor = "#FF60B2D3";

    public HorizontalConditionSymbolVM(EdblockVM edblockVM, int countLines) : base(edblockVM, countLines)
    {
        TextFieldSymbolVM = new(CanvasSymbolsVM, this)
        {
            Text = defaultText
        };

        Color = defaultColor;

        AddScaleRectangles();
        AddConnectionPoints();
        AddLinesSwitchCase(countLines);

        coordinateConnectionPointVM = new(this);

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;

        displacementCoefficient = CalculateDisplacementCoefficient();

        TextFieldSymbolVM.Width = width / 2;
        TextFieldSymbolVM.LeftOffset = width / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();

        var xCoordinateConnectionPoint = -displacementCoefficient + width / 2;
        
        foreach (var connectionPoint in ConnectionPointsSwitchCaseVM)
        {
            connectionPoint.XCoordinate = xCoordinateConnectionPoint;
            connectionPoint.XCoordinateLineDraw = xCoordinateConnectionPoint;

            xCoordinateConnectionPoint += width + indentBetweenSymbol;
        }

        SetCoordinateVerticalLine();
        SetCoordinateHorizontalLine();
        SetCoordinateLinesCondition();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        TextFieldSymbolVM.Height = height / 2;
        TextFieldSymbolVM.TopOffset = height / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        coordinateConnectionPointVM.SetCoordinate();

        var yCoordinateConnectionPoint = height + baselineLength + conditionLineLength;

        foreach (var connectionPoint in ConnectionPointsSwitchCaseVM)
        {
            connectionPoint.YCoordinate = yCoordinateConnectionPoint;
            connectionPoint.YCoordinateLineDraw = yCoordinateConnectionPoint;
        }

        SetCoordinateVerticalLine();
        SetCoordinateHorizontalLine();
        SetCoordinateLinesCondition();
    }

    private double CalculateDisplacementCoefficient() =>
        (Width + indentBetweenSymbol) * (_countLines - 1) / 2;

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

    private void SetCoordinateVerticalLine()
    {
        double x = Width / 2;

        VerticalLineSwitchCase.X1 = x;
        VerticalLineSwitchCase.Y1 = Height;
        VerticalLineSwitchCase.X2 = x;
        VerticalLineSwitchCase.Y2 = Height + baselineLength;
    }

    private void SetCoordinateHorizontalLine()
    {
        double y = Height + baselineLength;

        HorizontalLineSwitchCase.X1 = -displacementCoefficient + Width / 2;
        HorizontalLineSwitchCase.Y1 = y;
        HorizontalLineSwitchCase.X2 = displacementCoefficient + Width / 2;
        HorizontalLineSwitchCase.Y2 = y;
    }

    private void SetCoordinateLinesCondition()
    {
        double y1 = Height + baselineLength;
        double y2 = y1 + conditionLineLength;

        for (int i = 0; i < _countLines; i++)
        {
            double xCoordinateLine = -displacementCoefficient + Width / 2 + (Width + indentBetweenSymbol) * i;

            LinesSwitchCase[i].X1 = xCoordinateLine;
            LinesSwitchCase[i].Y1 = y1;
            LinesSwitchCase[i].X2 = xCoordinateLine;
            LinesSwitchCase[i].Y2 = y2;
        }
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

    private void AddConnectionPoints()
    {
        var builderConnectionPointsVM = new BuilderConnectionPointsVM(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM);

        ConnectionPointsVM = builderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddLeftConnectionPoint()
            .Build();
    }

    private void AddLinesSwitchCase(int countLines)
    {
        LinesSwitchCase = new(countLines);

        for (int i = 0; i < countLines; i++)
        {
            LinesSwitchCase.Add(new());

            var bottomConnectionPoint = new ConnectionPointVM(
                CanvasSymbolsVM,
                this,
                _checkBoxLineGostVM,
                SideSymbol.Bottom);

            ConnectionPointsSwitchCaseVM.Add(bottomConnectionPoint);
        }
    }
}
