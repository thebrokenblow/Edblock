using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class StartEndSymbolVM : BlockSymbolVM
{
    private const int defaultWidth = 140;
    private const int defaultHeigth = 60;

    private const string defaultText = "Начало / Конец";
    private const string defaultColor = "#FFF25252";

    public StartEndSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        ScaleRectangles = _builderScaleRectangles
                .AddMiddleTopRectangle()
                .AddRightTopRectangle()
                .AddRightMiddleRectangle()
                .AddRightBottomRectangle()
                .AddMiddleBottomRectangle()
                .AddLeftBottomRectangle()
                .AddLeftMiddleRectangle()
                .AddLeftTopRectangle()
                .Build();

        Color = defaultColor;

        TextFieldVM.Text = defaultText;

        Width = defaultWidth;
        Height = defaultHeigth;

        SetWidth(Width);
        SetHeight(Height);
    }

    public override void SetWidth(int width)
    {
        Width = width;

        var textFieldWidth = BlockSymbolModel.GetTextFieldWidth();
        var textFieldLeftOffset = BlockSymbolModel.GetTextFieldLeftOffset();

        TextFieldVM.Width = textFieldWidth;
        TextFieldVM.LeftOffset = textFieldLeftOffset;

        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(int height)
    {
        Height = height;

        var textFieldHeight = BlockSymbolModel.GetTextFieldHeight();
        var textFieldTopOffset = BlockSymbolModel.GetTextFieldTopOffset();

        TextFieldVM.Height = textFieldHeight;
        TextFieldVM.TopOffset = textFieldTopOffset;

        ChangeCoordinateAuxiliaryElements();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolModel = GetType().BaseType?.ToString();

        var startEndSymbolModel = new StartEndSymbolModel()
        {
            Id = Id,
            NameSymbol = nameBlockSymbolModel,
            Color = Color,
        };

        return startEndSymbolModel;
    }
}