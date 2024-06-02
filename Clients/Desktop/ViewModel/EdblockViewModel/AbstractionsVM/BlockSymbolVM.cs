using System;
using System.Windows.Input;
using Prism.Commands;
using EdblockModel.SymbolsModel;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.PagesVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols;
using EdblockViewModel.CoreVM;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;
using EdblockViewModel.ComponentsVM.CanvasSymbols.Interfaces;

namespace EdblockViewModel.AbstractionsVM;

public abstract class BlockSymbolVM : ObservableObject
{
    public string Id { get; set; } = string.Empty;

    private string color = string.Empty;
    public string Color
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

    public EditorVM EdblockVM { get; init; }
    public DelegateCommand MouseEnter { get; set; }
    public DelegateCommand MouseLeave { get; set; }
    public DelegateCommand MouseLeftButtonDown { get; set; }
    public BlockSymbolModel BlockSymbolModel { get; init; } = null!;
    public CanvasSymbolsVM CanvasSymbolsVM { get; init; }

    protected readonly LineStateStandardVM _checkBoxLineGostVM;
    protected readonly ScaleAllSymbolVM _scaleAllSymbolVM;

    private readonly IFontFamilyComponentVM _fontFamilyControlVM;
    private readonly IFontSizeComponentVM _fontSizeControlVM;
    private readonly ITextAlignmentComponentVM _textAlignmentControlVM;
    private readonly IFormatTextComponentVM _formatTextControlVM;
    private readonly IListCanvasSymbolsVM _listCanvasSymbolsVM;
    public BlockSymbolVM(EditorVM edblockVM)
    {
        EdblockVM = edblockVM;

        CanvasSymbolsVM = edblockVM.CanvasSymbolsVM;

        _fontSizeControlVM = edblockVM.FontSizeControlVM;
        _fontFamilyControlVM = edblockVM.FontFamilyControlVM;
        _formatTextControlVM = edblockVM.FormatTextControlVM;
        _textAlignmentControlVM = edblockVM.TextAlignmentControlVM;
        _scaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM;
        _checkBoxLineGostVM = edblockVM.PopupBoxMenuVM.CheckBoxLineGostVM;

        Id = Guid.NewGuid().ToString();

        BlockSymbolModel = CreateBlockSymbolModel();

        MouseEnter = new(ShowAuxiliaryElements);
        MouseLeave = new(HideAuxiliaryElements);
        MouseLeftButtonDown = new(SetMovableSymbol);



        _listCanvasSymbolsVM = edblockVM.CanvasSymbolsVM.ListCanvasSymbolsVM;
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
        CanvasSymbolsVM.Cursor = Cursors.SizeAll;

        var movableSymbol = _listCanvasSymbolsVM.MovableBlockSymbol;
        var scalePartBlockSymbolVM = CanvasSymbolsVM.ScalePartBlockSymbol;

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

        CanvasSymbolsVM.Cursor = Cursors.Arrow;
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

        CanvasSymbolsVM.Cursor = Cursors.SizeAll;

        _listCanvasSymbolsVM.MovableBlockSymbol = this;
    }

    public void SetSelectedProperties()
    {
        IsSelected = true;

        if (this is IHasTextFieldVM selectedSymbolHasTextField)
        {
            _fontSizeControlVM.SetFontSize(selectedSymbolHasTextField);
            _fontFamilyControlVM.SetFontFamily(selectedSymbolHasTextField);
            _formatTextControlVM.SetFontText(selectedSymbolHasTextField);
            _textAlignmentControlVM.SetFormatAlignment(selectedSymbolHasTextField);
            _listCanvasSymbolsVM.SelectedSymbolsHasTextField.Add(selectedSymbolHasTextField);
        }

        _listCanvasSymbolsVM.SelectedBlockSymbols.Add(this);
    }
}