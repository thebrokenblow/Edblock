using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.Symbols;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для InputOutputSymbolUI.xaml
/// </summary>
public partial class InputOutputSymbolUI : UserControl, IFactorySymbolVM
{
    public InputOutputSymbolUI()
    {
        InitializeComponent();
    }

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        return new InputOutputSymbolVM(editorVM);
    }
}