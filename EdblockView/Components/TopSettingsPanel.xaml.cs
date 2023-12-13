using EdblockViewModel;
using System.Windows.Controls;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для TopSettingsPanel.xaml
/// </summary>
public partial class TopSettingsPanel : UserControl
{
    public TopSettingsPanel()
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
            PopupBoxMenuUI.CanvasSymbolsVM = canvasSymbolsVM;
        }
    }
}