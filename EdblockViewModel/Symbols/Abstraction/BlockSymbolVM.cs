using System;
using Prism.Commands;
using System.Windows.Input;
using EdblockModel.Symbols;
using System.Collections.Generic;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class BlockSymbolVM : SymbolVM
{
    public List<ConnectionPoint> ConnectionPoints { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }

    private int width;
    public int Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }

    private int heigth;
    public int Height
    {
        get => heigth;
        set
        {
            heigth = value;
            OnPropertyChanged();
        }
    }

    private int xCoordinate; 
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            OnPropertyChanged();
        }
    }

    private int yCoordinate;
    public int YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
            OnPropertyChanged();
        }
    }

    public TextField TextField { get; init; }
    public BlockSymbolModel BlockSymbolModel { get; init; }
    public DelegateCommand EnterCursor { get; set; }
    public DelegateCommand LeaveCursor { get; set; }

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FactoryBlockSymbolModel factoryBlockSymbolModel = new();

    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;
    public BlockSymbolVM(string nameBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;

        TextField = new(canvasSymbolsVM);

        BlockSymbolModel = factoryBlockSymbolModel.Create(nameBlockSymbol);
        BlockSymbolModel.Width = defaultWidth;
        BlockSymbolModel.Height = defaultHeigth;

        Width = defaultWidth;
        Height = defaultHeigth;
        Color = BlockSymbolModel.HexColor;

        var factoryConnectionPoints = new FactoryConnectionPoints(_canvasSymbolsVM, this);
        ConnectionPoints = factoryConnectionPoints.Create();

        var factoryScaleRectangles = new FactoryScaleRectangles(_canvasSymbolsVM, this);
        ScaleRectangles = factoryScaleRectangles.Create();

        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
    }

    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);

    public void SetCoordinate((int x, int y) currentCoordinate, (int x, int y) previousCoordinate)
    {
        int roundedXCoordinate = CanvasSymbolsVM.RoundCoordinate(currentCoordinate.x);
        int roundedYCoordinate = CanvasSymbolsVM.RoundCoordinate(currentCoordinate.y);

        if (XCoordinate == 0 && YCoordinate == 0)
        {
            XCoordinate = roundedXCoordinate - Width / 2;
            YCoordinate = roundedYCoordinate - Height / 2;
        }
        else
        {
            XCoordinate = roundedXCoordinate - (previousCoordinate.x - XCoordinate);
            YCoordinate = roundedYCoordinate - (previousCoordinate.y - YCoordinate);
        }

        BlockSymbolModel.XCoordinate = XCoordinate;
        BlockSymbolModel.YCoordinate = YCoordinate;
    }

    public void ShowStroke()
    {
        // Условие истино, когда символ не перемещается и не масштабируется (просто навёл курсор)
        if (_canvasSymbolsVM.MovableSymbol == null && _canvasSymbolsVM.ScalePartBlockSymbolVM == null)
        {
            ConnectionPoint.SetDisplayConnectionPoints(ConnectionPoints, true);
            ScaleRectangle.SetStateDisplay(ScaleRectangles, true);
            TextField.Cursor = Cursors.SizeAll;
        }
    }

    public void HideStroke()
    {
        ConnectionPoint.SetDisplayConnectionPoints(ConnectionPoints, false);
        ScaleRectangle.SetStateDisplay(ScaleRectangles, false);
    }

    protected void ChangeCoordinateAuxiliaryElements()
    {
        foreach (var connectionPoint in ConnectionPoints)
        {
            connectionPoint.ChangeCoordination();
        }

        foreach (var scaleRectangle in ScaleRectangles)
        {
            scaleRectangle.ChangeCoordination();
        }
    }
}