using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.Symbols;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для SubroutineSymbolUI.xaml
/// </summary>
public partial class SubroutineSymbolUI : UserControl, IFactorySymbolVM
{
    public SubroutineSymbolUI() =>
         InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM) =>
        new SubroutineSymbolVM(editorVM);
}