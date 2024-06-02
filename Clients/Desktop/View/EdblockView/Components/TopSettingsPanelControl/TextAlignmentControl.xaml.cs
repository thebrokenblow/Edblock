using System.Windows.Controls;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent;

namespace EdblockView.Components.TopSettingsPanelControl;

/// <summary>
/// Логика взаимодействия для FormatAlignmentControl.xaml
/// </summary>
public partial class TextAlignmentControl : UserControl
{
    private TextAlignmentComponentVM? textAlignmentControlVM;

    public TextAlignmentControl() =>
        InitializeComponent();

    private void SelectFormatAlign(object sender, SelectionChangedEventArgs e)
    {
        textAlignmentControlVM ??= (TextAlignmentComponentVM)DataContext;

        if (textAlignmentControlVM.IndexFormatAlign == -1)
        {
            FormatAligns.SelectedIndex = textAlignmentControlVM.PreviousIndexFormatAlign;
        }
    }
}