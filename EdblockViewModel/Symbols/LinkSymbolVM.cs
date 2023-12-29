using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class LinkSymbolVM : BlockSymbolVM
{
    private const int defaultWidth = 60;
    private const int defaultHeigth = 60;

    private const string defaultText = "Ссылка";
    private const string defaultColor = "#FF5761A8";

    public LinkSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        ScaleRectangles = _builderScaleRectangles
                .AddRightTopRectangle()
                .AddRightBottomRectangle()
                .AddLeftBottomRectangle()
                .AddLeftTopRectangle()
                .Build();

        TextFieldVM.Text = defaultText;

        Color = defaultColor;

        Width = defaultWidth;
        Height = defaultHeigth;

        SetWidth(Width);
        SetHeight(Height);
    }

    public override void SetHeight(int height)
    {
        SetSize(height);
    }

    public override void SetWidth(int width)
    {
        SetSize(width);
    }

    public void SetSize(int size)
    {
        Height = size;
        Width = size;

        var textFieldWidth = BlockSymbolModel.GetTextFieldWidth();
        var textFieldLeftOffset = BlockSymbolModel.GetTextFieldLeftOffset();

        var textFieldHeight = BlockSymbolModel.GetTextFieldHeight();
        var textFieldTopOffset = BlockSymbolModel.GetTextFieldTopOffset();

        TextFieldVM.Width = textFieldWidth;
        TextFieldVM.Height = textFieldHeight;

        TextFieldVM.LeftOffset = textFieldLeftOffset;
        TextFieldVM.TopOffset = textFieldTopOffset;

        ChangeCoordinateAuxiliaryElements();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolVM = GetType().BaseType?.ToString();

        var linkSymbolModel = new LinkSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color
        };

        return linkSymbolModel;
    }
}