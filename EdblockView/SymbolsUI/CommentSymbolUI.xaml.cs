using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsCommentSymbolVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CommentSymbolUI.xaml
/// </summary>
public partial class CommentSymbolUI : UserControl, IFactorySymbolVM
{
    public CommentSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        return new CommentSymbolVM(editorVM);
    }
}