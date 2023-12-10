using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class TextField : INotifyPropertyChanged
{
    private bool focusable = false;
    public bool Focusable
    {
        get => focusable;
        set
        {
            focusable = value;
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

    private string? text;
    public string? Text
    {
        get => text;
        set
        {
            text = value;
            _blockSymbolModel.Text = text;
            OnPropertyChanged();
        }
    }

    public DelegateCommand<BlockSymbolVM> MouseDoubleClick { get; init; }
    public DelegateCommand<BlockSymbolVM> MouseLeftButtonDown { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolModel _blockSymbolModel;

    public TextField(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolModel blockSymbolModel)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbolModel = blockSymbolModel;

        MouseLeftButtonDown = new(canvasSymbolsVM.SetMovableSymbol);
        MouseDoubleClick = new(AddFocus);
    }

    public static void ChangeFocus(ObservableCollection<SymbolVM> Symbols)
    {
        foreach (var symbol in Symbols)
        {
            if (symbol is BlockSymbolVM blockSymbol)
            {
                var textFieldSymbol = blockSymbol.TextField;
                if (textFieldSymbol.Focusable)
                {
                    textFieldSymbol.Focusable = false;
                }
            }
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    private void AddFocus(BlockSymbolVM symbolViewModel)
    {
        _canvasSymbolsVM.Cursor = Cursors.IBeam;
        symbolViewModel.TextField.Cursor = Cursors.IBeam;
        symbolViewModel.TextField.Focusable = true;
    }
}