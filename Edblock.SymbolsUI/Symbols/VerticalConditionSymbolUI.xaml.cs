using System.Windows.Controls;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsUI.Factories;
using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsViewModel.Symbols.SwitchCaseConditionSymbols;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для VerticalConditionSymbolUI.xaml
/// </summary>
public partial class VerticalConditionSymbolUI : UserControl, IFactorySymbolViewModel
{
    private const int minCountLines = 2;
    private const int maxCountLines = 21;

    public VerticalConditionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel)
    {
        int countLines = Convert.ToInt32(textBoxCountLines.Text);

        if (countLines < minCountLines || countLines >= maxCountLines)
        {
            throw new Exception($"Количество выходов должно быть {minCountLines} или больше и меньше {maxCountLines}");
        }

        return new VerticalConditionSymbolViewModel(editorViewModel, countLines); ;
    }
}