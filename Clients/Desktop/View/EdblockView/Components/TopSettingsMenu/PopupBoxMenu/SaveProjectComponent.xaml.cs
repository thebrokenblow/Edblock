using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using EdblockViewModel.Pages;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для ButtonSaveProjectComponent.xaml
/// </summary>
public partial class SaveProjectComponent : UserControl
{
    private const string fileName = "Edblock";
    private const string fileExtension = ".json";
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";

    private EditorVM? editorVM;
    public SaveProjectComponent() =>
        InitializeComponent();

    private void SaveProject(object sender, RoutedEventArgs e)
    {
        editorVM ??= (EditorVM)DataContext;

        var saveFileDialog = new SaveFileDialog
        {
            Filter = fileFilter,
            FileName = fileName + fileExtension
        };

        if (saveFileDialog.ShowDialog() == false)
        {
            return;
        }

        var fileInfo = new FileInfo(saveFileDialog.FileName);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        var filePath = saveFileDialog.FileName.ToString();
        try
        {
            editorVM.SaveProject(filePath);
        }
        catch
        {
            MessageBox.Show("Ошибка при сохранении проекта");
        }
    }
}