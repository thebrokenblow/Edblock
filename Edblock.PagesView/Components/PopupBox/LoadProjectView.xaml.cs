using Edblock.PagesViewModel.Pages;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace Edblock.PagesView.Components.PopupBox;

/// <summary>
/// Логика взаимодействия для LoadProjectView.xaml
/// </summary>
public partial class LoadProjectView : UserControl
{
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";
    private readonly OpenFileDialog openFileDialog = new()
    {
        Filter = fileFilter
    };

    public LoadProjectView() =>
        InitializeComponent();

    private void LoadProject(object sender, RoutedEventArgs e)
    {
        if (DataContext is EditorViewModel editorViewModel)
        {
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }

            try
            {
                editorViewModel?.LoadProject(openFileDialog.FileName);
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
            }
        }
    }
}
