using System.Windows.Controls;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsUI.Factories;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsViewModel.Symbols.ComponentsCommentSymbolVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CommentSymbolUI.xaml
/// </summary>
public partial class CommentSymbolUI : UserControl, IFactorySymbolViewModel
{
    public CommentSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new CommentSymbolViewModel(editorViewModel);
}