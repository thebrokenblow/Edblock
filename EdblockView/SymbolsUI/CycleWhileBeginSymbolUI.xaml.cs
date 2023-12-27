using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.Symbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleWhileBeginSymbolUI.xaml
/// </summary>
public partial class CycleWhileBeginSymbolUI : UserControl, IFactorySymbolVM
{
    public CycleWhileBeginSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var cycleWhileBeginSymbolVM = new CycleWhileBeginSymbolVM(edblockVM);

        return cycleWhileBeginSymbolVM;
    }
}