using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using System;
using System.Windows.Controls;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для HorizontalConditionSymbolUI.xaml
/// </summary>
public partial class HorizontalConditionSymbolUI : UserControl, IFactorySymbolVM
{
    public HorizontalConditionSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var horizontalConditionSymbolVM = new HorizontalConditionSymbolVM(edblockVM, Convert.ToInt32(countLines.Text));

        return horizontalConditionSymbolVM;
    }
}