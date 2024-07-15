using EdblockViewModel.Components.Interfaces;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для SaveProjectComponent.xaml
/// </summary>
public partial class SaveProjectComponent : UserControl
{
    private const string fileName = "Edblock";
    private const string fileExtension = ".json";
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";

    private readonly IProject? project;

    public SaveProjectComponent()
    {
        InitializeComponent();
    }

    private void SaveProject(object sender, RoutedEventArgs e)
    {
        if (project is null)
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
            project.Save(filePath);
        }
        catch
        {
            MessageBox.Show("Ошибка при сохранении проекта");
        }
    }
}