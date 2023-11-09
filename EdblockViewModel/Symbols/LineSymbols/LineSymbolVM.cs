using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.LineSymbols;

public class LineSymbolVM : SymbolVM
{
    private int x1;
    public int X1
    {
        get => x1;
        set
        {
            x1 = value;
            OnPropertyChanged();
        }
    }

    private int y1;
    public int Y1
    {
        get => y1;
        set
        {
            y1 = value;
            OnPropertyChanged();
        }
    }

    private int x2;
    public int X2
    {
        get => x2;
        set
        {
            x2 = value;
            OnPropertyChanged();
        }
    }

    private int y2;

    public int Y2
    {
        get => y2;
        set
        {
            y2 = value;
            OnPropertyChanged();
        }
    }
}