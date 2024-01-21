using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleForSymbolUI.xaml
/// </summary>
public partial class CycleForSymbolUI : UserControl, IFactorySymbolVM
{
    public CycleForSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var cycleForSymbolVM = new CycleForSymbolVM(edblockVM);

        return cycleForSymbolVM;
    }
}