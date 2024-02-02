using System.Collections.Generic;
using EdblockModel.Enum;
using EdblockModel.SymbolsModel;
using EdblockModel.AbstractionsModel;
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
        BlockSymbolModel = CreateBlockSymbolModel();

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

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameSymbol = GetType().BaseType?.ToString();

        var actionSymbolModel = new ActionSymbolModel
        {
            Id = Id,
            NameSymbol = nameSymbol,
            Color = Color
        };

        return actionSymbolModel;
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
        var topConnectionPoint = ConnectionPoints[0];

        topConnectionPoint.XCoordinate = Width / 2;
        topConnectionPoint.YCoordinate = -offsetPositionConnectionPoint;

        topConnectionPoint.XCoordinateLineDraw = Width / 2;
        topConnectionPoint.YCoordinateLineDraw = 0;
    }

    public void SetCoordinateRightConnectionPoint()
    {
        var bottomConnectionPoint = ConnectionPoints[1];

        bottomConnectionPoint.XCoordinate = Width + offsetPositionConnectionPoint;
        bottomConnectionPoint.YCoordinate = Height / 2;

        bottomConnectionPoint.XCoordinateLineDraw = Width;
        bottomConnectionPoint.YCoordinateLineDraw = Height / 2;
    }

    public void SetCoordinateBottomConnectionPoint()
    {
        var bottomConnectionPoint = ConnectionPoints[2];

        bottomConnectionPoint.XCoordinate = Width / 2;
        bottomConnectionPoint.YCoordinate = Height + offsetPositionConnectionPoint;

        bottomConnectionPoint.XCoordinateLineDraw = Width / 2;
        bottomConnectionPoint.YCoordinateLineDraw = Height;
    }

    public void SetCoordinateLeftConnectionPoint()
    {
        var leftConnectionPoint = ConnectionPoints[3];

        leftConnectionPoint.XCoordinate = -offsetPositionConnectionPoint;
        leftConnectionPoint.YCoordinate = Height / 2;

        leftConnectionPoint.XCoordinateLineDraw = 0;
        leftConnectionPoint.YCoordinateLineDraw = Height / 2;
    }
}