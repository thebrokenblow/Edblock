using System.Windows;
using EdblockViewModel;
using EdblockView.Components;
using EdblockViewModel.ComponentsVM;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    public Edblock()
    {
        InitializeComponent();

        var canvasSymbolsVM = new CanvasSymbolsVM();
        var scaleAllSymbolVM = new ScaleAllSymbolVM();
        var checkBoxLineGostVM = new CheckBoxLineGostVM();

        CanvasSymbols.CanvasSymbolsVM = canvasSymbolsVM;

        TopSettingsPanelUI.ScaleAllSymbolVM = scaleAllSymbolVM;
        TopSettingsPanelUI.CheckBoxLineGostVM = checkBoxLineGostVM;

        var edblockVM = new EdblockVM(canvasSymbolsVM, scaleAllSymbolVM, checkBoxLineGostVM);

        TopSettingsPanelUI.EdblockVM = edblockVM;

        DataContext = edblockVM;
    }
}