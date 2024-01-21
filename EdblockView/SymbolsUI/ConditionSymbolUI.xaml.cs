using EdblockViewModel;
using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel.Symbols;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для ConditionSymbolUI.xaml
/// </summary>
/// 
public partial class ConditionSymbolUI : UserControl, IFactorySymbolVM
{
    public ConditionSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var conditionSymbolVM = new ConditionSymbolVM(edblockVM);

        return conditionSymbolVM;
    }
}