using System.Windows.Controls;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsUI.Factories;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для ConditionSymbolUI.xaml
/// </summary>
public partial class ConditionSymbolUI : UserControl, IFactorySymbolViewModel
{
    public ConditionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new ConditionSymbolViewModel(editorViewModel);
}