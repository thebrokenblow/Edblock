using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ConnectionPoints;
using EdblockViewModel.Symbols.ScaleRectangles;

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

    public DelegateCommand MouseDoubleClick { get; init; }
    public DelegateCommand MouseLeftButtonDown { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbolVM;
    private readonly BlockSymbolModel _blockSymbolModel;

    public TextField(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;

        _blockSymbolVM = blockSymbolVM;
        _blockSymbolModel = blockSymbolVM.BlockSymbolModel;

        Cursor = Cursors.SizeAll;

        MouseDoubleClick = new(AddFocus);
        MouseLeftButtonDown = new(SetMovableSymbol);
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

    private void AddFocus()
    {
        _canvasSymbolsVM.Cursor = Cursors.IBeam;
        Cursor = Cursors.IBeam;

        Focusable = true;
    }

    private void SetMovableSymbol()
    {
        ConnectionPoint.SetDisplayConnectionPoints(_blockSymbolVM.ConnectionPoints, false);
        ScaleRectangle.SetStateDisplay(_blockSymbolVM.ScaleRectangles, false);

        Cursor = Cursors.SizeAll;
        
        _canvasSymbolsVM.Cursor = Cursors.SizeAll;

        _canvasSymbolsVM.MovableBlockSymbol = _blockSymbolVM;
        _canvasSymbolsVM.SetCurrentRedrawLines(_blockSymbolVM);
    }
}