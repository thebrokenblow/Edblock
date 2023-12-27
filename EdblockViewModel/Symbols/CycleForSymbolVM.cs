using System.Windows;
using System.Windows.Media;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class CycleForSymbolVM : BlockSymbolVM, IHavePolygon
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

    private const string defaultText = "Цикл for";
    private const string defaultColor = "#FFC618";
    private const int sideProjection = 10;

    public CycleForSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Color = defaultColor;
        TextField.Text = defaultText;
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

        var cycleForSymbolModel = new CycleForSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color,
        };

        return cycleForSymbolModel;
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(sideProjection, 0),
            new Point(0, sideProjection),
            new Point(0, Height - sideProjection),
            new Point(sideProjection, Height),
            new Point(Width - sideProjection, Height),
            new Point(Width, Height - sideProjection),
            new Point(Width, sideProjection),
            new Point(Width - sideProjection, 0),
        };
    }
}
