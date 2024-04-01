using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.PagesVM;
using EdblockViewModel.Symbols;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для ConditionSymbolUI.xaml
/// </summary>
/// 
public partial class ConditionSymbolUI : UserControl, IFactorySymbolVM
{
    public ConditionSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        return new ConditionSymbolVM(editorVM);
    }
}