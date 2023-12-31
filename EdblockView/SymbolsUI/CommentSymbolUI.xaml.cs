using EdblockView.Abstraction;
using EdblockViewModel;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.CommentSymbolVMComponents;
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