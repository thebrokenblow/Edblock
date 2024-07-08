using EdblockViewModel.Symbols.LinesSymbolVM.Components;
using System.Windows.Controls;
using System.Windows.Input;

namespace Edblock.SymbolsView.Symbols.Lines;

/// <summary>
/// Логика взаимодействия для DrawnLineSymbolView.xaml
/// </summary>
public partial class DrawnLineSymbolView : UserControl
{
    public DrawnLineSymbolView() =>
        InitializeComponent();

    private void Border_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is Border border)
        {
            if (border.DataContext is MovableRectangleLineVM movableRectangleLineVM)
            {
                movableRectangleLineVM.EnterCursor();
               
            }
        }
    }

    private void Border_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is Border border)
        {
            if (border.DataContext is MovableRectangleLineVM movableRectangleLineVM)
            {
                movableRectangleLineVM.LeaveCursor();
            }
        }
    }

    private void Border_MouseDown(object sender, MouseEventArgs e)
    {
        if (sender is Border border)
        {
            if (border.DataContext is MovableRectangleLineVM movableRectangleLineVM)
            {
                movableRectangleLineVM.SubscribeToChangeCoordinates();
            }
        }

        e.Handled = true;   
    }
}
