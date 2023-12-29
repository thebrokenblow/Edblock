using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.Symbols;
using EdblockViewModel.Symbols.Abstraction;
using System.Windows.Controls;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для LinkSymbolUI.xaml
/// </summary>
public partial class LinkSymbolUI : UserControl, IFactorySymbolVM
{
    public LinkSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var linkSymbolVM = new LinkSymbolVM(edblockVM);

        return linkSymbolVM;
    }
}