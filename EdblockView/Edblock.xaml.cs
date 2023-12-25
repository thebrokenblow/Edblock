using System.Windows;
using EdblockViewModel;
using System.Windows.Input;
using EdblockView.Abstraction;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    private readonly EdblockVM edblockVM;
    public Edblock()
    {
        InitializeComponent();

        edblockVM = new EdblockVM();
        DataContext = edblockVM;
    }

    private void AddSymbol(object sender, MouseButtonEventArgs e)
    {
        if (sender is IFactorySymbolVM factorySymbolVM)
        {
            var blockSymbolVM = factorySymbolVM.CreateBlockSymbolVM(edblockVM);
            edblockVM.AddBlockSymbol(blockSymbolVM);
        }
    }
}