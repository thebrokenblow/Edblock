using System.Collections.Generic;
using EdblockModel.Enum;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace EdblockViewModel.Symbols;

public class ActionSymbolVM : BlockSymbolVM, IHasTextFieldVM, IHasConnectionPoint, IHasScaleRectangles
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public List<ConnectionPointVM> ConnectionPoints { get; init; } = new(countConnectionPoints);
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;
    private const int countConnectionPoints = 4;
    private const int offsetPositionConnectionPoint = 15;

    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";
    
    public ActionSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Width = defaultWidth;
        Height = defaultHeigth;

        TextFieldSymbolVM = new(edblockVM.CanvasSymbolsVM, this);

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

        var topConnectionPoint = new ConnectionPointVM(CanvasSymbolsVM, this, _checkBoxLineGostVM, SideSymbol.Top);
        var rightConnectionPoint = new ConnectionPointVM(CanvasSymbolsVM, this, _checkBoxLineGostVM, SideSymbol.Right);
        var bottomConnectionPoint = new ConnectionPointVM(CanvasSymbolsVM, this, _checkBoxLineGostVM, SideSymbol.Bottom);
        var leftConnectionPoint = new ConnectionPointVM(CanvasSymbolsVM, this, _checkBoxLineGostVM, SideSymbol.Left);

        ConnectionPoints.Add(topConnectionPoint);
        ConnectionPoints.Add(rightConnectionPoint);
        ConnectionPoints.Add(bottomConnectionPoint);
        ConnectionPoints.Add(leftConnectionPoint);

        SetWidth(Width);
        SetHeight(Height);
    }

    public override void SetWidth(double width)
    {
        Width = width;
        TextFieldSymbolVM.Width = width;

        SetCoordinateConnectionPoint();
        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(double height)
    {
        Height = height;
        TextFieldSymbolVM.Height = height;

        SetCoordinateConnectionPoint();
        ChangeCoordinateAuxiliaryElements();
    }

    private void SetCoordinateConnectionPoint()
    {
        SetCoordinateTopConnectionPoint();
        SetCoordinateRightConnectionPoint();
        SetCoordinateBottomConnectionPoint();
        SetCoordinateLeftConnectionPoint();
    }

    private void SetCoordinateTopConnectionPoint()
    {
        var xCoordinate = Width / 2;

        var topConnectionPoint = ConnectionPoints[0];

        topConnectionPoint.XCoordinate = xCoordinate;
        topConnectionPoint.YCoordinate = -offsetPositionConnectionPoint;

        topConnectionPoint.XCoordinateLineDraw = xCoordinate;
        topConnectionPoint.YCoordinateLineDraw = 0;
    }

    public void SetCoordinateRightConnectionPoint()
    {
        var yCoordinate = Height / 2;

        var bottomConnectionPoint = ConnectionPoints[1];

        bottomConnectionPoint.XCoordinate = Width + offsetPositionConnectionPoint;
        bottomConnectionPoint.YCoordinate = yCoordinate;

        bottomConnectionPoint.XCoordinateLineDraw = Width;
        bottomConnectionPoint.YCoordinateLineDraw = yCoordinate;
    }

    public void SetCoordinateBottomConnectionPoint()
    {
        var xCoordinate = Width / 2;

        var bottomConnectionPoint = ConnectionPoints[2];

        bottomConnectionPoint.XCoordinate = xCoordinate;
        bottomConnectionPoint.YCoordinate = Height + offsetPositionConnectionPoint;

        bottomConnectionPoint.XCoordinateLineDraw = xCoordinate;
        bottomConnectionPoint.YCoordinateLineDraw = Height;
    }

    public void SetCoordinateLeftConnectionPoint()
    {
        var yCoordinate = Height / 2;

        var leftConnectionPoint = ConnectionPoints[3];

        leftConnectionPoint.XCoordinate = -offsetPositionConnectionPoint;
        leftConnectionPoint.YCoordinate = yCoordinate;

        leftConnectionPoint.XCoordinateLineDraw = 0;
        leftConnectionPoint.YCoordinateLineDraw = yCoordinate;
    }
}