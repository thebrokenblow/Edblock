using System;
using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

namespace EdblockView.SymbolsUI;

/// <summary>
/// Логика взаимодействия для VerticalConditionSymbolUI.xaml
/// </summary>
public partial class VerticalConditionSymbolUI : UserControl, IFactorySymbolVM
{
    private const int minCountLines = 2;
    private const int maxCountLines = 21;

    public VerticalConditionSymbolUI() =>
        InitializeComponent();

    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM)
    {
        int countLines = Convert.ToInt32(textBoxCountLines.Text);

        if (countLines < minCountLines || countLines >= maxCountLines)
        {
            throw new Exception($"Количество выходов должно быть {minCountLines} или больше и меньше {maxCountLines}");
        }

        return new VerticalConditionSymbolVM(editorVM, countLines); ;
    }
}