using EdblockViewModel.Components;
using System.Windows.Controls;

namespace EdblockView.Components.TopSettingsMenu;

/// <summary>
/// Логика взаимодействия для TopSettingsMenuComponent.xaml
/// </summary>
public partial class TopSettingsMenuComponent : UserControl
{
    private Canvas? canvasSymbolsComponent;
    public Canvas? CanvasSymbolsComponent
    {
        get => canvasSymbolsComponent;
        set
        {
            canvasSymbolsComponent = value;
            popupBoxMenuComponent.CanvasSymbolsComponent = canvasSymbolsComponent;
        }
    }

    public TopSettingsMenuComponent() =>
        InitializeComponent();
}