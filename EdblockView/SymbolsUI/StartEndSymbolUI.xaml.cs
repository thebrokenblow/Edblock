using EdblockView.Abstraction;
using EdblockViewModel.Symbols;
using EdblockViewModel;
using System.Windows.Controls;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для StartEndSymbolUI.xaml
/// </summary>
public partial class StartEndSymbolUI : UserControl, IFactorySymbolVM
{
    public StartEndSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var startEndSymbolVM = new StartEndSymbolVM(edblockVM);

        return startEndSymbolVM;
    }
}