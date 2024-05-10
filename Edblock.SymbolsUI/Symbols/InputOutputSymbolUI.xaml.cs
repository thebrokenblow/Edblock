using System.Windows.Controls;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsUI.Factories;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для InputOutputSymbolUI.xaml
/// </summary>
public partial class InputOutputSymbolUI : UserControl, IFactorySymbolViewModel
{
    public InputOutputSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) =>
        new InputOutputSymbolViewModel(editorViewModel);
}