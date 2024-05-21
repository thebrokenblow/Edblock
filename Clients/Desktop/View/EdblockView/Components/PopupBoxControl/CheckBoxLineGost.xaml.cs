using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для CheckBoxLineGost.xaml
/// </summary>
public partial class CheckBoxLineGost : UserControl
{
    public LineStateStandardVM? CheckBoxLineGostVM
    {
        set
        {
            DataContext = value;
        }
    }

    public CheckBoxLineGost() =>
         InitializeComponent();
}