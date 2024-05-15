using Edblock.SymbolsUI.Factories;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.PagesVM;
using EdblockViewModel.Symbols;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для SubroutineSymbolUI.xaml
/// </summary>
public partial class SubroutineSymbolUI : UserControl, IFactorySymbolViewModel
{
    public SubroutineSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM) =>
        new SubroutineSymbolVM(editorVM);
}