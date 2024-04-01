using System.Windows;
using EdblockViewModel.PagesVM;
using EdblockViewModel.StoresVM;

namespace EdblockView;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore naigationStoreMainWindow = new();
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow()
        {
            DataContext = new MainWindowVM(naigationStoreMainWindow)
        };

        MainWindow.Show();
        base.OnStartup(e);
    }
}