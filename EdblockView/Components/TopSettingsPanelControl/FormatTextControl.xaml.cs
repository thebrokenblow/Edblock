using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.TopSettingsPanelControl;

/// <summary>
/// Логика взаимодействия для FormatTextControl.xaml
/// </summary>
public partial class FormatTextControl : UserControl
{
    public FormatTextControlVM FormatTextControlVM
    {
        set
        {
            DataContext = value;
        }
    }

    public FormatTextControl()
    {
        InitializeComponent();
    }
}