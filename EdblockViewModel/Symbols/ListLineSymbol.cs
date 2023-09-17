using System.Collections.ObjectModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ListLineSymbol : Symbol
{
    public ObservableCollection<LineSymbol> LineSymbols { get; init; }

    public ListLineSymbol()
    {
        LineSymbols = new();
    }
}