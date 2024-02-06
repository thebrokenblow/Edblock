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
    private const string fileName = "Edblock";
    private const string fileExtension = ".json";
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";

    public ButtonSaveProject()
    {
        InitializeComponent();
    }

    private void SaveProject(object sender, RoutedEventArgs e)
    {
        if (DataContext is EdblockVM edblock)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = fileFilter,
                FileName = fileName + fileExtension
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var fileInfo = new FileInfo(saveFileDialog.FileName);

                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

                var filePath = saveFileDialog.FileName.ToString();

                try
                {
                    edblock.SaveProject(filePath);
                }
                catch
                {
                    MessageBox.Show("Ошибка при сохранении проекта");
                }
            }
        }
    }
}