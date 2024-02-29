using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel;
using EdblockView.Abstraction;
using EdblockViewModel.AbstractionsVM;
using System.Windows.Threading;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    private readonly EdblockVM edblockVM = new();

    private readonly DispatcherTimer dispatcherTimer;

    private MouseEventArgs? eventArgs;

    private double widthWindow;
    private double heightWindow;

    private const int offsetLeaveCanvas = 40;
    public Edblock()
    {
        InitializeComponent();

        DataContext = edblockVM;

        dispatcherTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.3)
        };

        SizeChanged += Edblock_SizeChanged;
    }

    private void Edblock_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is Edblock edblock)
        {
            widthWindow = edblock.ActualWidth;
            heightWindow = edblock.ActualHeight;

            if (CanvasSymbolsView.Width < widthWindow)
            {
                CanvasSymbolsView.Width = widthWindow;
            }

            if (CanvasSymbolsView.Height < heightWindow)
            {
                CanvasSymbolsView.Height = heightWindow;
            }
        }
    }

    private void AddSymbolView(object sender, MouseButtonEventArgs e)
    {
        if (sender is IFactorySymbolVM factorySymbolVM)
        {
            try
            {
                var blockSymbolVM = factorySymbolVM.CreateBlockSymbolVM(edblockVM);

                var isScaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM.IsScaleAllSymbolVM;

                if (isScaleAllSymbolVM && edblockVM.CanvasSymbolsVM.BlockSymbolVM.FirstOrDefault() is BlockSymbolVM firstBlockSymbolVM)
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

    private BlockSymbolVM? movableBlockSymbol;

    private void CanvasSymbols_MouseLeave(object sender, MouseEventArgs e)
    {
        
        var position = e.GetPosition(this);

        if (edblockVM.CanvasSymbolsVM.MovableBlockSymbol is not null)
        {
            movableBlockSymbol = edblockVM.CanvasSymbolsVM.MovableBlockSymbol;
            movableBlockSymbol.FirstMove = true;
            eventArgs ??= e;

            if (position.X >= widthWindow - panel.ActualWidth)
            {
                dispatcherTimer.Tick += IncreaseSizeHorizontal;
            }

            if (position.Y >= heightWindow - TopSettingsPanelUI.ActualHeight)
            {
                dispatcherTimer.Tick += IncreaseSizeVertical;
            }

            dispatcherTimer.Start();
        }
    }

    private void CanvasSymbols_MouseEnter(object sender, MouseEventArgs e)
    {
        movableBlockSymbol = null;

        dispatcherTimer.Tick -= IncreaseSizeVertical;
        dispatcherTimer.Tick -= IncreaseSizeHorizontal;

        dispatcherTimer.Stop();
    }

    private void IncreaseSizeVertical(object? sender, EventArgs e)
    {
        CanvasSymbolsView.Height = CanvasSymbolsView.ActualHeight + offsetLeaveCanvas;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.YCoordinate += offsetLeaveCanvas;

            var contentVerticalOffset = scrollViewer.ContentVerticalOffset;
            scrollViewer.ScrollToVerticalOffset(contentVerticalOffset + offsetLeaveCanvas);
        }
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        CanvasSymbolsView.Width = CanvasSymbolsView.ActualWidth + offsetLeaveCanvas;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.XCoordinate += offsetLeaveCanvas;

            var contentHorizontalOffset = scrollViewer.ContentHorizontalOffset;
            scrollViewer.ScrollToHorizontalOffset(contentHorizontalOffset + offsetLeaveCanvas);
        }
    }
}