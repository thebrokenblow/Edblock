using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using EdblockViewModel.Pages;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для LoadProjectComponent.xaml
/// </summary>
public partial class LoadProjectComponent : UserControl
{
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";
    private readonly OpenFileDialog openFileDialog = new()
    {
        Filter = fileFilter
    };

    private readonly EditorVM? editorVM;

    public LoadProjectComponent()
    {
        InitializeComponent();
    }

    private async void LoadProject(object sender, RoutedEventArgs e)
    {
        if (openFileDialog.ShowDialog() == false)
        {
            return;
        }

        try
        {
            editorVM.IsLoadingProject = true;
            await editorVM?.LoadProject(openFileDialog.FileName);
            editorVM.IsLoadingProject = false;

        }
        catch
        {
            MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
        }
    }
}