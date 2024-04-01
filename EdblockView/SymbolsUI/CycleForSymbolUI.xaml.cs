using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.Symbols;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

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

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM dditorVM)
    {
        return new CycleForSymbolVM(dditorVM);
    }
}