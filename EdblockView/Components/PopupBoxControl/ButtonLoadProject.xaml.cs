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
    public ButtonLoadProject()
    {
        InitializeComponent();
    }

    public CanvasSymbolsVM? CanvasSymbolsVM { get; set; }

    private void LoadProject(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog()
        {
            Filter = "File json|*.json"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            try
            {
                CanvasSymbolsVM?.LoadProject(filePath);
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файл, вероятно файл испорчен");
            }
        }
    }
}