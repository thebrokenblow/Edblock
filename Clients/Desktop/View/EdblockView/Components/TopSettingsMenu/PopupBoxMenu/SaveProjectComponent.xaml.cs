using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using EdblockViewModel.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для ButtonSaveProjectComponent.xaml
/// </summary>
public partial class SaveProjectComponent : UserControl
{
    private const string fileName = "Edblock";
    private const string fileExtension = ".json";
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";

    private readonly EditorVM? editorVM;
    public SaveProjectComponent()
    {
        if (App.serviceProvider is not null)
        {
            editorVM = App.serviceProvider.GetRequiredService<EditorVM>();
        }

        InitializeComponent();
    }

    private void SaveProject(object sender, RoutedEventArgs e)
    {
        if (editorVM is null)
        {
            return;
        }

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