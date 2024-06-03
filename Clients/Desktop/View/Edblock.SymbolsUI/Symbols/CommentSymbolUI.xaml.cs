using Edblock.SymbolsUI.Factories;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols.ComponentsCommentSymbolVM;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для CommentSymbolUI.xaml
/// </summary>
public partial class CommentSymbolUI : UserControl, IFactorySymbolViewModel
{
    public CommentSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM) =>
        new CommentSymbolVM(editorVM);
}