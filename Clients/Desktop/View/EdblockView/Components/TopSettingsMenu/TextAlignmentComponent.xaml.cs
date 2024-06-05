using System.Windows.Controls;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;

namespace EdblockView.Components.TopSettingsMenu;

/// <summary>
/// Логика взаимодействия для FormatAlignmentControl.xaml
/// </summary>
public partial class TextAlignmentComponent : UserControl
{
    private ITextAlignmentComponentVM? textAlignmentControlVM;

    public TextAlignmentComponent() =>
        InitializeComponent();

    private void SelectFormatAlign(object sender, SelectionChangedEventArgs e)
    {
        textAlignmentControlVM ??= (ITextAlignmentComponentVM)DataContext;

        if (textAlignmentControlVM.IndexFormatAlign == -1)
        {
            FormatAligns.SelectedIndex = textAlignmentControlVM.PreviousIndexFormatAlign;
        }
    }
}