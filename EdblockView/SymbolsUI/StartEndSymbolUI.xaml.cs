using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.Symbols;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для StartEndSymbolUI.xaml
/// </summary>
public partial class StartEndSymbolUI : UserControl, IFactorySymbolVM
{
    public StartEndSymbolUI() =>
          InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM) =>
        new StartEndSymbolVM(editorVM);
}