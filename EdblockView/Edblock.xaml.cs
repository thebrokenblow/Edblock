using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel;
using EdblockView.Abstraction;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    private readonly EdblockVM edblockVM = new();
    
    //TODO: устранить дублирование кода в этом классе
    public Edblock()
    {
        InitializeComponent();

        DataContext = edblockVM;

        SizeChanged += ChangedSizeWindow;
    }

    private void AddSymbolView(object sender, MouseButtonEventArgs e)
    {
        if (sender is IFactorySymbolVM factorySymbolVM)
        {
            try
            {
                var blockSymbolVM = factorySymbolVM.CreateBlockSymbolVM(edblockVM);

                var firstBlockSymbolsVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM.FirstOrDefault();
                var isScaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM.IsScaleAllSymbolVM;

                if (isScaleAllSymbolVM && firstBlockSymbolsVM is BlockSymbolVM firstBlockSymbolVM)
                {
                    blockSymbolVM.SetWidth(firstBlockSymbolVM.Width);
                    blockSymbolVM.SetHeight(firstBlockSymbolVM.Height);
                }

                edblockVM.AddBlockSymbol(blockSymbolVM);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    private void ChangedSizeWindow(object sender, SizeChangedEventArgs e)
    {
        if (sender is Edblock edblock)
        {
            var widthWindow = (int)edblock.ActualWidth;
            var heightWindow = (int)edblock.ActualHeight;

            var widthPanelSymbols = (int)edblock.PanelSymbols.ActualWidth;
            var heightTopSettingsPanel = (int)edblock.TopSettingsPanelUI.ActualHeight;

            edblockVM.CanvasSymbolsVM.SetActualSize(widthWindow, heightWindow, widthPanelSymbols, heightTopSettingsPanel);
        }
    }

    //private double GetMaxXCoordinateSymbol()
    //{
    //    var blockSymbolsVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM;
    //    var drawnLinesSymbolVM = edblockVM.CanvasSymbolsVM.DrawnLinesSymbolVM;

    //    var maxXCoordinate = blockSymbolsVM.Max(vm => vm.XCoordinate + vm.Width);

    //    foreach (var drawnLineSymbolVM in drawnLinesSymbolVM)
    //    {
    //        foreach (var lineSymbolVM in drawnLineSymbolVM.LinesSymbolVM)
    //        {
    //            maxXCoordinate = Math.Max(maxXCoordinate, Math.Max(lineSymbolVM.X1, lineSymbolVM.X2));
    //        }
    //    }

    //    return maxXCoordinate;
    //}

    //private double GetMaxYCoordinateSymbol()
    //{
    //    var blockSymbolsVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM;
    //    var drawnLinesSymbolVM = edblockVM.CanvasSymbolsVM.DrawnLinesSymbolVM;

    //    var maxYCoordinate = blockSymbolsVM.Max(b => b.YCoordinate + b.Height);

    //    foreach (var drawnLinesSymbol in drawnLinesSymbolVM)
    //    {
    //        foreach (var linesSymbol in drawnLinesSymbol.LinesSymbolVM)
    //        {
    //            maxYCoordinate = Math.Max(maxYCoordinate, Math.Max(linesSymbol.Y1, linesSymbol.Y2));
    //        }
    //    }

    //    return maxYCoordinate;
    //}

    //private void CanvasSymbols_MouseEnter(object sender, MouseEventArgs e)
    //{
    //    movableBlockSymbol = null;

    //    dispatcherTimer.Tick -= IncreaseSizeVertical;
    //    dispatcherTimer.Tick -= DecreaseSizeVertical;

    //    dispatcherTimer.Tick -= IncreaseSizeHorizontal;
    //    dispatcherTimer.Tick -= DecreaseSizeHorizontal;

    //    dispatcherTimer.Stop();
    //}

    //private void IncreaseSizeVertical(object? sender, EventArgs e)
    //{
    //    CanvasSymbolsView.Height = CanvasSymbolsView.ActualHeight + offsetLeaveCanvas;

    //    if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
    //    {
    //        return;
    //    }

    //    if (movableBlockSymbol is not null)
    //    {
    //        movableBlockSymbol.YCoordinate += offsetLeaveCanvas;
    //    }
    //    else if (currentDrawnLineSymbol is not null)
    //    {
    //        var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
    //        lastLineSymbolVM.Y2 += offsetLeaveCanvas;

    //        currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
    //    }

    //    var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset + offsetLeaveCanvas;
    //    scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);
    //}

    //private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    //{
    //    CanvasSymbolsView.Width = CanvasSymbolsView.ActualWidth + offsetLeaveCanvas;

    //    if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
    //    {
    //        return;
    //    }

    //    if (movableBlockSymbol is not null)
    //    {
    //        movableBlockSymbol.XCoordinate += offsetLeaveCanvas;
    //    }
    //    else if (currentDrawnLineSymbol is not null)
    //    {
    //        var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
    //        lastLineSymbolVM.X2 += offsetLeaveCanvas;

    //        currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
    //    }

    //    var horizontalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset + offsetLeaveCanvas;
    //    scrollViewer.ScrollToHorizontalOffset(horizontalOffsetScrollViewer);
    //}

    //private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    //{
    //    if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
    //    {
    //        return;
    //    }

    //    var widthCanvas = (int)(widthWindow - PanelSymbols.ActualWidth);
    //    var actualWidthCanvas = (int)CanvasSymbolsView.ActualWidth;

    //    if (actualWidthCanvas < widthCanvas)
    //    {
    //        return;
    //    }

    //    if (movableBlockSymbol is not null)
    //    {
    //        var extremeCoordinateSymbolBehind = movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

    //        if (PanelSymbols.ActualWidth <= extremeCoordinateSymbolBehind)
    //        {
    //            movableBlockSymbol.XCoordinate -= offsetLeaveCanvas;
    //        }
    //    }

    //    if (currentDrawnLineSymbol is not null)
    //    {
    //        var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
    //        lastLineSymbolVM.X2 -= offsetLeaveCanvas;

    //        currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
    //    }

    //    var maxXCoordinate = GetMaxXCoordinateSymbol();

    //    if (CanvasSymbolsView.ActualWidth > maxXCoordinate + minIndentation)
    //    {
    //        CanvasSymbolsView.Width -= offsetLeaveCanvas;
    //    }

    //    var horizontalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset - offsetLeaveCanvas;
    //    scrollViewer.ScrollToHorizontalOffset(horizontalOffsetScrollViewer);
    //}

    //private void DecreaseSizeVertical(object? sender, EventArgs e)
    //{
    //    if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
    //    {
    //        return;
    //    }

    //    var heightCanvas = (int)(heightWindow - TopSettingsPanelUI.ActualHeight);
    //    var actualHeightCanvas = (int)CanvasSymbolsView.ActualHeight + heightScroll;

    //    if (actualHeightCanvas < heightCanvas)
    //    {
    //        return;
    //    }

    //    if (movableBlockSymbol is not null)
    //    {
    //        var extremeCoordinateSymbolBehind = movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;

    //        if (TopSettingsPanelUI.ActualHeight <= extremeCoordinateSymbolBehind)
    //        {
    //            movableBlockSymbol.YCoordinate -= offsetLeaveCanvas;
    //        }
    //    }

    //    if (currentDrawnLineSymbol is not null)
    //    {
    //        var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
    //        lastLineSymbolVM.Y2 -= offsetLeaveCanvas;

    //        currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
    //    }

    //    var maxYCoordinate = GetMaxYCoordinateSymbol();

    //    if (CanvasSymbolsView.ActualHeight > maxYCoordinate + minIndentation)
    //    {
    //        CanvasSymbolsView.Height -= offsetLeaveCanvas;
    //    }

    //    var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset - offsetLeaveCanvas;
    //    scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);
    //}
}