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
        if (DataContext is EdblockVM edblockVM)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    edblockVM?.LoadProject(filePath);
                }
                catch
                {
                    MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
                }
            }
        }
    }
}