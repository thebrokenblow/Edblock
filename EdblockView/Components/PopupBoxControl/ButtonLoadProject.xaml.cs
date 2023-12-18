using System.Windows;
using Microsoft.Win32;
using EdblockViewModel;
using System.Windows.Controls;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ButtonLoadProject.xaml
/// </summary>
public partial class ButtonLoadProject : UserControl
{
    private const string fileFilter = "Files(*.json)|*.json|All(*.*)|*";
    public EdblockVM? EdblockVM { get; set; }

    public ButtonLoadProject()
    {
        InitializeComponent();
    }

    private void LoadProject(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog()
        {
            Filter = fileFilter
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;

            try
            {
                EdblockVM?.LoadProject(filePath);
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
            }
        }
    }
}