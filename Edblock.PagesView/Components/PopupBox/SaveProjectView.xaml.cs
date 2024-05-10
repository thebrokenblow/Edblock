using Edblock.PagesViewModel.Pages;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Edblock.PagesView.Components.PopupBox;

/// <summary>
/// Логика взаимодействия для SaveProject.xaml
/// </summary>
public partial class SaveProjectView : UserControl
{
    private const string fileName = "Edblock";
    private const string fileExtension = ".json";
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";

    public SaveProjectView() =>
        InitializeComponent();

    private void Save(object sender, RoutedEventArgs e)
    {
        if (DataContext is EditorViewModel editorVM)
        {
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
}
