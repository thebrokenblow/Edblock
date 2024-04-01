using System;
using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsParallelActionSymbolVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для ParallelActionSymbolUI.xaml
/// </summary>
public partial class ParallelActionSymbolUI : UserControl, IFactorySymbolVM
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