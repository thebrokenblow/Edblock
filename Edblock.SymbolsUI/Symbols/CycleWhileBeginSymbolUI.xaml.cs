using System.Windows.Controls;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsUI.Factories;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleWhileBeginSymbolUI.xaml
/// </summary>
public partial class CycleWhileBeginSymbolUI : UserControl, IFactorySymbolViewModel
{
    public CycleWhileBeginSymbolUI() => 
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new CycleWhileBeginSymbolViewModel(editorViewModel);
}