using System.Windows.Media;
using System.Collections.Generic;
using EdblockModel.EnumsModel;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.Attributes;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;

namespace EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

[SymbolType("HorizontalConditionSymbolVM")]
public sealed class HorizontalConditionSymbolVM : SwitchCaseSymbolVM, IHasTextFieldVM, IHasConnectionPoint
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

    private readonly CoordinateConnectionPointVM coordinateConnectionPointVM;

    private double displacementCoefficient;

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

        HorizontalLineSwitchCase.X1 = -displacementCoefficient + halfWidth;
        HorizontalLineSwitchCase.Y1 = y;
        HorizontalLineSwitchCase.X2 = displacementCoefficient + halfWidth;
        HorizontalLineSwitchCase.Y2 = y;
    }

    private void SetCoordinateLinesCondition()
    {
        double y1 = Height + baselineLength;
        double y2 = y1 + conditionLineLength;
        double halfWidth = width / 2;

        for (int i = 0; i < _countLines; i++)
        {
            double xCoordinateLine = -displacementCoefficient + halfWidth + (width + indentBetweenSymbol) * i;

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
            this,
            lineStateStandardComponentVM);

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

            //var bottomConnectionPoint = new ConnectionPointVM(
            //    _canvasSymbolsComponentVM,
            //    lineStateStandardComponentVM,
            //    this,
            //    SideSymbol.Bottom);

            //ConnectionPointsSwitchCaseVM.Add(bottomConnectionPoint);
        }
    }
}
