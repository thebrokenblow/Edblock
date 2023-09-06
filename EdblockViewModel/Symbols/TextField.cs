using Prism.Mvvm;
using Prism.Commands;
using EdblockViewModel.Symbols;

namespace MVVM.ViewModel.SymbolsViewModel;

public class TextField : BindableBase
{
    private bool focus = false;
    public bool Focus
    {
        get => focus;
        set
        {
            focus = value;
            SetProperty(ref focus, value);
        }
        
    }

    public DelegateCommand<Symbol> DoubleClickedTextField { get; init; }

    public TextField()
    {
        DoubleClickedTextField = new(AddFocus);
    }

    private void AddFocus(Symbol symbolViewModel)
    {
        symbolViewModel.Focus = true;
    }
}