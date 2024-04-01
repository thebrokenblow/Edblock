using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.Symbols;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleWhileEndSymbolUI.xaml
/// </summary>
public partial class CycleWhileEndSymbolUI : UserControl, IFactorySymbolVM
{
    public CycleWhileEndSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        return new CycleWhileEndSymbolVM(editorVM);
    }
}