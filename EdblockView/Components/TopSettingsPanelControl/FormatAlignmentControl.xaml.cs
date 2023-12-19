using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.TopSettingsPanelControl;

/// <summary>
/// Логика взаимодействия для FormatAlignmentControl.xaml
/// </summary>
public partial class FormatAlignmentControl : UserControl
{
    public TextAlignmentControlVM TextAlignmentControlVM 
    {
        set
        {
            DataContext = value;
        }
    }

    public FormatAlignmentControl()
    {
        InitializeComponent();
    }
}