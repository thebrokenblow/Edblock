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
        var edblock = (Edblock)sender;

        var widthWindow = (int)edblock.ActualWidth;
        var heightWindow = (int)edblock.ActualHeight;

        var widthPanelSymbols = (int)edblock.PanelSymbols.ActualWidth;
        var heightTopSettingsPanel = (int)edblock.TopSettingsPanelUI.ActualHeight;

        edblockVM.CanvasSymbolsVM.SetActualSize(
            widthWindow, 
            heightWindow, 
            widthPanelSymbols, 
            heightTopSettingsPanel);
    }
}