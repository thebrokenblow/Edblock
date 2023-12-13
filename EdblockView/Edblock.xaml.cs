using System.Windows;
using EdblockViewModel;
using EdblockView.Components;

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

        CanvasSymbols.CanvasSymbolsVM = canvasSymbolsVM;
        TopSettingsPanelUI.CanvasSymbolsVM = canvasSymbolsVM;

        var edblockVM = new EdblockVM(canvasSymbolsVM);
        DataContext = edblockVM;
    }
}