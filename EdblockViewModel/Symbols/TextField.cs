using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols;

public class TextField : INotifyPropertyChanged
{
    private bool focus = false;
    public bool Focus
    {
        get => focus;
        set
        {
            focus = value;
            OnPropertyChanged();
        }
    }

    private Cursor cursor = Cursors.Arrow;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand<Symbol> DoubleClickedTextField { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public TextField(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        DoubleClickedTextField = new(AddFocus);
    }

    private void AddFocus(Symbol symbolViewModel)
    {
        _canvasSymbolsVM.Cursor = Cursors.IBeam;
        symbolViewModel.TextField.Cursor = Cursors.IBeam;
        symbolViewModel.TextField.Focus = true;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}