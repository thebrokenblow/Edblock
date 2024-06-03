using Edblock.SymbolsUI.Factories;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для LinkSymbolUI.xaml
/// </summary>
public partial class LinkSymbolUI : UserControl, IFactorySymbolViewModel
{
    public LinkSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM) =>
        new LinkSymbolVM(editorVM);
}