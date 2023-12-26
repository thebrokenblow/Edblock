using EdblockViewModel;
using System.Windows.Controls;
using EdblockView.Abstraction;
using EdblockViewModel.Symbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для InputOutputSymbolUI.xaml
/// </summary>
public partial class InputOutputSymbolUI : UserControl, IFactorySymbolVM
{
    public InputOutputSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var inputOutputSymbolVM = new InputOutputSymbolVM(edblockVM);

        return inputOutputSymbolVM;
    }
}