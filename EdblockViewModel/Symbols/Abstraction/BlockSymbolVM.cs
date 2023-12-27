using System;
using Prism.Commands;
using System.Windows.Input;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using EdblockModel.SymbolsModel;
using EdblockModel.SymbolsModel.Enum;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.Abstraction;

public abstract class BlockSymbolVM : SymbolVM
{
    public List<ConnectionPointVM> ConnectionPoints { get; init; }
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

    public TextFieldVM TextFieldVM { get; init; }
    public BlockSymbolModel BlockSymbolModel { get; init; }

    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FontFamilyControlVM _fontFamilyControlVM;
    private readonly FontSizeControlVM _fontSizeControlVM;
    private readonly TextAlignmentControlVM _textAlignmentControlVM;
    private readonly FormatTextControlVM _formatTextControlVM;

    public BlockSymbolVM(EdblockVM edblockVM)
    {
        _canvasSymbolsVM = edblockVM.CanvasSymbolsVM;
        _fontSizeControlVM = edblockVM.FontSizeControlVM;
        _fontFamilyControlVM = edblockVM.FontFamilyControlVM;
        _textAlignmentControlVM = edblockVM.TextAlignmentControlVM;
        _formatTextControlVM = edblockVM.FormatTextControlVM;

        Id = Guid.NewGuid().ToString();

        BlockSymbolModel = CreateBlockSymbolModel();

        TextFieldVM = new(_canvasSymbolsVM, this);

        var factoryConnectionPoints = new FactoryConnectionPoints(_canvasSymbolsVM, edblockVM.PopupBoxMenuVM.CheckBoxLineGostVM, this);
        ConnectionPoints = factoryConnectionPoints.CreateConnectionPoints();

        var factoryScaleRectangles = new FactoryScaleRectangles(_canvasSymbolsVM, edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM,  this);
        ScaleRectangles = factoryScaleRectangles.Create();

        Width = defaultWidth;
        Height = defaultHeigth;

        MouseEnter = new(ShowAuxiliaryElements);
        MouseLeave = new(HideAuxiliaryElements);
        MouseLeftButtonDown = new(SetMovableSymbol);
    }

    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);
    public abstract BlockSymbolModel CreateBlockSymbolModel();

    public void SetCoordinate((int x, int y) currentCoordinate, (int x, int y) previousCoordinate)
    {
        var currentDrawnLineSymbol = _canvasSymbolsVM.СurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol == null) //Условие истино, если не рисуется линия
        {
            _canvasSymbolsVM.RemoveSelectDrawnLine();

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
    }

    public void ShowAuxiliaryElements()
    {
        _canvasSymbolsVM.Cursor = Cursors.SizeAll;

        var movableSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var scalePartBlockSymbolVM = _canvasSymbolsVM.ScalePartBlockSymbol;

        if (movableSymbol == null && scalePartBlockSymbolVM == null)  // Условие истино, когда символ не перемещается и не масштабируется (просто навёл курсор)
        {
            ConnectionPointVM.SetDisplayConnectionPoints(ConnectionPoints, true);
            ScaleRectangle.SetStateDisplay(ScaleRectangles, true);
            TextFieldVM.Cursor = Cursors.SizeAll;
        }
    }

    public void HideAuxiliaryElements()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;

        ConnectionPointVM.SetDisplayConnectionPoints(ConnectionPoints, false);
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

    internal ConnectionPointVM GetConnectionPoint(PositionConnectionPoint outgoingPosition)
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

    public void SetMovableSymbol()
    {
        ConnectionPointVM.SetDisplayConnectionPoints(ConnectionPoints, false);
        ScaleRectangle.SetStateDisplay(ScaleRectangles, false);

        _canvasSymbolsVM.Cursor = Cursors.SizeAll;

        _canvasSymbolsVM.MovableBlockSymbol = this;
        _canvasSymbolsVM.SetCurrentRedrawLines(this);
    }

    public void Select()
    {
        IsSelected = true;

        _fontSizeControlVM.SetFontSize(this);
        _fontFamilyControlVM.SetFontFamily(this);
        _textAlignmentControlVM.SetFormatAlignment(this);
        _formatTextControlVM.SetFontText(this);

        _canvasSymbolsVM.SelectedBlockSymbols.Add(this);
    }
}