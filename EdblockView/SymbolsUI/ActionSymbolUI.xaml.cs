using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.Symbols;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.PagesVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для ActionSymbolUI.xaml
/// </summary>
public partial class ActionSymbolUI : UserControl, IFactorySymbolVM
{
    public ActionSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        return new ActionSymbolVM(editorVM);
    }
}