using System.Windows.Controls;
using Edblock.PagesViewModel.Components.PopupBox;

namespace Edblock.PagesView.Components.PopupBox;

/// <summary>
/// Логика взаимодействия для ScaleAllSymbol.xaml
/// </summary>
public partial class ScaleAllSymbolView : UserControl
{
    public ScaleAllSymbolViewModel ScaleAllSymbolViewModel
    {
        set
        {
            DataContext = value;
        }
    }

    //public ScaleAllSymbolView() =>
    //    InitializeComponent();
}