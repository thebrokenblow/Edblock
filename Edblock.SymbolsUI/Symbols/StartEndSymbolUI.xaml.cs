using System.Windows.Controls;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsUI.Factories;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для StartEndSymbolUI.xaml
/// </summary>
public partial class StartEndSymbolUI : UserControl, IFactorySymbolViewModel
{
    public StartEndSymbolUI() =>
          InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel) => 
        new StartEndSymbolViewModel(editorViewModel);
}