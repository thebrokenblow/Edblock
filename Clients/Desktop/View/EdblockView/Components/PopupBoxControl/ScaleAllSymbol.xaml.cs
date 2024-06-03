using System.Windows.Controls;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ScaleAllSymbol.xaml
/// </summary>
public partial class ScaleAllSymbol : UserControl
{
    public ScaleAllSymbolComponentVM ScaleAllSymbolVM
    {
        set
        {
            DataContext = value;
        }
    }

    public ScaleAllSymbol() =>
        InitializeComponent();
}