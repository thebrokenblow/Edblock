using System.Windows.Controls;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsUI.Factories;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для ActionSymbolUI.xaml
/// </summary>
public partial class ActionSymbolUI : UserControl, IFactorySymbolViewModel
{
    public ActionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new ActionSymbolViewModel(editorViewModel);
}