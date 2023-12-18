using EdblockViewModel;
using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для TopSettingsPanel.xaml
/// </summary>
public partial class TopSettingsPanel : UserControl
{
    public EdblockVM? EdblockVM
    {
        set
        {
            PopupBoxMenuUI.EdblockVM = value;
        }
    }

    public CheckBoxLineGostVM CheckBoxLineGostVM
    {
        set
        {
            PopupBoxMenuUI.CheckBoxLineGostVM = value;
        }
    }

    public ScaleAllSymbolVM ScaleAllSymbolVM
    {
        set
        {
            PopupBoxMenuUI.ScaleAllSymbolVM = value;
        }
    }

    public FontFamilyControlVM FontFamilyControlVM
    {
        set
        {
            FontFamilyControlUI.FontFamilyControlVM = value;
        }
    }

    public TopSettingsPanel()
    {
        InitializeComponent();
    }
}