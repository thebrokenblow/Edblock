using System;
using Prism.Commands;
using System.Windows.Input;
using EdblockModel.Symbols;
using EdblockModel.Symbols.Enum;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class BlockSymbolVM : SymbolVM
{
    public List<ConnectionPoint> ConnectionPoints { get; init; }
    public List<ScaleRectangle> ScaleRectangles { get; init; }
    public string Id { get; set; }

    private string? color;
    public override string? Color
    {
        get => color;
        set
        {
            color = value;
            BlockSymbolModel.Color = color;
        } 
    }

    private int width;
    public int Width
    {
        get => width;
        set
        {
            width = value;
            SetWidth(width);
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
            SetHeight(Height);
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
            BlockSymbolModel.XCoordinate = xCoordinate;

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
            BlockSymbolModel.YCoordinate = yCoordinate;

            OnPropertyChanged();
        }
    }

    public DelegateCommand MouseEnter { get; set; }
    public DelegateCommand MouseLeave { get; set; }

    public TextFieldVM TextField { get; init; }
    public BlockSymbolModel BlockSymbolModel { get; init; }

    private bool isSelected;
    public bool IsSelected 
    {
        get => isSelected;
        set
        {
            isSelected = value;
            OnPropertyChanged();
        }
    }

    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FontFamilyControlVM _fontFamilyControlVM;
    private readonly FontSizeControlVM _fontSizeControlVM;

    public BlockSymbolVM(
        CanvasSymbolsVM canvasSymbolsVM, 
        ScaleAllSymbolVM scaleAllSymbolVM, 
        CheckBoxLineGostVM checkBoxLineGostVM,
        FontFamilyControlVM fontFamilyControlVM,
        FontSizeControlVM fontSizeControlVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _fontSizeControlVM = fontSizeControlVM;
        _fontFamilyControlVM = fontFamilyControlVM;

        Id = Guid.NewGuid().ToString();

        var nameBlockSymbol = GetType().Name?.ToString();

        BlockSymbolModel = FactoryBlockSymbolModel.Create(Id, nameBlockSymbol, color);

        TextField = new(canvasSymbolsVM, this);

        var factoryConnectionPoints = new FactoryConnectionPoints(_canvasSymbolsVM, checkBoxLineGostVM, this);
        ConnectionPoints = factoryConnectionPoints.CreateConnectionPoints();

        var factoryScaleRectangles = new FactoryScaleRectangles(_canvasSymbolsVM, scaleAllSymbolVM,  this);
        ScaleRectangles = factoryScaleRectangles.Create();

        Width = defaultWidth;
        Height = defaultHeigth;

        MouseEnter = new(ShowStroke);
        MouseLeave = new(HideStroke);
    }

    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);

    public void SetCoordinate((int x, int y) currentCoordinate, (int x, int y) previousCoordinate)
    {
        if (XCoordinate == 0 && YCoordinate == 0)
        {
            XCoordinate = currentCoordinate.x - Width / 2;
            YCoordinate = currentCoordinate.y - Height / 2;
        }
        else
        {
            XCoordinate = currentCoordinate.x - (previousCoordinate.x - XCoordinate);
            YCoordinate = currentCoordinate.y - (previousCoordinate.y - YCoordinate);
        }
    }

    public void ShowStroke()
    {
        // Условие истино, когда символ не перемещается и не масштабируется (просто навёл курсор)

        var movableSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var scalePartBlockSymbolVM = _canvasSymbolsVM.ScalePartBlockSymbol;

        if (movableSymbol == null && scalePartBlockSymbolVM == null)
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

    internal ConnectionPoint GetConnectionPoint(PositionConnectionPoint outgoingPosition)
    {
        foreach (var connectionPoint in ConnectionPoints)
        {
            if (connectionPoint.Position == outgoingPosition)
            {
                return connectionPoint;
            }
        }

        throw new Exception("Точки соединения с такой позицией нет");
    }

    public void Select()
    {
        IsSelected = true;

        _fontSizeControlVM.SetFontSize(this);
        _fontFamilyControlVM.SetFontFamily(this);

        _canvasSymbolsVM.SelectedBlockSymbols.Add(this);
    }
}