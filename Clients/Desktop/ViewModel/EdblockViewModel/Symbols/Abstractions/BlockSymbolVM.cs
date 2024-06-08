using System;
using System.Windows.Input;
using Prism.Commands;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Core;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.Abstractions;

public abstract class BlockSymbolVM : ObservableObject
{
    public string Id { get; set; } = string.Empty;

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

    private double width;
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

    private double heigth;
    public double Height
    {
        get => heigth;
        set
        {
            heigth = value;
            BlockSymbolModel.Height = heigth;

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

    public DelegateCommand MouseEnter { get; set; }
    public DelegateCommand MouseLeave { get; set; }
    public DelegateCommand MouseLeftButtonDown { get; set; }
    public BlockSymbolModel BlockSymbolModel { get; } = null!;
    public ICanvasSymbolsComponentVM CanvasSymbolsComponentVM { get; }

    protected readonly IScaleAllSymbolComponentVM scaleAllSymbolComponentVM;
    protected readonly ILineStateStandardComponentVM lineStateStandardComponentVM;

    private readonly IListCanvasSymbolsComponentVM _listCanvasSymbolsComponentVM;
    private readonly ITopSettingsMenuComponentVM _topSettingsMenuComponentVM;

    public BlockSymbolVM(
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM, 
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM, 
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM)
    {
        CanvasSymbolsComponentVM = canvasSymbolsComponentVM;

        lineStateStandardComponentVM = popupBoxMenuComponentVM.LineStateStandardComponentVM;
        scaleAllSymbolComponentVM = popupBoxMenuComponentVM.ScaleAllSymbolComponentVM;

        _topSettingsMenuComponentVM = topSettingsMenuComponentVM;
        _listCanvasSymbolsComponentVM = listCanvasSymbolsComponentVM;

        Id = Guid.NewGuid().ToString();

        BlockSymbolModel = CreateBlockSymbolModel();

        MouseEnter = new(ShowAuxiliaryElements);
        MouseLeave = new(HideAuxiliaryElements);
        MouseLeftButtonDown = new(SetMovableSymbol);
    }

    public abstract void SetWidth(double width);
    public abstract void SetHeight(double height);

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
        CanvasSymbolsComponentVM.Cursor = Cursors.SizeAll;

        var movableSymbol = _listCanvasSymbolsComponentVM.MovableBlockSymbol;
        var scalePartBlockSymbolVM = CanvasSymbolsComponentVM.ScalePartBlockSymbol;

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

        CanvasSymbolsComponentVM.Cursor = Cursors.Arrow;
    }

    private void SetDisplayConnectionPoints(bool status)
    {
        if (this is IHasConnectionPoint symbolHasConnectionPoint)
        {
            var connectionPoints = symbolHasConnectionPoint.ConnectionPointsVM;

            ConnectionPointVM.SetDisplayConnectionPoints(connectionPoints, status);
        }
    }

    private void SetDisplayScaleRectangles(bool status)
    {
        if (this is IHasScaleRectangles symbolHasScaleRectangles)
        {
            var scaleRectangles = symbolHasScaleRectangles.ScaleRectangles;

            ScaleRectangle.SetStateDisplay(scaleRectangles, status);
        }
    }

    protected void ChangeCoordinateScaleRectangle()
    {
        if (this is IHasScaleRectangles symbolHasScaleRectangles)
        {
            var scaleRectangles = symbolHasScaleRectangles.ScaleRectangles;

            foreach (var scaleRectangle in scaleRectangles)
            {
                scaleRectangle.ChangeCoordination();
            }
        }
    }

    public void SetMovableSymbol()
    {
        SetDisplayScaleRectangles(false);
        SetDisplayConnectionPoints(false);

        CanvasSymbolsComponentVM.Cursor = Cursors.SizeAll;

        _listCanvasSymbolsComponentVM.MovableBlockSymbol = this;
    }

    public void SetSelectedProperties()
    {
        IsSelected = true;

        if (this is IHasTextFieldVM selectedSymbolHasTextField)
        {
            _listCanvasSymbolsComponentVM.SelectedSymbolsHasTextField.Add(selectedSymbolHasTextField);

            _topSettingsMenuComponentVM.FontSizeComponentVM.SetFontSize(selectedSymbolHasTextField);
            _topSettingsMenuComponentVM.FontFamilyComponentVM.SetFontFamily(selectedSymbolHasTextField);
            _topSettingsMenuComponentVM.FormatTextComponentVM.SetFontText(selectedSymbolHasTextField);
            _topSettingsMenuComponentVM.TextAlignmentComponentVM.SetFormatAlignment(selectedSymbolHasTextField);
        }

        _listCanvasSymbolsComponentVM.SelectedBlockSymbols.Add(this);
    }
}