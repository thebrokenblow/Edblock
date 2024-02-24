using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.EnumsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

public class HorizontalConditionSymbolVM : SwitchCaseSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; } = new();
    public CoordinateConnectionPointVM CoordinateConnectionPointVM { get; init; }
    public LineSwitchCase VerticalLineSwitchCase { get; init; } = new();
    public LineSwitchCase HorizontalLineSwitchCase { get; init; } = new();
    public List<LineSwitchCase> LinesSwitchCase { get; init; }
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

    public List<ConnectionPointVM> ConnectionPointsVM { get; init; }

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
        var canvasSymbolsVM = edblockVM.CanvasSymbolsVM;
        var scaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM;

        var BuilderConnectionPointsVM = new BuilderConnectionPointsVM(
        CanvasSymbolsVM,
        this,
        _checkBoxLineGostVM);

        ConnectionPointsVM = BuilderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddLeftConnectionPoint()
            .Create();

        TextFieldSymbolVM = new(canvasSymbolsVM, this);
        BuilderScaleRectangles = new(canvasSymbolsVM, scaleAllSymbolVM, this);

        CoordinateConnectionPointVM = new(this);

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

        Color = defaultColor;

        TextFieldSymbolVM.Text = defaultText;

        displacementCoefficient = CalculateDisplacementCoefficient();

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;

        displacementCoefficient = CalculateDisplacementCoefficient();

        TextFieldSymbolVM.Width = Width / 2;
        TextFieldSymbolVM.LeftOffset = Width / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();

        var xCoordinateConnectionPoint = -displacementCoefficient + Width / 2;
        
        foreach (var connectionPoint in ConnectionPointsSwitchCaseVM)
        {
            connectionPoint.XCoordinate = xCoordinateConnectionPoint;
            connectionPoint.XCoordinateLineDraw = xCoordinateConnectionPoint;

            xCoordinateConnectionPoint += Width + indentBetweenSymbol;
        }

        SetCoordinateVerticalLine();
        SetCoordinateHorizontalLine();
        SetCoordinateLinesCondition();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        TextFieldSymbolVM.Height = Height / 2;
        TextFieldSymbolVM.TopOffset = Height / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();

        var yCoordinateConnectionPoint = Height + baselineLength + conditionLineLength;

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
}
