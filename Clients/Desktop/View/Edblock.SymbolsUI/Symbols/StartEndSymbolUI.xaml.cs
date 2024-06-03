using Edblock.SymbolsUI.Factories;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для StartEndSymbolUI.xaml
/// </summary>
public partial class StartEndSymbolUI : UserControl, IFactorySymbolViewModel
{
    public StartEndSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM) =>
        new StartEndSymbolVM(editorVM);
}