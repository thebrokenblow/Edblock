using System.Windows;
using EdblockViewModel;

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
        var edblockVM = new EdblockVM(canvasSymbolsVM);
        CanvasSymbols.CanvasSymbolsVM = canvasSymbolsVM;
        CanvasSymbols.DraggableSymbol = canvasSymbolsVM.DraggableSymbol;
        DataContext = edblockVM;
    }
}