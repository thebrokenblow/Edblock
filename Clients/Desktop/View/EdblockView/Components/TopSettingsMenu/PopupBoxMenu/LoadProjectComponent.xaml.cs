using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using EdblockViewModel.Pages;
using Microsoft.Extensions.DependencyInjection;

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

    private readonly EditorVM? editorVM;

    public LoadProjectComponent()
    {
        if (App.serviceProvider is not null)
        {
            editorVM = App.serviceProvider.GetRequiredService<EditorVM>();
        }

        InitializeComponent();
    }

    private void LoadProject(object sender, RoutedEventArgs e)
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