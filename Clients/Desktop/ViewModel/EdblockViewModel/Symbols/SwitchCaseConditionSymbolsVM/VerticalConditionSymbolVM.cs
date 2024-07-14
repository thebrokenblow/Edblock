using System.Windows.Media;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;
using System;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

[SymbolType("VerticalConditionSymbolVM")]
public sealed class VerticalConditionSymbolVM : SwitchCaseSymbolVM, IHasTextFieldVM, IHasConnectionPoint
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<LineSwitchCase> LinesSwitchCase { get; set; } = null!;
    public List<ConnectionPointVM> ConnectionPointsVM { get; set; } = null!;
    public LineSwitchCase VerticalLineSwitchCase { get; init; } = new();

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

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

    private const int indentBetweenSymbol = 20;

    private const string defaultText = "Условие";
    private const string defaultColor = "#FF60B2D3";

    public VerticalConditionSymbolVM(
        IBuilderScaleRectangles builderScaleRectangles,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM,
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM, int countLines) : base(
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

        coordinateConnectionPointVM = new(this);

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

        foreach (var connectionPointsVM in ConnectionPointsVM)
        {
            connectionPointsVM.SetCoordinate();
        }

        foreach (var connectionPointsSwitchCaseVM in ConnectionPointsSwitchCaseVM)
        {
            connectionPointsSwitchCaseVM.SetCoordinate();
        }

        SetCoordinateLinesCondition();
        SetCoordinateVerticalLineSwitchCase();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        var halfHeight = height / 2;

        TextFieldSymbolVM.Height = height / 2;
        TextFieldSymbolVM.TopOffset = height / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();

        foreach (var connectionPointsVM in ConnectionPointsVM)
        {
            connectionPointsVM.SetCoordinate();
        }

        foreach (var connectionPointsSwitchCaseVM in ConnectionPointsSwitchCaseVM)
        {
            connectionPointsSwitchCaseVM.SetCoordinate();
        }

        SetCoordinateLinesCondition();
        SetCoordinateVerticalLineSwitchCase();
    }

    private void SetCoordinateVerticalLineSwitchCase()
    {
        var halfWidth = width / 2;
        var halfHeight = height / 2;

        VerticalLineSwitchCase.X1 = halfWidth;
        VerticalLineSwitchCase.Y1 = height;
        VerticalLineSwitchCase.X2 = halfWidth;
        VerticalLineSwitchCase.Y2 = halfHeight + (height + indentBetweenSymbol) * _countLines;
    }

    private void SetCoordinateLinesCondition()
    {
        for (int i = 1; i <= _countLines; i++)
        {
            double heightLine = height / 2 + (height + indentBetweenSymbol) * i;

            LinesSwitchCase[i - 1].X1 = width / 2;
            LinesSwitchCase[i - 1].Y1 = heightLine;
            LinesSwitchCase[i - 1].X2 = width;
            LinesSwitchCase[i - 1].Y2 = heightLine;
        }
    }

    private void SetCoordinatePolygonPoints()
    {
        var halfWidth = width / 2;
        var halfHeight = height / 2;

        Points =
        [
            new(halfWidth, height),
            new(width, halfHeight),
            new(halfWidth, 0),
            new(0, halfHeight)
        ];
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

        for (int i = 1; i <= countLines; i++)
        {
            LinesSwitchCase.Add(new());
            builderConnectionPointsVM.AddBottomConnectionPoint(CreateCoordinateConnectionPoint(i), CreateCoordinateStartDrawLine(i));

        }

        ConnectionPointsSwitchCaseVM = builderConnectionPointsVM.Build();
    }

    private Func<(double, double)> CreateCoordinateConnectionPoint(int numberConnectionPoint)
    {
        return () => (width, height / 2 + (height + indentBetweenSymbol) * numberConnectionPoint);
    }

    private Func<(double, double)> CreateCoordinateStartDrawLine(int numberConnectionPoint)
    {
        return () => (xCoordinate + width, yCoordinate + height / 2 + (height + indentBetweenSymbol) * numberConnectionPoint);
    }
}