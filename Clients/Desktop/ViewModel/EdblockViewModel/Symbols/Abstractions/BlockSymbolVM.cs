using System;
using System.Windows.Input;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Core;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using Prism.Commands;

namespace EdblockViewModel.Symbols.Abstractions;

public abstract class BlockSymbolVM : ObservableObject
{
    public string Id { get; } = Guid.NewGuid().ToString();

    protected string color = string.Empty;
    public virtual string Color
    {
        get => color;
        set
        {
            color = value;
            BlockSymbolModel.Color = color;

            OnPropertyChanged();
        }
    }

    protected double width;
    public double Width
    {
        get => width;
        set
        {
            width = value;
            BlockSymbolModel.Width = width;

            OnPropertyChanged();
        }
    }

    protected double height;
    public double Height
    {
        get => height;
        set
        {
            height = value;
            BlockSymbolModel.Height = height;

            OnPropertyChanged();
        }
    }

    private double xCoordinate;
    public double XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            BlockSymbolModel.XCoordinate = xCoordinate;

            OnPropertyChanged();
        }
    }

    private double yCoordinate;
    public double YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
            BlockSymbolModel.YCoordinate = yCoordinate;

            OnPropertyChanged();
        }
    }

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

    public bool MoveMiddle { get; set; }

    public DelegateCommand MouseEnter { get; }
    public DelegateCommand MouseLeave { get; }
    public DelegateCommand MouseLeftButtonDown { get; }

    public BlockSymbolModel BlockSymbolModel { get; }

    protected readonly IScaleAllSymbolComponentVM scaleAllSymbolComponentVM;
    protected readonly ILineStateStandardComponentVM lineStateStandardComponentVM;
    protected readonly ICanvasSymbolsComponentVM _canvasSymbolsComponentVM;

    private readonly IListCanvasSymbolsComponentVM _listCanvasSymbolsComponentVM;
    private readonly ITopSettingsMenuComponentVM _topSettingsMenuComponentVM;

    private readonly IHasConnectionPoint? _symbolHasConnectionPoint;
    private readonly IHasScaleRectangles? _symbolHasScaleRectangles;
    private readonly IHasTextFieldVM? _symbolHasTextField;

    public BlockSymbolVM(
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM, 
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM, 
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM)
    {
        lineStateStandardComponentVM = popupBoxMenuComponentVM.LineStateStandardComponentVM;
        scaleAllSymbolComponentVM = popupBoxMenuComponentVM.ScaleAllSymbolComponentVM;

        _canvasSymbolsComponentVM = canvasSymbolsComponentVM;
        _topSettingsMenuComponentVM = topSettingsMenuComponentVM;
        _listCanvasSymbolsComponentVM = listCanvasSymbolsComponentVM;

        if (this is IHasConnectionPoint symbolHasConnectionPoint)
        {
            _symbolHasConnectionPoint = symbolHasConnectionPoint;
        }

        if (this is IHasScaleRectangles symbolHasScaleRectangles)
        {
            _symbolHasScaleRectangles = symbolHasScaleRectangles;
        }

        if (this is IHasTextFieldVM symbolHasTextField)
        {
            _symbolHasTextField = symbolHasTextField;
        }

        BlockSymbolModel = CreateBlockSymbolModel();

        MouseEnter = new(ShowAuxiliaryElements);
        MouseLeave = new(HideAuxiliaryElements);
        MouseLeftButtonDown = new(SetMovableSymbol);
    }

    public abstract void SetWidth(double width);
    public abstract void SetHeight(double height);

    public virtual void SetSize(ScalePartBlockSymbol scalePartBlockSymbol)
    {
        if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightMiddle)
        {
            double widthBlockSymbol = _canvasSymbolsComponentVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

            if (widthBlockSymbol < 40)
            {
                widthBlockSymbol = 40;
            }

            SetWidth(widthBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightBottom)
        {
            double widthBlockSymbol = _canvasSymbolsComponentVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;
            double heigthBlockSymbol = _canvasSymbolsComponentVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            if (widthBlockSymbol < 40)
            {
                widthBlockSymbol = 40;
            }

            SetWidth(widthBlockSymbol);

            if (heigthBlockSymbol < 40)
            {
                heigthBlockSymbol = 40;
            }

            SetHeight(heigthBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.MiddleBottom)
        {
            double heigthBlockSymbol = _canvasSymbolsComponentVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            if (heigthBlockSymbol < 40)
            {
                heigthBlockSymbol = 40;
            }

            SetHeight(heigthBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftBottom)
        {
            int currentXCoordinateCursor = _canvasSymbolsComponentVM.XCoordinate;
            double initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
            double initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

            double widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

            if (widthBlockSymbol < 40)
            {
                widthBlockSymbol = 40;
            }

            XCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);

            SetWidth(widthBlockSymbol);

            double heigthBlockSymbol = _canvasSymbolsComponentVM.YCoordinate - scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            if (heigthBlockSymbol < 40)
            {
                heigthBlockSymbol = 40;
            }

            SetHeight(heigthBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftMiddle)
        {
            int currentXCoordinateCursor = _canvasSymbolsComponentVM.XCoordinate;
            double initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
            double initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

            double widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

            if (widthBlockSymbol < 40)
            {
                widthBlockSymbol = 40;
            }

            XCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);

            SetWidth(widthBlockSymbol);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.LeftTop)
        {
            int currentXCoordinateCursor = _canvasSymbolsComponentVM.XCoordinate;
            double initialWidth = scalePartBlockSymbol.InitialWidthBlockSymbol;
            double initialXCoordinate = scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

            double widthBlockSymbol = initialWidth + (initialXCoordinate - currentXCoordinateCursor);

            if (widthBlockSymbol < 40)
            {
                widthBlockSymbol = 40;
            }

            XCoordinate = initialXCoordinate - (widthBlockSymbol - initialWidth);

            SetWidth(widthBlockSymbol);

            int currentYCoordinateCursor = _canvasSymbolsComponentVM.YCoordinate;
            double initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
            double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            double heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

            if (heigthBlockSymbol < 40)
            {
                heigthBlockSymbol = 40;
            }

            SetHeight(heigthBlockSymbol);

            YCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.MiddleTop)
        {

            int currentYCoordinateCursor = _canvasSymbolsComponentVM.YCoordinate;
            double initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
            double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            double heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

            if (heigthBlockSymbol < 40)
            {
                heigthBlockSymbol = 40;
            }

            SetHeight(heigthBlockSymbol);

            YCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);
        }
        else if (scalePartBlockSymbol.PositionScaleRectangle == PositionScaleRectangle.RightTop)
        {
            double widthBlockSymbol = _canvasSymbolsComponentVM.XCoordinate - scalePartBlockSymbol.InitialXCoordinateBlockSymbol;

            if (widthBlockSymbol < 40)
            {
                widthBlockSymbol = 40;
            }

            SetWidth(widthBlockSymbol);

            int currentYCoordinateCursor = _canvasSymbolsComponentVM.YCoordinate;
            double initialHeigth = scalePartBlockSymbol.InitialHeigthBlockSymbol;
            double initialYCoordinate = scalePartBlockSymbol.InitialYCoordinateBlockSymbol;

            double heigthBlockSymbol = initialHeigth + (initialYCoordinate - currentYCoordinateCursor);

            if (heigthBlockSymbol < 40)
            {
                heigthBlockSymbol = 40;
            }

            SetHeight(heigthBlockSymbol);

            YCoordinate = initialYCoordinate - (heigthBlockSymbol - initialHeigth);
        }
    }

    public virtual BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameSymbol = GetType().Name.ToString();

        var blockSymbolModel = new BlockSymbolModel()
        {
            Id = Id,
            NameSymbol = nameSymbol,
        };

        return blockSymbolModel;
    }

    public void SetCoordinate((int x, int y) currentCoordinate, (int x, int y) previousCoordinate)
    {
        if (MoveMiddle)
        {
            XCoordinate = currentCoordinate.x - Width / 2;
            YCoordinate = currentCoordinate.y - Height / 2;
        }
        else
        {
            if (previousCoordinate.x != 0 && previousCoordinate.y != 0)
            {
                XCoordinate = currentCoordinate.x - (previousCoordinate.x - XCoordinate);
                YCoordinate = currentCoordinate.y - (previousCoordinate.y - YCoordinate);
            }
        }
    }

    public void ShowAuxiliaryElements()
    {
        _canvasSymbolsComponentVM.Cursor = Cursors.SizeAll;

        var movableSymbol = _listCanvasSymbolsComponentVM.MovableBlockSymbol;
        var scalePartBlockSymbolVM = _canvasSymbolsComponentVM.ScalePartBlockSymbol;

        if (movableSymbol is not null || scalePartBlockSymbolVM is not null)
        {
            return;
        }

        SetDisplayScaleRectangles(true);
        SetDisplayConnectionPoints(true);

        if (this is IHasTextFieldVM symbolHasTextField)
        {
            symbolHasTextField.TextFieldSymbolVM.Cursor = Cursors.SizeAll;
        }
    }

    public void HideAuxiliaryElements()
    {
        SetDisplayScaleRectangles(false);
        SetDisplayConnectionPoints(false);

        _canvasSymbolsComponentVM.Cursor = Cursors.Arrow;
    }

    private void SetDisplayConnectionPoints(bool status)
    {
        if (_symbolHasConnectionPoint is null)
        {
            return;
        }

        ConnectionPointVM.SetDisplayConnectionPoints(_symbolHasConnectionPoint.ConnectionPointsVM, status);
    }

    private void SetDisplayScaleRectangles(bool status)
    {
        if (_symbolHasScaleRectangles is null)
        {
            return;
        }

        ScaleRectangle.SetStateDisplay(_symbolHasScaleRectangles.ScaleRectangles, status);
    }

    protected void ChangeCoordinateScaleRectangle()
    {
        if (_symbolHasScaleRectangles is null)
        {
            return;
        }

        foreach (var scaleRectangle in _symbolHasScaleRectangles.ScaleRectangles)
        {
            scaleRectangle.ChangeCoordination();
        }
    }

    public void SetMovableSymbol()
    {
        SetDisplayScaleRectangles(false);
        SetDisplayConnectionPoints(false);

        _canvasSymbolsComponentVM.Cursor = Cursors.SizeAll;

        _listCanvasSymbolsComponentVM.MovableBlockSymbol = this;
    }

    public void SetSelectedProperties()
    {
        IsSelected = true;

        _listCanvasSymbolsComponentVM.SelectedBlockSymbols.Add(this);

        if (_symbolHasTextField is null)
        {
            return;
        }

        _topSettingsMenuComponentVM.FontSizeComponentVM.SetFontSize(_symbolHasTextField);
        _topSettingsMenuComponentVM.FontFamilyComponentVM.SetFontFamily(_symbolHasTextField);
        _topSettingsMenuComponentVM.FormatTextComponentVM.SetFontText(_symbolHasTextField);
        _topSettingsMenuComponentVM.TextAlignmentComponentVM.SetFormatAlignment(_symbolHasTextField);
        _topSettingsMenuComponentVM.FormatVerticalAlignComponentVM.SetFormatVerticalAlignment(_symbolHasTextField);

        _listCanvasSymbolsComponentVM.SelectedSymbolsHasTextField.Add(_symbolHasTextField);
    }
}