using System.Windows.Controls;
using Edblock.PagesViewModel.ComponentsViewModel;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для CheckBoxLineGost.xaml
/// </summary>
public partial class CheckBoxLineGost : UserControl
{
    public CheckBoxLineGostVM? CheckBoxLineGostVM
    {
        set
        {
            DataContext = value;
        }
    }

    public CheckBoxLineGost() =>
         InitializeComponent();
}