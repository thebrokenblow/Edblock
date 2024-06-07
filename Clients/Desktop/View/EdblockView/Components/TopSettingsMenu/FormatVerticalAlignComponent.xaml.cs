using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using System.Windows.Controls;

namespace EdblockView.Components.TopSettingsMenu;

/// <summary>
/// Логика взаимодействия для FormatVerticalAlignComponent.xaml
/// </summary>
public partial class FormatVerticalAlignComponent : UserControl
{
    private IFormatVerticalAlignComponentVM? formatVerticalAlignComponentVM;
    public FormatVerticalAlignComponent() =>
        InitializeComponent();

    private void SelectFormatVerticalAlign(object sender, SelectionChangedEventArgs e)
    {
        formatVerticalAlignComponentVM ??= (IFormatVerticalAlignComponentVM)DataContext;

        if (formatVerticalAlignComponentVM.IndexFormatVerticalAlign == -1)
        {
            FormatVerticalAlign.SelectedIndex = formatVerticalAlignComponentVM.PreviousIndexFormatVerticalAlign;
        }
    }
}