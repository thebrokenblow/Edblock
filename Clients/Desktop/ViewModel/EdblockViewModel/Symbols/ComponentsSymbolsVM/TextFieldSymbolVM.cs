using System.Windows.Input;
using Prism.Commands;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Core;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM;

public class TextFieldSymbolVM : ObservableObject
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
            TextFieldModel.Text = text;
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
            TextFieldModel.FontFamily = fontFamily;
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
            TextFieldModel.FontSize = fontSize;
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
            TextFieldModel.TextAlignment = textAlignment;
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
            TextFieldModel.FontWeight = fontWeight;
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
            TextFieldModel.FontStyle = fontStyle;
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
            TextFieldModel.TextDecorations = textDecorations;
            OnPropertyChanged();
        }
    }

    private string? verticalAlign;
    public string? VerticalAlign 
    {
        get => verticalAlign;
        set
        {
            verticalAlign = value;
            TextFieldModel.VerticalAlign = verticalAlign;
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

    public DelegateCommand MouseDoubleClick { get; }
    public DelegateCommand MouseLeftButtonDown { get; }

    private readonly ICanvasSymbolsComponentVM _canvasSymbolsVM;
    private readonly BlockSymbolVM _blockSymbolVM;
    public TextFieldSymbolModel TextFieldModel { get; set; }

    public TextFieldSymbolVM(ICanvasSymbolsComponentVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _blockSymbolVM = blockSymbolVM;

        _blockSymbolVM.BlockSymbolModel.TextFieldSymbolModel = new();

        TextFieldModel = _blockSymbolVM.BlockSymbolModel.TextFieldSymbolModel;

        Cursor = Cursors.SizeAll;
        MouseDoubleClick = new(AddFocus);
        MouseLeftButtonDown = new(SetMovableSymbol);
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