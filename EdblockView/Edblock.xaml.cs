using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel;
using EdblockView.Abstraction;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    private readonly EdblockVM edblockVM = new();
    private readonly CanvasSymbolsVM canvasSymbolsVM;
    
    public Edblock()
    {
        InitializeComponent();

        DataContext = edblockVM;
        canvasSymbolsVM = edblockVM.CanvasSymbolsVM;

        SizeChanged += ChangedSizeEdblock;
    }

    private void ChangedSizeEdblock(object sender, SizeChangedEventArgs e)
    {
        if (sender is Edblock edblock)
        {
            var actualWidthWindow = (int)edblock.ActualWidth;
            var actualHeightWindow = (int)edblock.ActualHeight;
            var actualWidthPanelSymbols = (int)edblock.PanelSymbolsUI.ActualWidth;
            var actualHeightTopSettingsPanel = (int)edblock.TopSettingsPanelUI.ActualHeight;

            canvasSymbolsVM.SetActualSizeWindow(
                actualWidthWindow, 
                actualHeightWindow, 
                actualWidthPanelSymbols, 
                actualHeightTopSettingsPanel);
        }
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
}