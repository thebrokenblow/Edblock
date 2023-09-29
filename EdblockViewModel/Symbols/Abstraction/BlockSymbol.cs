using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using EdblockModel.Symbols;
using System.Collections.Generic;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class BlockSymbol : Symbol
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

    private Point coordinate;
    public Point Coordinate
    {
        get => coordinate;
        set
        {
            coordinate = value;
            OnPropertyChanged();
        }
    }

    public TextField TextField { get; init; }
    public BlockSymbolModel BlockSymbolModel { get; init; }
    public DelegateCommand EnterCursor { get; set; }
    public DelegateCommand LeaveCursor { get; set; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;
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

        var factoryScaleRectangles = new FactoryScaleRectangles(_canvasSymbolsVM, this);
        ScaleRectangles = factoryScaleRectangles.Create();

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