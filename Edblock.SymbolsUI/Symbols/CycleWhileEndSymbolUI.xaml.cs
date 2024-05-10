using System.Windows.Controls;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsUI.Factories;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для CycleWhileEndSymbolUI.xaml
/// </summary>
public partial class CycleWhileEndSymbolUI : UserControl, IFactorySymbolViewModel
{
    public CycleWhileEndSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new CycleWhileEndSymbolViewModel(editorViewModel);
}