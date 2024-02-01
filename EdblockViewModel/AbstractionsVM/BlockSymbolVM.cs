using System;
using System.Windows.Input;
using Prism.Commands;
using EdblockModel.Enum;
using EdblockModel.AbstractionsModel;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.AbstractionsVM;

public abstract class BlockSymbolVM : SymbolVM
{
    public Guid Id { get; set; }

    private string? color;
    public override string? Color
    {
        get => color;
        set
        {
            color = value;

            if (BlockSymbolModel is not null)
            {
                BlockSymbolModel.Color = color;
            }

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

            if (BlockSymbolModel is not null)
            {
                BlockSymbolModel.Width = width;
            }

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

            if (BlockSymbolModel is not null)
            {
                BlockSymbolModel.Height = heigth;
            }

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

            if (BlockSymbolModel is not null)
            {
                BlockSymbolModel.XCoordinate = xCoordinate;
            }

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

            if (BlockSymbolModel is not null)
            {
                BlockSymbolModel.YCoordinate = yCoordinate;
            }

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

    public DelegateCommand MouseEnter { get; set; }
    public DelegateCommand MouseLeave { get; set; }
    public DelegateCommand MouseLeftButtonDown { get; set; }
    public BlockSymbolModel? BlockSymbolModel { get; init; }
    public CanvasSymbolsVM CanvasSymbolsVM { get; init; }

    protected readonly CheckBoxLineGostVM _checkBoxLineGostVM;

    private readonly FontFamilyControlVM _fontFamilyControlVM;
    private readonly FontSizeControlVM _fontSizeControlVM;
    private readonly TextAlignmentControlVM _textAlignmentControlVM;
    private readonly FormatTextControlVM _formatTextControlVM;

    public BlockSymbolVM(EdblockVM edblockVM)
    {
        CanvasSymbolsVM = edblockVM.CanvasSymbolsVM;

        _fontSizeControlVM = edblockVM.FontSizeControlVM;
        _fontFamilyControlVM = edblockVM.FontFamilyControlVM;
        _formatTextControlVM = edblockVM.FormatTextControlVM;
        _textAlignmentControlVM = edblockVM.TextAlignmentControlVM;
        _checkBoxLineGostVM = edblockVM.PopupBoxMenuVM.CheckBoxLineGostVM;

        Id = Guid.NewGuid();

        MouseEnter = new(ShowAuxiliaryElements);
        MouseLeave = new(HideAuxiliaryElements);
        MouseLeftButtonDown = new(SetMovableSymbol);
    }

    public abstract BlockSymbolModel CreateBlockSymbolModel();
    public abstract void SetWidth(double width);
    public abstract void SetHeight(double height);

    public void SetCoordinate((int x, int y) currentCoordinate, (int x, int y) previousCoordinate)
    {
        var currentDrawnLineSymbol = CanvasSymbolsVM.СurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol != null) //Условие истино, если не рисуется линия
        {
            return;
        }

        CanvasSymbolsVM.RemoveSelectDrawnLine();

        if (XCoordinate == 0 || YCoordinate == 0) //TODO: здесь должна быть проверка на то что это новый симвоб
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
        if (CanvasSymbolsVM.Cursor == Cursors.Arrow)
        {
            CanvasSymbolsVM.Cursor = Cursors.SizeAll;
        }

        var movableSymbol = CanvasSymbolsVM.MovableBlockSymbol;
        var scalePartBlockSymbolVM = CanvasSymbolsVM.ScalePartBlockSymbol;

        if (movableSymbol is not null || scalePartBlockSymbolVM is not null) // Условие истино, когда символ перемещается и масштабируется(просто навёл курсор)
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
            var connectionPoints = symbolHasConnectionPoint.ConnectionPoints;

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

    protected void ChangeCoordinateAuxiliaryElements()
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

    internal ConnectionPointVM GetConnectionPoint(SideSymbol outgoingPosition)
    {
        if (this is IHasConnectionPoint symbolHasConnectionPoint)
        {
            var connectionPoints = symbolHasConnectionPoint.ConnectionPoints;

            foreach (var connectionPoint in connectionPoints)
            {
                if (connectionPoint.Position == outgoingPosition)
                {
                    return connectionPoint;
                }
            }
        }

        throw new Exception("Точки соединения с такой позицией нет");
    }

    public void SetMovableSymbol()
    {
        SetDisplayScaleRectangles(false);
        SetDisplayConnectionPoints(false);

        CanvasSymbolsVM.Cursor = Cursors.SizeAll;

        CanvasSymbolsVM.MovableBlockSymbol = this;
        CanvasSymbolsVM.SetCurrentRedrawLines(this);
    }

    public void Select()
    {
        IsSelected = true;

        _fontSizeControlVM.SetFontSize(this);
        _formatTextControlVM.SetFontText(this);
        _fontFamilyControlVM.SetFontFamily(this);
        _textAlignmentControlVM.SetFormatAlignment(this);

        CanvasSymbolsVM.SelectedBlockSymbols.Add(this);
    }
}