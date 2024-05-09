using System.Windows.Controls;
using Edblock.PagesViewModel.ComponentsViewModel;

namespace EdblockView.Components.TopSettingsPanelControl;

/// <summary>
/// Логика взаимодействия для FormatAlignmentControl.xaml
/// </summary>
public partial class TextAlignmentControl : UserControl
{
    private TextAlignmentControlVM? textAlignmentControlVM;

    public TextAlignmentControl() =>
        InitializeComponent();

    private void SelectFormatAlign(object sender, SelectionChangedEventArgs e)
    {
        textAlignmentControlVM ??= (TextAlignmentControlVM)DataContext;

        if (textAlignmentControlVM.IndexFormatAlign == -1)
        {
            FormatAligns.SelectedIndex = textAlignmentControlVM.PreviousIndexFormatAlign;
        }
    }
}