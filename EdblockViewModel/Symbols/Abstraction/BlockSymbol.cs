using Prism.Commands;
using System.Windows.Input;
using EdblockModel.Symbols;
using System.Collections.Generic;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class BlockSymbol : Symbol
{
    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;
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
    public BlockSymbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;

        TextField = new(canvasSymbolsVM)
        {
            Width = defaultWidth,
            Height = defaultHeigth
        };

        BlockSymbolModel = new ActionSymbolModel()
        {
            Width = defaultWidth,
            Height = defaultHeigth
        };


        Width = defaultWidth;
        Height = defaultHeigth;

        var factoryConnectionPoints = new FactoryConnectionPoints(_canvasSymbolsVM, this);
        ConnectionPoints = factoryConnectionPoints.Create();

        ScaleRectangles = new();
        AddScaleRectangle();

        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
    }

    public void ShowStroke()
    {
        if (_canvasSymbolsVM.DraggableSymbol == null && _canvasSymbolsVM.ScaleData == null)
        {
            ColorConnectionPoint.Show(ConnectionPoints);
            ColorScaleRectangle.Show(ScaleRectangles);
            TextField.Cursor = Cursors.SizeAll;
        }
    }

    public void HideStroke()
    {
        ColorConnectionPoint.Hide(ConnectionPoints);
        ColorScaleRectangle.Hide(ScaleRectangles);
    }

    protected virtual void AddScaleRectangle() //Factory отдельный класс
    {
        var coordinateScaleRectangle = new CoordinateScaleRectangle(this);

        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeNS, null, SizesScaleRectangle.ChangeHeigthTop, coordinateScaleRectangle.GetCoordinateMiddleTopRectangle));
        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeNESW, SizesScaleRectangle.ChangeWidthRigth, SizesScaleRectangle.ChangeHeigthTop, coordinateScaleRectangle.GetCoordinateRightTopRectangle));
        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeWE, SizesScaleRectangle.ChangeWidthRigth, null, coordinateScaleRectangle.GetCoordinateRightMiddleRectangle));
        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeNWSE, SizesScaleRectangle.ChangeWidthRigth, SizesScaleRectangle.ChangeHeigthBottom, coordinateScaleRectangle.GetCoordinateRightBottomRectangle));
        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeNS, null, SizesScaleRectangle.ChangeHeigthBottom, coordinateScaleRectangle.GetCoordinateMiddleBottomRectangle));
        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeNESW, SizesScaleRectangle.ChangeWidthLeft, SizesScaleRectangle.ChangeHeigthBottom, coordinateScaleRectangle.GetCoordinateLeftBottomRectangle));
        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeWE, SizesScaleRectangle.ChangeWidthLeft, null, coordinateScaleRectangle.GetCoordinateLeftMiddleRectangle));
        ScaleRectangles.Add(new(_canvasSymbolsVM, this, Cursors.SizeNWSE, SizesScaleRectangle.ChangeWidthLeft, SizesScaleRectangle.ChangeHeigthTop, coordinateScaleRectangle.GetCoordinateLeftTopRectangle));
    }

    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);

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