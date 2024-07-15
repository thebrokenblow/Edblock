using System;
using System.Windows.Input;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Core;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using Prism.Commands;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Symbols.Abstractions;

public abstract class BlockSymbolVM : ObservableObject
{
    public string Id { get; } = Guid.NewGuid().ToString();

    protected string? color = string.Empty;
    public virtual string? Color
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

    protected double xCoordinate;
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

    protected double yCoordinate;
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
    protected readonly ILineStateStandardComponentVM _lineStateStandardComponentVM;
    protected readonly ICanvasSymbolsComponentVM _canvasSymbolsComponentVM;
    protected readonly ITopSettingsMenuComponentVM _topSettingsMenuComponentVM;

    private readonly IListCanvasSymbolsComponentVM _listCanvasSymbolsComponentVM;
    private readonly ScalableBlockSymbolVM? _scalableBlockSymbolVM;

    private readonly IHasConnectionPoint? _symbolHasConnectionPoint;
    private readonly IHasTextFieldVM? _symbolHasTextField;

    public BlockSymbolVM(
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM, 
        IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM, 
        IPopupBoxMenuComponentVM popupBoxMenuComponentVM)
    {
        _lineStateStandardComponentVM = popupBoxMenuComponentVM.LineStateStandardComponentVM;
        scaleAllSymbolComponentVM = popupBoxMenuComponentVM.ScaleAllSymbolComponentVM;

        _canvasSymbolsComponentVM = canvasSymbolsComponentVM;
        _topSettingsMenuComponentVM = topSettingsMenuComponentVM;
        _listCanvasSymbolsComponentVM = listCanvasSymbolsComponentVM;

        if (this is ScalableBlockSymbolVM scalableBlockSymbolVM)
        {
            _scalableBlockSymbolVM = scalableBlockSymbolVM;
        }

        if (this is IHasConnectionPoint symbolHasConnectionPoint)
        {
            _symbolHasConnectionPoint = symbolHasConnectionPoint;
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
        if (_scalableBlockSymbolVM is null)
        {
            return;
        }

        ScaleRectangle.SetStateDisplay(_scalableBlockSymbolVM.ScaleRectangles, status);
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

        _listCanvasSymbolsComponentVM.SelectedBlockSymbolsVM.Add(this);

        if (_symbolHasTextField is null)
        {
            return;
        }

        if (this is IObserverColor observerColor)
        {
            _topSettingsMenuComponentVM.ColorSubject.RegisterObserver(observerColor);
        }

        if (this is IObserverFontFamily observerFontFamily)
        {
            _topSettingsMenuComponentVM.FontFamilySubject.RegisterObserver(observerFontFamily);
            observerFontFamily.UpdateFontFamily();
        }

        if (this is IObserverFontSize observerFontSize)
        {
            _topSettingsMenuComponentVM.FontSizeSubject.RegisterObserver(observerFontSize);
            observerFontSize.UpdateFontSize();
        }

        if (this is IObserverFormatText observerFormatText)
        {
            _topSettingsMenuComponentVM.FormatTextSubject.RegisterObserver(observerFormatText);
            observerFormatText.UpdateFormatText();
        }

        if (this is IObserverTextAlignment observerTextAlignment)
        {
            _topSettingsMenuComponentVM.TextAlignmentSubject.RegisterObserver(observerTextAlignment);
            observerTextAlignment.UpdateTextAlignment();
        }

        _listCanvasSymbolsComponentVM.SelectedSymbolsHasTextField.Add(_symbolHasTextField);
    }
}