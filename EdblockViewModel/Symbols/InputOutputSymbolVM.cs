using System.Windows;
using System.Windows.Media;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class InputOutputSymbolVM : BlockSymbolVM, IHavePolygon
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

    private const string defaultText = "Ввод / Вывод";
    private const string defaultColor = "#FF008080";
    private const int sideProjection = 20;

    public InputOutputSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Color = defaultColor;
        TextField.Text = defaultText;

        SetCoordinatePolygonPoints();
    }

    public override void SetWidth(int width)
    {
        BlockSymbolModel.Width = width;

        var textFieldWidth = BlockSymbolModel.GetTextFieldWidth();
        var textFieldLeftOffset = BlockSymbolModel.GetTextFieldLeftOffset();

        TextField.Width = textFieldWidth;
        TextField.LeftOffset = textFieldLeftOffset;

        ChangeCoordinateAuxiliaryElements();

        SetCoordinatePolygonPoints();
    }

    public override void SetHeight(int height)
    {
        BlockSymbolModel.Height = height;

        var textFieldHeight = BlockSymbolModel.GetTextFieldHeight();
        var textFieldTopOffset = BlockSymbolModel.GetTextFieldTopOffset();

        TextField.Height = textFieldHeight;
        TextField.TopOffset = textFieldTopOffset;

        ChangeCoordinateAuxiliaryElements();

        SetCoordinatePolygonPoints();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolVM = GetType().BaseType?.ToString();

        var inputOutputSymbolModel = new InputOutputSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color
        };

        return inputOutputSymbolModel;
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(sideProjection, 0),
            new Point(0, Height),
            new Point(Width - sideProjection, Height),
            new Point(Width, 0)
        };
    }
}