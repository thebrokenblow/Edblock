using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Prism.Commands;
using EdblockModel.SymbolsModel;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM;

public class TextFieldSymbolVM : INotifyPropertyChanged
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

    private double width;
    public double Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }


    private double height;
    public double Height
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
            _textFieldModel.Text = text;
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
            _textFieldModel.FontFamily = fontFamily;
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
            _textFieldModel.FontSize = fontSize;
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
            _textFieldModel.TextAlignment = textAlignment;
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
            _textFieldModel.FontWeight = fontWeight;
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
            _textFieldModel.FontStyle = fontStyle;
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
            _textFieldModel.TextDecorations = textDecorations;
            OnPropertyChanged();
        }
    }

    private double leftOffset;
    public double LeftOffset
    {
        get => leftOffset;
        set
        {
            leftOffset = value;
            OnPropertyChanged();
        }
    }

    private double topOffset;
    public double TopOffset
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
    private readonly TextFieldSymbolModel _textFieldModel;

    public TextFieldSymbolVM(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbolVM = blockSymbolVM;
        _blockSymbolVM.BlockSymbolModel.TextFieldSymbolModel = new();
        _textFieldModel = _blockSymbolVM.BlockSymbolModel.TextFieldSymbolModel;

        Cursor = Cursors.SizeAll;
        MouseDoubleClick = new(AddFocus);
        MouseLeftButtonDown = new(SetMovableSymbol);
    }

    public static void ChangeFocus(ObservableCollection<SymbolVM> Symbols)
    {
        foreach (var symbol in Symbols)
        {
            if (symbol is IHasTextFieldVM blockTextFieldVM)
            {
                var textFieldSymbol = blockTextFieldVM.TextFieldSymbolVM;

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
        Cursor = Cursors.SizeAll;

        _blockSymbolVM.SetMovableSymbol();
    }
}