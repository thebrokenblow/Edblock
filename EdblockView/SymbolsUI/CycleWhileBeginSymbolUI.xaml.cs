using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols;

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