using Edblock.SymbolsUI.Factories;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.PagesVM;
using EdblockViewModel.Symbols;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для CycleForSymbolUI.xaml
/// </summary>
public partial class CycleForSymbolUI : UserControl, IFactorySymbolViewModel
{
    public CycleForSymbolUI() =>
         InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM) =>
        new CycleForSymbolVM(editorVM);
}