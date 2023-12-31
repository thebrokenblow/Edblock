using System.Windows;
using EdblockViewModel;
using System.Windows.Input;
using EdblockView.Abstraction;
using EdblockViewModel.Symbols.CommentSymbolVMComponents;

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

    private void AddSymbolView(object sender, MouseButtonEventArgs e)
    {
        if (sender is IFactorySymbolVM factorySymbolVM)
        {
            var blockSymbolVM = factorySymbolVM.CreateBlockSymbolVM(edblockVM);

            var movableBlockSymbol = edblockVM.CanvasSymbolsVM.MovableBlockSymbol;

            if (movableBlockSymbol is not CommentSymbolVM)
            {
                edblockVM.AddBlockSymbol(blockSymbolVM);
            }
        }
    }
}