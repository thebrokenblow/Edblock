using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using EdblockModel.SymbolsModel;
using EdblockViewModel.ComponentsVM;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using EdblockViewModel.Symbols.ScaleRectangles;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols;

public class TextFieldVM : INotifyPropertyChanged
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
            _blockSymbolModel.TextFieldModel.Text = text;
            OnPropertyChanged();
        }
    }

    private string? fontFamily;
    public string? FontFamily
    {
        get => fontFamily;
        set
        {
            fontFamily = value;
            _blockSymbolModel.TextFieldModel.FontFamily = fontFamily;
            OnPropertyChanged();
        }
    }

    private double fontSize;
    public double FontSize
    {
        get => fontSize;
        set
        {
            fontSize = value;
            _blockSymbolModel.TextFieldModel.FontSize = fontSize;
            OnPropertyChanged();
        }
    }

    private string? textAlignment;
    public string? TextAlignment 
    {
        get => textAlignment;
        set
        {
            textAlignment = value;
            _blockSymbolModel.TextFieldModel.TextAlignment = textAlignment;
            OnPropertyChanged();
        }
    }

    private string? fontWeight;
    public string? FontWeight 
    {
        get => fontWeight;
        set
        {
            fontWeight = value;
            _blockSymbolModel.TextFieldModel.FontWeight = fontWeight;
            OnPropertyChanged();
        }
    }

    private string? fontStyle;
    public string? FontStyle
    {
        get => fontStyle;
        set
        {
            fontStyle = value;
            _blockSymbolModel.TextFieldModel.FontStyle = fontStyle;
            OnPropertyChanged();
        }
    }

    private string? textDecorations;
    public string? TextDecorations
    {
        get => textDecorations;
        set
        {
            textDecorations = value;
            _blockSymbolModel.TextFieldModel.TextDecorations = textDecorations;
            OnPropertyChanged();
        }
    }

    private int leftOffset;
    public int LeftOffset
    {
        get => leftOffset;
        set
        {
            leftOffset = value;
            OnPropertyChanged();
        }
    }

    private int topOffset;
    public int TopOffset
    {
        get => topOffset;
        set
        {
            topOffset = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand MouseDoubleClick { get; init; }
    public DelegateCommand MouseLeftButtonDown { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbolVM;
    private readonly BlockSymbolModel _blockSymbolModel;

    public TextFieldVM(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM)
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