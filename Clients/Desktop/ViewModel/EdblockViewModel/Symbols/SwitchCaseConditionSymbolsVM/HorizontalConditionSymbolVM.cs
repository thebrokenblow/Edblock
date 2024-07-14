using System;
using System.Windows.Media;
using System.Collections.Generic;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

[SymbolType("HorizontalConditionSymbolVM")]
public class HorizontalConditionSymbolVM : SwitchCaseSymbolVM, IHasTextFieldVM, IHasConnectionPoint
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; }
    public List<LineSwitchCase> LinesSwitchCase { get; set; } = null!;
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

    private double DisplacementCoefficient 
    { 
        get => (width + indentBetweenSymbol) * (_countLines - 1) / 2;
    }

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int baselineLength = 20;
    private const int indentBetweenSymbol = 20;
    private const int conditionLineLength = 20;

    private const string defaultText = "Условие";
    private const string defaultColor = "#FF60B2D3";

    public HorizontalConditionSymbolVM(
        IBuilderScaleRectangles builderScaleRectangles,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM,
        int countLines) : base(
            builderScaleRectangles,
            canvasSymbolsComponentVM,
            listCanvasSymbolsComponentVM,
            topSettingsMenuComponentVM,
            popupBoxMenuComponentVM,
            countLines)
    {
        TextFieldSymbolVM = new(_canvasSymbolsComponentVM, this)
        {
            Text = defaultText
        };

        Color = defaultColor;

        AddConnectionPoints();
        AddLinesSwitchCase(countLines);

        SetWidth(defaultWidth);
        SetHeight(defaultHeigth);
    }

    public override void SetWidth(double width)
    {
        Width = width;

        TextFieldSymbolVM.Width = width / 2;
        TextFieldSymbolVM.LeftOffset = width / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();

        foreach (var connectionPointVM in ConnectionPointsVM)
        {
            connectionPointVM.SetCoordinate();
        }

        foreach (var connectionPointVM in ConnectionPointsSwitchCaseVM)
        {
            connectionPointVM.SetCoordinate();
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

        foreach (var connectionPointVM in ConnectionPointsVM)
        {
            connectionPointVM.SetCoordinate();
        }

        foreach (var connectionPointVM in ConnectionPointsSwitchCaseVM)
        {
            connectionPointVM.SetCoordinate();
        }

        SetCoordinateVerticalLine();
        SetCoordinateHorizontalLine();
        SetCoordinateLinesCondition();
    }

    private double CalculateDisplacementCoefficient() =>
        (width + indentBetweenSymbol) * (_countLines - 1) / 2;

    public void SetCoordinatePolygonPoints()
    {
        double halfWidth = width / 2;
        double halfHeight = height / 2;

        Points =
        [
            new(halfWidth, height),
            new(width, halfHeight),
            new(halfWidth, 0),
            new(0, halfHeight)
        ];
    }

    private void SetCoordinateVerticalLine()
    {
        double halfWidth = width / 2;

        VerticalLineSwitchCase.X1 = halfWidth;
        VerticalLineSwitchCase.Y1 = height;
        VerticalLineSwitchCase.X2 = halfWidth;
        VerticalLineSwitchCase.Y2 = height + baselineLength;
    }

    private void SetCoordinateHorizontalLine()
    {
        double halfWidth = width / 2;
        double y = Height + baselineLength;

        HorizontalLineSwitchCase.X1 = -DisplacementCoefficient + halfWidth;
        HorizontalLineSwitchCase.Y1 = y;
        HorizontalLineSwitchCase.X2 = DisplacementCoefficient + halfWidth;
        HorizontalLineSwitchCase.Y2 = y;
    }

    private void SetCoordinateLinesCondition()
    {
        double y1 = Height + baselineLength;
        double y2 = y1 + conditionLineLength;

        for (int i = 0; i < _countLines; i++)
        {
            double xCoordinateLine = -DisplacementCoefficient + width / 2 + (width + indentBetweenSymbol) * i;

            LinesSwitchCase[i].X1 = xCoordinateLine;
            LinesSwitchCase[i].Y1 = y1;
            LinesSwitchCase[i].X2 = xCoordinateLine;
            LinesSwitchCase[i].Y2 = y2;
        }
    }

    private void AddConnectionPoints()
    {
        var builderConnectionPointsVM = new BuilderConnectionPointsVM(
            _canvasSymbolsComponentVM,
            _lineStateStandardComponentVM,
            this);

        ConnectionPointsVM = builderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddLeftConnectionPoint()
            .Build();
    }

    private void AddLinesSwitchCase(int countLines)
    {
        LinesSwitchCase = new(countLines);

        var builderConnectionPointsVM = new BuilderConnectionPointsVM(
           _canvasSymbolsComponentVM,
           _lineStateStandardComponentVM,
           this);

        for (int i = 0; i < countLines; i++)
        {
            LinesSwitchCase.Add(new());
            builderConnectionPointsVM.AddBottomConnectionPoint(CreateCoordinateConnectionPoint(i), CreateCoordinateStartDrawLine(i));
           
        }

        ConnectionPointsSwitchCaseVM = builderConnectionPointsVM.Build();
    }

    private Func<(double, double)> CreateCoordinateConnectionPoint(int numberConnectionPoint) => 
        () => (-DisplacementCoefficient + Width / 2 + (Width + indentBetweenSymbol) * numberConnectionPoint, Height + baselineLength + conditionLineLength);

    private Func<(double, double)> CreateCoordinateStartDrawLine(int numberConnectionPoint) =>
        () => (XCoordinate + -DisplacementCoefficient + Width / 2 + (Width + indentBetweenSymbol) * numberConnectionPoint, YCoordinate + Height + baselineLength + conditionLineLength);
}