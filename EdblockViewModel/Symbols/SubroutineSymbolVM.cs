using System.Windows;
using System.Windows.Media;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class SubroutineSymbolVM : BlockSymbolVM, IHavePolygon
{
    private PointCollection? points;
    public PointCollection? Points
    {
        get => points;
        set
        {
            points = value;
            OnPropertyChanged();
        }
    }

    private int widthBorder;
    public int WidthBorder
    {
        get => widthBorder;
        set
        {
            widthBorder = value;
            OnPropertyChanged();
        }
    }

    private int heightBorder;
    public int HeightBorder
    {
        get => heightBorder;
        set
        {
            heightBorder = value;
            OnPropertyChanged();
        }
    }

    public const int leftOffsetBorder = 20;
    public static int LeftOffsetBorder
    {
        get => leftOffsetBorder;
    }

    private const string defaultText = "Подпрограмма";
    private const string defaultColor = "#FFBA64C8";

    public SubroutineSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Color = defaultColor;
        TextField.Text = defaultText;
    }

    public override void SetWidth(int width)
    {
        BlockSymbolModel.Width = width;

        WidthBorder = width - leftOffsetBorder * 2;

        var textFieldWidth = BlockSymbolModel.GetTextFieldWidth();
        var textFieldLeftOffset = BlockSymbolModel.GetTextFieldLeftOffset();

        TextField.Width = textFieldWidth;
        TextField.LeftOffset = textFieldLeftOffset;

        SetCoordinatePolygonPoints();
        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(int height)
    {
        HeightBorder = height;
        BlockSymbolModel.Height = height;

        var textFieldHeight = BlockSymbolModel.GetTextFieldHeight();
        var textFieldTopOffset = BlockSymbolModel.GetTextFieldTopOffset();

        TextField.Height = textFieldHeight;
        TextField.TopOffset = textFieldTopOffset;

        SetCoordinatePolygonPoints();
        ChangeCoordinateAuxiliaryElements();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolVM = GetType().BaseType?.ToString();

        var subroutineSymbolModel = new SubroutineSymbolModel
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color
        };

        return subroutineSymbolModel;
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(0, 0),
            new Point(0, Height),
            new Point(Width, Height),
            new Point(Width, 0)
        };
    }
}