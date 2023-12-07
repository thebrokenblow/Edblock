using System.Windows;
using EdblockViewModel;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    private CanvasSymbolsVM canvasSymbolsVM = new();
    public Edblock()
    {
        InitializeComponent();
        var edblockVM = new EdblockVM(canvasSymbolsVM);
        CanvasSymbols.CanvasSymbolsVM = canvasSymbolsVM;
        DataContext = edblockVM;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        canvasSymbolsVM.SaveProject();
    }
}