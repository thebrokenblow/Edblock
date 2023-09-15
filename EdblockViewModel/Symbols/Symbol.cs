using EdblockModel;
using Prism.Commands;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EdblockViewModel.ConnectionPointViewModel;

namespace EdblockViewModel.Symbols;

public abstract class Symbol : INotifyPropertyChanged
{
    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;
    public List<ConnectionPoint> ConnectionPoints { get; init; }

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
    public SymolModel SymolModel { get; init; }
    public DelegateCommand EnterCursor { get; set; }
    public DelegateCommand LeaveCursor { get; set; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public Symbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;

        TextField = new();
        
        SymolModel = new()
        {
            Width = defaultWidth,
            Height = defaultHeigth
        };

        Width = defaultWidth;
        Height = defaultHeigth;

        ConnectionPoints = new()
        {
            new ConnectionPoint(this, GetCoordinateTopCP),
            new ConnectionPoint(this, GetCoordinateRightCP),
            new ConnectionPoint(this, GetCoordinateBottomCP),
            new ConnectionPoint(this, GetCoordinateLeftCP)
        };

        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
    }

    public void ShowStroke()
    {
        if (_canvasSymbolsVM.DraggableSymbol == null)
        {
            ColorConnectionPoint.Show(ConnectionPoints);
        }
    }

    public void HideStroke()
    {
        ColorConnectionPoint.Hide(ConnectionPoints);
    }

    protected virtual Point GetCoordinateLeftCP(double diametr, int offset)
    {
        double connectionPointsX = -offset - diametr;
        double connectionPointsY = Height / 2 - diametr / 2;
        var coordinateLeftConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateLeftConnectionPoint;
    }

    protected virtual Point GetCoordinateRightCP(double diametr, int offset)
    {
        double connectionPointsX = Width + offset;
        double connectionPointsY = Height / 2 - diametr / 2;
        var coordinateRightConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateRightConnectionPoint;
    }

    protected virtual Point GetCoordinateTopCP(double diametr, int offset)
    {
        double connectionPointsX = Width / 2 - diametr / 2;
        double connectionPointsY = -offset - diametr;
        var coordinateTopConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateTopConnectionPoint;
    }

    protected virtual Point GetCoordinateBottomCP(double diametr, int offset)
    {
        double connectionPointsX = Width / 2 - diametr / 2;
        double connectionPointsY = Height + offset;
        var coordinateTopConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateTopConnectionPoint;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}