using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.ComponentsVM;

public class TextAlignmentControlVM
{
    private string? textAlignment;
    private ListBoxItem? itemTextAlignment;
    public ListBoxItem? ItemTextAlignment
    {
        get => itemTextAlignment;
        set
        {
            itemTextAlignment = value;

            if (itemTextAlignment != null)
            {
                var packIconTextAlignment = (PackIcon)itemTextAlignment.Content;
                var kindTextAlignment = packIconTextAlignment.Kind;
                textAlignment = textAlignmentByIconKind[kindTextAlignment];
                SetFormatAlignment();
            }
        }
    }

    private readonly Dictionary<PackIconKind, string> textAlignmentByIconKind;
    private readonly List<BlockSymbolVM> _selectedBlockSymbols;

    public TextAlignmentControlVM(List<BlockSymbolVM> selectedBlockSymbols)
    {
        _selectedBlockSymbols = selectedBlockSymbols;

        textAlignmentByIconKind = new()
        {
            { PackIconKind.FormatAlignLeft, "Left" },
            { PackIconKind.FormatAlignJustify, "Justify" },
            { PackIconKind.FormatAlignCentre, "Center" },
            { PackIconKind.FormatAlignRight, "Right" }
        };

        var packIconTextAlignment = new PackIcon
        {
            Kind = PackIconKind.FormatAlignCentre
        };

        ItemTextAlignment = new()
        {
            Content = packIconTextAlignment,
        };
    }

    public void SetFormatAlignment(BlockSymbolVM selectedBlockSymbol)
    {
        selectedBlockSymbol.TextField.TextAlignment = textAlignment;
    }

    private void SetFormatAlignment()
    {
        foreach (var selectedBlockSymbol in _selectedBlockSymbols)
        {
            selectedBlockSymbol.TextField.TextAlignment = textAlignment;
        }
    }
}