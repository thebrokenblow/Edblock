using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using EdblockViewModel.Pages;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для ButtonLoadProjectComponent.xaml
/// </summary>
public partial class LoadProjectComponent : UserControl
{
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";
    private readonly OpenFileDialog openFileDialog = new()
    {
        Filter = fileFilter
    };
    private EditorVM? editorVM;

    public LoadProjectComponent() =>
        InitializeComponent();

    private void LoadProject(object sender, RoutedEventArgs e)
    {
        editorVM ??= (EditorVM)DataContext;

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