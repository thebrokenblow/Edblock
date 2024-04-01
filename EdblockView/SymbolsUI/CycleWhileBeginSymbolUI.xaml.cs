using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.Symbols;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleWhileBeginSymbolUI.xaml
/// </summary>
public partial class CycleWhileBeginSymbolUI : UserControl, IFactorySymbolVM
{
    public CycleWhileBeginSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        return new CycleWhileBeginSymbolVM(editorVM);
    }
}