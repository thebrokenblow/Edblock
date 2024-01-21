using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleWhileEndSymbolUI.xaml
/// </summary>
public partial class CycleWhileEndSymbolUI : UserControl, IFactorySymbolVM
{
    public CycleWhileEndSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var cycleWhileEndSymbolVM = new CycleWhileEndSymbolVM(edblockVM);

        return cycleWhileEndSymbolVM;
    }
}