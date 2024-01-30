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
    public List<ConnectionPointVM> ConnectionPoints { get; init; }
    public BuilderScaleRectangles BuilderScaleRectangles { get; init; }

    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";

    private const int offsetPositionConnectionPoint = 10;
    public ActionSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        TextFieldSymbolVM = new(edblockVM.CanvasSymbolsVM, this);

        var topConnectionPoint = new ConnectionPointVM(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM,
            GetCoordinateTopConnectionPoint,
            GetTopBorderCoordinate,
            SideSymbol.Top);

        var leftConnectionPoint = new ConnectionPointVM(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM,
            GetCoordinateLeftConnectionPoint,
            GetLeftBorderCoordinate,
            SideSymbol.Left);

        var rightConnectionPoint = new ConnectionPointVM(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM,
            GetCoordinateRightConnectionPoint,
            GetRightBorderCoordinate,
            SideSymbol.Right);

        var bottomConnectionPoint = new ConnectionPointVM(
            CanvasSymbolsVM,
            this,
            _checkBoxLineGostVM,
            GetCoordinateBottomConnectionPoint,
            GetBottomBorderCoordinate,
            SideSymbol.Bottom);

        ConnectionPoints = new()
        {
            topConnectionPoint,
            leftConnectionPoint,
            rightConnectionPoint,
            bottomConnectionPoint
        };


        BuilderScaleRectangles = new(CanvasSymbolsVM, edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM, this);

        BlockSymbolModel = CreateBlockSymbolModel();

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

        Width = defaultWidth;
        Height = defaultHeigth;

        SetWidth(Width);
        SetHeight(Height);
    }

    public override void SetWidth(double width)
    {
        Width = width;

        var textFieldWidth = GetTextFieldWidth();
        var textFieldLeftOffset = GetTextFieldLeftOffset();

        TextFieldSymbolVM.Width = textFieldWidth;
        TextFieldSymbolVM.LeftOffset = textFieldLeftOffset;

        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(double height)
    {
        Height = height;

        var textFieldHeight = GetTextFieldHeight();
        var textFieldTopOffset = GetTextFieldTopOffset();

        TextFieldSymbolVM.Height = textFieldHeight;
        TextFieldSymbolVM.TopOffset = textFieldTopOffset;

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

    public double GetTextFieldWidth()
    {
        return Width;
    }

    public double GetTextFieldHeight()
    {
        return Height;
    }

    public static double GetTextFieldLeftOffset()
    {
        return 0;
    }

    public static double GetTextFieldTopOffset()
    {
        return 0;
    }

    public (double, double) GetCoordinateLeftConnectionPoint()
    {
        double pointsX = -offsetPositionConnectionPoint - 8;
        double pointsY = Height / 2;

        return (pointsX, pointsY);
    }

    public (double, double) GetCoordinateRightConnectionPoint()
    {
        double pointsX = Width + offsetPositionConnectionPoint;
        double pointsY = Height / 2;

        return (pointsX, pointsY);
    }

    public (double, double) GetCoordinateTopConnectionPoint()
    {
        double pointsX = Width / 2;
        double pointsY = -offsetPositionConnectionPoint;

        return (pointsX, pointsY);
    }

    public (double, double) GetCoordinateBottomConnectionPoint()
    {
        double pointsX = Width / 2;
        double pointsY = Height + offsetPositionConnectionPoint;

        return (pointsX, pointsY);
    }

    public (double x, double y) GetTopBorderCoordinate()
    {
        return (XCoordinate + Width / 2, YCoordinate);
    }

    public (double x, double y) GetBottomBorderCoordinate()
    {
        return (XCoordinate + Width / 2, YCoordinate + Height);
    }

    public (double x, double y) GetLeftBorderCoordinate()
    {
        return (XCoordinate, YCoordinate + Height / 2);
    }

    public (double x, double y) GetRightBorderCoordinate()
    {
        return (XCoordinate + Width, YCoordinate + Height / 2);
    }
}