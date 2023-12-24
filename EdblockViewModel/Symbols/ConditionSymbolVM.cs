using System.Windows;
using EdblockModel.Symbols;
using System.Windows.Media;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ConditionSymbolVM : BlockSymbolVM
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

    private const string defaultText = "Условеие";
    private const string defaultColor = "#FF60B2D3";
    private const int countPoints = 4;

    public ConditionSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Color = defaultColor;


        var textFieldWidth = BlockSymbolModel.GetTextFieldWidth();
        var textFieldLeftOffset = BlockSymbolModel.GetTextFieldLeftOffset();

        TextField.Width = textFieldWidth;
        TextField.LeftOffset = textFieldLeftOffset;

        var textFieldHeight = BlockSymbolModel.GetTextFieldHeight();
        var textFieldTopOffset = BlockSymbolModel.GetTextFieldTopOffset();

        TextField.Height = textFieldHeight;
        TextField.TopOffset = textFieldTopOffset;


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

    private void SetCoordinatePolygonPoints()
    {
        Points = new()
        {
            new Point(Width / 2, Height),
            new Point(Width, Height / 2),
            new Point(Width / 2, 0),
            new Point(0, Height / 2)
        };
    }
}