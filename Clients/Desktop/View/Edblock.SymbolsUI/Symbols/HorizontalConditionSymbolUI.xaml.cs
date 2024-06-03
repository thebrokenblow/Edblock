using Edblock.SymbolsUI.Factories;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для HorizontalConditionSymbolUI.xaml
/// </summary>
public partial class HorizontalConditionSymbolUI : UserControl, IFactorySymbolViewModel
{
    private const int minCountLines = 2;
    private const int maxCountLines = 21;

    public HorizontalConditionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM)
    {
        int countLines = Convert.ToInt32(textBoxCountLines.Text);

        if (countLines < minCountLines || countLines >= maxCountLines)
        {
            throw new Exception($"Количество выходов должно быть {minCountLines} или больше и меньше {maxCountLines}");
        }

        return new HorizontalConditionSymbolVM(editorVM, countLines);
    }
}