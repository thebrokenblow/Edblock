using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

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
        int countLines = Convert.ToInt32(textBoxCountLines.Text);

        if (countLines < 1 || countLines >= 20)
        {
            throw new Exception("Количество выходов дожно быть больше 1 и меньше 20");
        }

        var horizontalConditionSymbolVM = new HorizontalConditionSymbolVM(edblockVM, countLines);

        return horizontalConditionSymbolVM;
    }
}