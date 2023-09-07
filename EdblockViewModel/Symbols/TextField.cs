using Prism.Mvvm;
using Prism.Commands;
using EdblockViewModel.Symbols;
using System.Windows.Input;

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

    private Cursor cursor = Cursors.Hand;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
            SetProperty(ref cursor, value);
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