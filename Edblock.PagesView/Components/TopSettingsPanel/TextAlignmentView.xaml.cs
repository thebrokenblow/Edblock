using Edblock.PagesViewModel.Components.TopSettingsPanel;
using System.Windows.Controls;

namespace Edblock.PagesView.Components.TopSettingsPanel;

/// <summary>
/// Логика взаимодействия для TextAlignmentView.xaml
/// </summary>
public partial class TextAlignmentView : UserControl
{
    private TextAlignmentViewModel? textAlignmentControlVM;

    public TextAlignmentView() =>
        InitializeComponent();

    private void SelectFormatAlign(object sender, SelectionChangedEventArgs e)
    {
        textAlignmentControlVM ??= (TextAlignmentViewModel)DataContext;

        if (textAlignmentControlVM.IndexFormatAlign == -1)
        {
            FormatAligns.SelectedIndex = textAlignmentControlVM.PreviousIndexFormatAlign;
        }
    }
}