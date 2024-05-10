using System.Windows.Controls;
using Edblock.PagesViewModel.Components.PopupBox;

namespace Edblock.PagesView.Components.PopupBox;

/// <summary>
/// Логика взаимодействия для LineGostView.xaml
/// </summary>
public partial class LineGostView : UserControl
{
    public LineGostViewModel? LineGostViewModel
    {
        set
        {
            DataContext = value;
        }
    }

    public LineGostView() =>
        InitializeComponent();
}