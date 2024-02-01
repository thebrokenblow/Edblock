using System;
using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для ParallelActionSymbolUI.xaml
/// </summary>
public partial class ParallelActionSymbolUI : UserControl, IFactorySymbolVM
{
    public ParallelActionSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var countSymbolsIncoming = Convert.ToInt32(CountSymbolsIncoming.Text);
        var countSymbolsOutgoing = Convert.ToInt32(CountSymbolsOutgoing.Text);

        var parallelActionSymbolVM = new ParallelActionSymbolVM(edblockVM, countSymbolsIncoming, countSymbolsOutgoing);

        return parallelActionSymbolVM;
    }
}