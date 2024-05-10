using System.Windows.Controls;
using Edblock.SymbolsUI.Factories;
using Edblock.SymbolsViewModel.Core;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleForSymbolUI.xaml
/// </summary>
public partial class CycleForSymbolUI : UserControl, IFactorySymbolViewModel
{
    public CycleForSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new CycleForSymbolViewModel(editorViewModel);
}