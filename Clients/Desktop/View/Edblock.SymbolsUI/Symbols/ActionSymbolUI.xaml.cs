using Edblock.SymbolsUI.Factories;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для ActionSymbolUI.xaml
/// </summary>
public partial class ActionSymbolUI : UserControl, IFactorySymbolViewModel
{
    public ActionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM) =>
        new ActionSymbolVM(editorVM);
}