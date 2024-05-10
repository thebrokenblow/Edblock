using System.Windows.Controls;
using Edblock.PagesViewModel.Components.TopSettingsPanel;

namespace EdblockView.Components.TopSettingsPanelControl;

/// <summary>
/// Логика взаимодействия для FormatAlignmentControl.xaml
/// </summary>
public partial class TextAlignmentControl : UserControl
{
    private TextAlignmentViewModel? textAlignmentViewModel;

    public TextAlignmentControl() =>
        InitializeComponent();

    private void SelectFormatAlign(object sender, SelectionChangedEventArgs e)
    {
        textAlignmentViewModel ??= (TextAlignmentViewModel)DataContext;

        if (textAlignmentViewModel.IndexFormatAlign == -1)
        {
            FormatAligns.SelectedIndex = textAlignmentViewModel.PreviousIndexFormatAlign;
        }
    }
}