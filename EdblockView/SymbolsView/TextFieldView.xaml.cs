using System.Windows.Controls;

namespace EdblockView.SymbolsView;

/// <summary>
/// Логика взаимодействия для TextField.xaml
/// </summary>
public partial class TextFieldView : UserControl
{
    public TextFieldView()
    {
        InitializeComponent();
    }

    private void TextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var t = DataContext;
    }
}
