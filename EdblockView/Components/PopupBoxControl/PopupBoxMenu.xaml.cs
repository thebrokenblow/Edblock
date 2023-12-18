using EdblockViewModel;
using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для PopupBoxMenu.xaml
/// </summary>
public partial class PopupBoxMenu : UserControl
{
    public EdblockVM? EdblockVM
    {
        set
        {
            ButtonSaveProject.EdblockVM = value;
            ButtonLoadProject.EdblockVM = value;
        }
    }

    public CheckBoxLineGostVM CheckBoxLineGostVM
    {
        set
        {
            CheckBoxLineGost.CheckBoxLineGostVM = value;
        }
    }

    public ScaleAllSymbolVM ScaleAllSymbolVM
    {
        set
        {
            ScaleAllSymbol.ScaleAllSymbolVM = value;
        }
    }

    public PopupBoxMenu()
    {
        InitializeComponent();
    }
}