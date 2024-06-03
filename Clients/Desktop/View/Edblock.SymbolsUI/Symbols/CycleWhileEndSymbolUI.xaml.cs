using Edblock.SymbolsUI.Factories;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для CycleWhileEndSymbolUI.xaml
/// </summary>
public partial class CycleWhileEndSymbolUI : UserControl, IFactorySymbolViewModel
{
    public CycleWhileEndSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM) =>
        new CycleWhileEndSymbolVM(editorVM);
}