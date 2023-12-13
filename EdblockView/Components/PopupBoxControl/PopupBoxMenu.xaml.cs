using EdblockViewModel;
using System.Windows.Controls;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для PopupBoxMenu.xaml
/// </summary>
public partial class PopupBoxMenu : UserControl
{
    public PopupBoxMenu()
    {
        InitializeComponent();
    }

    private CanvasSymbolsVM? canvasSymbolsVM;
    public CanvasSymbolsVM? CanvasSymbolsVM
    {
        get => canvasSymbolsVM;
        set
        {
            canvasSymbolsVM = value;

            ButtonSaveProject.CanvasSymbolsVM = canvasSymbolsVM;
            ButtonLoadProject.CanvasSymbolsVM = canvasSymbolsVM;
        }
    }
}