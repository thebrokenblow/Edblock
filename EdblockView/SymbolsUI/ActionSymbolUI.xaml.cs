using EdblockViewModel;
using EdblockView.Abstraction;
using System.Windows.Controls;
using EdblockViewModel.Symbols;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для ActionSymbolUI.xaml
/// </summary>
public partial class ActionSymbolUI : UserControl, IFactorySymbolVM
{
    public ActionSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var actionSymbolVM = new ActionSymbolVM(edblockVM);

        return actionSymbolVM;
    }
}