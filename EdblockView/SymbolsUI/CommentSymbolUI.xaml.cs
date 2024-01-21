using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsCommentSymbolVM;
using System.Windows.Controls;

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

    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM)
    {
        var commentSymbolVM = new CommentSymbolVM(edblockVM);

        return commentSymbolVM;
    }
}