using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using EdblockViewModel.PagesVM;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ButtonLoadProject.xaml
/// </summary>
public partial class ButtonLoadProject : UserControl
{
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";
    private readonly OpenFileDialog openFileDialog;
    public ButtonLoadProject()
    {
        InitializeComponent();

        openFileDialog = new OpenFileDialog()
        {
            Filter = fileFilter
        };
    }

    private void LoadProject(object sender, RoutedEventArgs e)
    {
        if (DataContext is EditorVM editorVM)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    editorVM?.LoadProject(filePath);
                }
                catch
                {
                    MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
                }
            }
        }
    }
}