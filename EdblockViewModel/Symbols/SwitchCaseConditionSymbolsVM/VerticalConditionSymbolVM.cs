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

[SymbolType("VerticalConditionSymbolVM")]
public class VerticalConditionSymbolVM : SwitchCaseSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public CoordinateConnectionPointVM CoordinateConnectionPointVM { get; init; }
    public List<ConnectionPointVM> ConnectionPointsVM { get; init; }
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

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const int indentBetweenSymbol = 20;

    private const string defaultText = "Условие";
    private const string defaultColor = "#FF60B2D3";

    public VerticalConditionSymbolVM(EdblockVM edblockVM, int countLines) : base(edblockVM, countLines)
    {
        var canvasSymbolsVM = edblockVM.CanvasSymbolsVM;
        var scaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM;

        TextFieldSymbolVM = new(canvasSymbolsVM, this)
        {
            Text = defaultText
        };

        var BuilderConnectionPointsVM = new BuilderConnectionPointsVM(
           CanvasSymbolsVM,
           this,
           _checkBoxLineGostVM);

        ConnectionPointsVM = BuilderConnectionPointsVM
            .AddTopConnectionPoint()
            .AddRightConnectionPoint()
            .AddLeftConnectionPoint()
            .Create();

        //ConnectionPoints = FactoryConnectionPointsVM.Create(
        //    CanvasSymbolsVM,
        //    this,
        //    _checkBoxLineGostVM);

        CoordinateConnectionPointVM = new(this);

        BuilderScaleRectangles = new(canvasSymbolsVM, scaleAllSymbolVM, this);

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
                SideSymbol.Right);

            ConnectionPointsSwitchCaseVM.Add(bottomConnectionPoint);
        }

        Color = defaultColor;

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
        CoordinateConnectionPointVM.SetCoordinate();

        SetCoordinateLinesCondition();
        SetCoordinateVerticalLineSwitchCase();

        for (int i = 0; i < ConnectionPointsSwitchCaseVM.Count; i++)
        {
            base.ConnectionPointsSwitchCaseVM[i].XCoordinate = width;
            base.ConnectionPointsSwitchCaseVM[i].XCoordinateLineDraw = width;
        }
    }

    public override void SetHeight(double height)
    {
        Height = height;

        TextFieldSymbolVM.Height = height / 2;
        TextFieldSymbolVM.TopOffset = height / 4;

        SetCoordinatePolygonPoints();
        ChangeCoordinateScaleRectangle();
        CoordinateConnectionPointVM.SetCoordinate();

        SetCoordinateLinesCondition();
        SetCoordinateVerticalLineSwitchCase();

        for (int i = 1; i <= ConnectionPointsSwitchCaseVM.Count ; i++)
        {
            double heightLine = height / 2 + (height + indentBetweenSymbol) * i;

            base.ConnectionPointsSwitchCaseVM[i - 1].YCoordinate = heightLine;
            base.ConnectionPointsSwitchCaseVM[i - 1].YCoordinateLineDraw = heightLine;
        }
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