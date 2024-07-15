using System.Windows.Controls;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для PopupBoxMenuComponent.xaml
/// </summary>
public partial class PopupBoxMenuComponent : UserControl
{
    private Canvas? canvasSymbolsComponent;
    public Canvas? CanvasSymbolsComponent
    {
        get => canvasSymbolsComponent;
        set
        {
            canvasSymbolsComponent = value;
            printProjectComponent.CanvasSymbolsComponent = canvasSymbolsComponent;
            saveImgComponent.CanvasSymbolsComponent = canvasSymbolsComponent;
        }
    }


    public PopupBoxMenuComponent()
    {
        InitializeComponent();
    }
}