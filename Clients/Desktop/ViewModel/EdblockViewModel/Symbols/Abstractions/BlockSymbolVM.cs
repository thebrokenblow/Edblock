using System;
using System.Windows.Input;
using Prism.Commands;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Pages;
using EdblockViewModel.Core;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.Abstractions;

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
    public ICanvasSymbolsComponentVM CanvasSymbolsVM { get; init; }

    protected readonly IScaleAllSymbolComponentVM scaleAllSymbolComponentVM;
    protected readonly ILineStateStandardComponentVM lineStateStandardComponentVM;

    private readonly IListCanvasSymbolsComponentVM listCanvasSymbolsVM;
    private readonly ITopSettingsMenuComponentVM topSettingsMenuComponentVM;

    public BlockSymbolVM(EditorVM edblockVM)
    {
        EdblockVM = edblockVM;

        CanvasSymbolsVM = edblockVM.CanvasSymbolsComponentVM;
        lineStateStandardComponentVM = edblockVM.TopSettingsMenuComponentVM.PopupBoxMenuComponentVM.LineStateStandardComponentVM;
        scaleAllSymbolComponentVM = edblockVM.TopSettingsMenuComponentVM.PopupBoxMenuComponentVM.ScaleAllSymbolComponentVM;

        topSettingsMenuComponentVM = edblockVM.TopSettingsMenuComponentVM;
        listCanvasSymbolsVM = edblockVM.CanvasSymbolsComponentVM.ListCanvasSymbolsComponentVM;

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
        CanvasSymbolsVM.Cursor = Cursors.SizeAll;

        var movableSymbol = listCanvasSymbolsVM.MovableBlockSymbol;
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

        listCanvasSymbolsVM.MovableBlockSymbol = this;
    }

    public void SetSelectedProperties()
    {
        IsSelected = true;

        if (this is IHasTextFieldVM selectedSymbolHasTextField)
        {
            listCanvasSymbolsVM.SelectedSymbolsHasTextField.Add(selectedSymbolHasTextField);

            topSettingsMenuComponentVM.FontSizeComponentVM.SetFontSize(selectedSymbolHasTextField);
            topSettingsMenuComponentVM.FontFamilyComponentVM.SetFontFamily(selectedSymbolHasTextField);
            topSettingsMenuComponentVM.FormatTextComponentVM.SetFontText(selectedSymbolHasTextField);
            topSettingsMenuComponentVM.TextAlignmentComponentVM.SetFormatAlignment(selectedSymbolHasTextField);
        }

        listCanvasSymbolsVM.SelectedBlockSymbols.Add(this);
    }
}