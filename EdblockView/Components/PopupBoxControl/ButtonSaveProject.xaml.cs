using System.IO;
using System.Windows;
using Microsoft.Win32;
using EdblockViewModel;
using System.Windows.Controls;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ButtonSaveProject.xaml
/// </summary>
public partial class ButtonSaveProject : UserControl
{
    public ButtonSaveProject()
    {
        InitializeComponent();
    }

    public CanvasSymbolsVM? CanvasSymbolsVM { get; set; }

    private void SaveProject(object sender, RoutedEventArgs e)
    {
        var saveFileDialog = new SaveFileDialog()
        {
            Filter = "Files(*.json)|*.json|All(*.*)|*"
        };

        var file = new FileInfo("Edblock.json");
        saveFileDialog.FileName = file.Name;

        if (saveFileDialog.ShowDialog() == true)
        {
            var fileInfo = new FileInfo(saveFileDialog.FileName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            var filePath = saveFileDialog.FileName.ToString();

            CanvasSymbolsVM?.SaveProject(filePath);
        }
    }
}