using EdblockViewModel;
using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для PopupBoxMenu.xaml
/// </summary>
public partial class PopupBoxMenu : UserControl
{
    public CanvasSymbolsVM? CanvasSymbolsVM
    {
        set
        {
            ButtonSaveProject.CanvasSymbolsVM = value;
            ButtonLoadProject.CanvasSymbolsVM = value;
        }
    }

    public CheckBoxLineGostVM CheckBoxLineGostVM
    {
        set
        {
            CheckBoxLineGost.CheckBoxLineGostVM = value;
        }
    }

    public PopupBoxMenu()
    {
        InitializeComponent();
    }
}