using System.Windows.Controls;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.TopSettingsPanelControl;

/// <summary>
/// Логика взаимодействия для FontFamilyControl.xaml
/// </summary>
public partial class FontFamilyControl : UserControl
{
    private FontFamilyControlVM? fontFamilyControlVM;
    public FontFamilyControlVM? FontFamilyControlVM 
    {
        get => fontFamilyControlVM;
        set
        {
            fontFamilyControlVM = value;
            DataContext = fontFamilyControlVM;
        }
    }

    public FontFamilyControl()
    {
        InitializeComponent();

        var fontFamilys = new List<string>
        {
            "Times New Roman"
        };

        FontFamily.ItemsSource = fontFamilys;
    }
}