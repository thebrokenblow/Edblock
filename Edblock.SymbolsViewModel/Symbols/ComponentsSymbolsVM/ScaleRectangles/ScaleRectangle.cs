using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Edblock.SymbolsViewModel.Core;
using Edblock.PagesViewModel.Components;
using Edblock.PagesViewModel.Components.PopupBox;

namespace Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScaleRectangle : INotifyPropertyChanged
{

    private double xCoordinate;
    public double XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
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

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public DelegateCommand ClickScaleRectangle { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly CanvasSymbolsViewModel _canvasSymbolsVM;
    private readonly ScaleAllSymbolViewModel _scaleAllSymbolVM;
    private readonly BlockSymbolViewModel _blockSymbolVM;
    private readonly Cursor _cursorScaling;
    private readonly Func<(double, double)> _getCoordinateScaleRectangle;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsViewModel, double>? _getWidthSymbol;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsViewModel, double>? _getHeightSymbol;

    public ScaleRectangle(
        CanvasSymbolsViewModel canvasSymbolsVM,
        ScaleAllSymbolViewModel scaleAllSymbolVM,
        BlockSymbolViewModel blockSymbolVM,
        Cursor cursorScaling,
        Func<ScalePartBlockSymbol, CanvasSymbolsViewModel, double>? getWidthSymbol,
        Func<ScalePartBlockSymbol, CanvasSymbolsViewModel, double>? getHeightSymbol,
        Func<(double, double)> getCoordinateScaleRectangle
        )
    {
        _blockSymbolVM = blockSymbolVM;
        _cursorScaling = cursorScaling;
        _canvasSymbolsVM = canvasSymbolsVM;
        _scaleAllSymbolVM = scaleAllSymbolVM;
        _getWidthSymbol = getWidthSymbol;
        _getHeightSymbol = getHeightSymbol;

        _getCoordinateScaleRectangle = getCoordinateScaleRectangle;
        (XCoordinate, YCoordinate) = getCoordinateScaleRectangle.Invoke();

        EnterCursor = new(ShowScaleRectangles);
        LeaveCursor = new(HideScaleRectangles);
        ClickScaleRectangle = new(SaveScaleRectangle);
    }

    public void ChangeCoordination()
    {
        (XCoordinate, YCoordinate) = _getCoordinateScaleRectangle.Invoke();
    }

    public static void SetStateDisplay(List<ScaleRectangle> scaleRectangles, bool isShowScaleRectangle)
    {
        foreach (var scaleRectangle in scaleRectangles)
        {
            scaleRectangle.IsShow = isShowScaleRectangle;
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    private void ShowScaleRectangles()
    {
        if (_canvasSymbolsVM.ScalePartBlockSymbol == null)
        {
            _canvasSymbolsVM.Cursor = _cursorScaling;

            if (_blockSymbolVM is IHasScaleRectangles blockSymbolHasScaleRectangles)
            {
                SetStateDisplay(blockSymbolHasScaleRectangles.ScaleRectangles, true);
            }
        }
    }

    private void HideScaleRectangles()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;

        if (_blockSymbolVM is IHasScaleRectangles blockSymbolHasScaleRectangles)
        {
            SetStateDisplay(blockSymbolHasScaleRectangles.ScaleRectangles, false);
        }
    }

    private void SaveScaleRectangle()
    {
        _canvasSymbolsVM.ScalePartBlockSymbol = new(_blockSymbolVM, _cursorScaling, _getWidthSymbol, _getHeightSymbol, _scaleAllSymbolVM, _canvasSymbolsVM.BlockSymbolsVM);
    }
}