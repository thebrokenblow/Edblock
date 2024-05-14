using Edblock.SymbolsUI.Factories;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.PagesVM;
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