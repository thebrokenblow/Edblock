using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.TopSettingsPanelControl;

/// <summary>
/// Логика взаимодействия для FontSizeControl.xaml
/// </summary>
public partial class FontSizeControl : UserControl
{
    public FontSizeControlVM FontSizeControlVM 
    {
        set
        {
            DataContext = value;        
        }
    }

    public FontSizeControl()
    {
        InitializeComponent();
    }
}