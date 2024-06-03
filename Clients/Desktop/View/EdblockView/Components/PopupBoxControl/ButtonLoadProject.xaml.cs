using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using EdblockViewModel.Pages;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ButtonLoadProject.xaml
/// </summary>
public partial class ButtonLoadProject : UserControl
{
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";
    private readonly OpenFileDialog openFileDialog = new()
    {
        Filter = fileFilter
    };

    public ButtonLoadProject() =>
        InitializeComponent();

    private void LoadProject(object sender, RoutedEventArgs e)
    {
        if (DataContext is EditorVM editorVM)
        {
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }

            try
            {
                editorVM?.LoadProject(openFileDialog.FileName);
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
            }
        }
    }
}