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
        DataContext = new CanvasSymbolsVM();
    }
}
