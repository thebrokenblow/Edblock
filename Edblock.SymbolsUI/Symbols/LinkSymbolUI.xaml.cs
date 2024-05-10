using System.Windows.Controls;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsUI.Factories;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для LinkSymbolUI.xaml
/// </summary>
public partial class LinkSymbolUI : UserControl, IFactorySymbolViewModel
{
    public LinkSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new LinkSymbolViewModel(editorViewModel);
}