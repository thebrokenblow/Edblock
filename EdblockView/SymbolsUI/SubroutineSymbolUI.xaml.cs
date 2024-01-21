using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для SubroutineSymbolUI.xaml
/// </summary>
public partial class SubroutineSymbolUI : UserControl, IFactorySymbolVM
{
    public SubroutineSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var subroutineSymbolVM = new SubroutineSymbolVM(edblockVM);

        return subroutineSymbolVM;
    }
}