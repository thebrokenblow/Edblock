using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    private int width;

    public int Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }


    private int height;

    public int Height
    {
        get => height;
        set
        {
            height = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand<BlockSymbolVM> DoubleClickedTextField { get; init; }
    public DelegateCommand<BlockSymbolVM> MouseMoveSymbol { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public TextField(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        MouseMoveSymbol = new(canvasSymbolsVM.MoveSymbol);
        DoubleClickedTextField = new(AddFocus);
    }

    private void AddFocus(BlockSymbolVM symbolViewModel)
    {
        _canvasSymbolsVM.Cursor = Cursors.IBeam;
        symbolViewModel.TextField.Cursor = Cursors.IBeam;
        symbolViewModel.TextField.Focus = true;
    }

    public static void ChangeFocus(ObservableCollection<SymbolVM> Symbols)
    {
        foreach (var symbol in Symbols)
        {
            if (symbol is BlockSymbolVM blockSymbol)
            {
                if (blockSymbol.TextField.Focus)
                {
                    blockSymbol.TextField.Focus = false;
                }
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}