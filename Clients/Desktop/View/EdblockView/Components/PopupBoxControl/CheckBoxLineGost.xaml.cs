using System.Windows.Controls;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для CheckBoxLineGost.xaml
/// </summary>
public partial class CheckBoxLineGost : UserControl
{
    public LineStateStandardComponentVM? CheckBoxLineGostVM
    {
        set
        {
            DataContext = value;
        }
    }

    public CheckBoxLineGost() =>
         InitializeComponent();
}