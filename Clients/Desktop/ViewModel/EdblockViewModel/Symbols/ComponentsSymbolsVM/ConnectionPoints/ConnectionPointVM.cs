using System.Windows.Input;
using System.Collections.Generic;
using Prism.Commands;
using EdblockModel.EnumsModel;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.CoreVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class ConnectionPointVM : ObservableObject
{
    private double xCoordinate;
    public double XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value - diametr / 2;
            OnPropertyChanged();
        }
    }

    private double yCoordinate;
    public double YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value - diametr / 2;
            OnPropertyChanged();
        }
    }

    private bool isShow = false;
    public bool IsShow
    {
        get => isShow;
        set
        {
            isShow = value;
            OnPropertyChanged();
        }
    }

    private bool isSelected = false;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            OnPropertyChanged();
        }
    }

    private bool isHasConnectingLine = false;
    public bool IsHasConnectingLine
    {
        get => isHasConnectingLine;
        set
        {
            isHasConnectingLine = value;
            OnPropertyChanged();
        }
    }

    public double XCoordinateLineDraw { get; set; }
    public double YCoordinateLineDraw { get; set; }

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public BlockSymbolVM BlockSymbolVM { get; init; }
    public IHasConnectionPoint BlockSymbolHasConnectionPoint { get; init; }
    public SideSymbol Position { get; init; }

    private const int diametr = 8;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly LineStateStandardVM _checkBoxLineGostVM;

    public ConnectionPointVM(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM, LineStateStandardVM checkBoxLineGostVM, SideSymbol position)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;

        Position = position;

        BlockSymbolVM = blockSymbolVM;
        BlockSymbolHasConnectionPoint = (IHasConnectionPoint)blockSymbolVM;

        EnterCursor = new(ShowConnectionPoints);
        LeaveCursor = new(HideConnectionPoints);
    }

    public void ShowConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Hand, true, true);
    }

    public void HideConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Arrow, false, false);
    }

    public static void SetDisplayConnectionPoints(List<ConnectionPointVM> connectionPoints, bool isShow)
    {
        foreach (var connectionPoint in connectionPoints)
        {
            connectionPoint.IsShow = isShow;
        }
    }

    private void SetDisplayConnectionPoint(Cursor cursorDisplaying, bool isEnterConnectionPoint, bool isSelectConnectionPoint)
    {
        if (_canvasSymbolsVM.ScalePartBlockSymbol == null) //Код выполняется, если символ не масштабируется
        {
            var connectionPoints = BlockSymbolHasConnectionPoint.ConnectionPointsVM;

            SetDisplayConnectionPoints(connectionPoints, isEnterConnectionPoint);

            _canvasSymbolsVM.Cursor = cursorDisplaying;

            IsSelected = isSelectConnectionPoint;
        }
    }
}