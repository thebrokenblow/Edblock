using System.Windows.Controls;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;
using Edblock.SymbolsUI.Factories;
using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для VerticalConditionSymbolUI.xaml
/// </summary>
public partial class VerticalConditionSymbolUI : UserControl, IFactorySymbolViewModel
{
    private const int minCountLines = 2;
    private const int maxCountLines = 21;

    public VerticalConditionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM)
    {
        int countLines = Convert.ToInt32(textBoxCountLines.Text);

        if (countLines < minCountLines || countLines >= maxCountLines)
        {
            throw new Exception($"Количество выходов должно быть {minCountLines} или больше и меньше {maxCountLines}");
        }

        return new VerticalConditionSymbolVM(editorVM, countLines); ;
    }
}