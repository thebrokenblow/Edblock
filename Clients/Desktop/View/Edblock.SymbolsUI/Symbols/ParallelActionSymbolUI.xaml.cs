using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;
using EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;
using System.Windows.Controls;

namespace Edblock.SymbolsUI.Symbols;

/// <summary>
/// Логика взаимодействия для ParallelActionSymbolUI.xaml
/// </summary>
public partial class ParallelActionSymbolUI : UserControl
{
    private const int minCountSymbolsIncoming = 1;
    private const int maxCountSymbolsIncoming = 21;

    private const int minCountSymbolsOutgoing = 1;
    private const int maxCountSymbolsOutgoing = 21;

    public ParallelActionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        var countSymbolsIncoming = Convert.ToInt32(сountSymbolsIncoming.Text);
        var countSymbolsOutgoing = Convert.ToInt32(сountSymbolsOutgoing.Text);

        if (countSymbolsIncoming < minCountSymbolsIncoming || countSymbolsIncoming >= maxCountSymbolsIncoming)
        {
            throw new Exception($"Количество входов должно быть {minCountSymbolsIncoming} или больше и меньше {maxCountSymbolsIncoming}");
        }

        if (countSymbolsOutgoing < minCountSymbolsOutgoing || countSymbolsOutgoing >= maxCountSymbolsOutgoing)
        {
            throw new Exception($"Количество выходов должно быть {minCountSymbolsOutgoing} или больше и меньше {maxCountSymbolsOutgoing}");
        }

        return new ParallelActionSymbolVM(editorVM, countSymbolsIncoming, countSymbolsOutgoing); ;
    }
}
