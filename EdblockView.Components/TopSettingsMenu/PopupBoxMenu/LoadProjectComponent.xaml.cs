using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using EdblockViewModel.Components.Interfaces;

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

    private readonly IProject? project;

    public LoadProjectComponent()
    {
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
            project?.Load(openFileDialog.FileName);
        }
        catch
        {
            MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
        }
    }
}