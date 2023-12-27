using System.Windows;
using System.Windows.Media;
using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class CycleWhileBeginSymbolVM : BlockSymbolVM, IHavePolygon
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

    private const string defaultText = "Начало цикла";
    private const string defaultColor = "#CCCCFF";
    private const int sideProjection = 10;

    public CycleWhileBeginSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Color = defaultColor;
        TextFieldVM.Text = defaultText;
    }

    public override void SetWidth(int width)
    {
        BlockSymbolModel.Width = width;

        var textFieldWidth = BlockSymbolModel.GetTextFieldWidth();
        var textFieldLeftOffset = BlockSymbolModel.GetTextFieldLeftOffset();

        TextFieldVM.Width = textFieldWidth;
        TextFieldVM.LeftOffset = textFieldLeftOffset;

        ChangeCoordinateAuxiliaryElements();

        SetCoordinatePolygonPoints();
    }

    public override void SetHeight(int height)
    {
        BlockSymbolModel.Height = height;

        var textFieldHeight = BlockSymbolModel.GetTextFieldHeight();
        var textFieldTopOffset = BlockSymbolModel.GetTextFieldTopOffset();

        TextFieldVM.Height = textFieldHeight;
        TextFieldVM.TopOffset = textFieldTopOffset;

        ChangeCoordinateAuxiliaryElements();

        SetCoordinatePolygonPoints();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolVM = GetType().BaseType?.ToString();

        var cycleWhileBeginSymbolModel = new CycleWhileBeginSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color,
        };

        return cycleWhileBeginSymbolModel;
    }

    public void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(0, Height),
            new Point(0, sideProjection),
            new Point(sideProjection, 0),
            new Point(Width - sideProjection, 0),
            new Point(Width, sideProjection),
            new Point(Width, Height),
        };
    }
}
