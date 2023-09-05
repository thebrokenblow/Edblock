using EdblockViewModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    public CanvasSymbols()
    {
        InitializeComponent();
        DataContext = new CanvasSymbolsVM();
    }
}