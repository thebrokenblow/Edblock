using System;
using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для VerticalConditionSymbolUI.xaml
/// </summary>
public partial class VerticalConditionSymbolUI : UserControl, IFactorySymbolVM
{
    public VerticalConditionSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var verticalConditionSymbolVM = new VerticalConditionSymbolVM(edblockVM, Convert.ToInt32(countLines.Text));

        return verticalConditionSymbolVM;
    }
}