using System.Windows.Controls;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsUI.Factories;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols.SwitchCaseConditionSymbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для HorizontalConditionSymbolUI.xaml
/// </summary>
public partial class HorizontalConditionSymbolUI : UserControl, IFactorySymbolViewModel
{
    private const int minCountLines = 2;
    private const int maxCountLines = 21;

    public HorizontalConditionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel)
    {
        int countLines = Convert.ToInt32(textBoxCountLines.Text);

        if (countLines < minCountLines || countLines >= maxCountLines)
        {
            throw new Exception($"Количество выходов должно быть {minCountLines} или больше и меньше {maxCountLines}");
        }

        return new HorizontalConditionSymbolViewModel(editorViewModel, countLines);
    }
}