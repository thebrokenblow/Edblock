using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class StartEndSymbolVM : BlockSymbolVM
{
    private const string defaultText = "Начало / Конец";
    private const string defaultColor = "#FFF25252";

    public StartEndSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Color = defaultColor;
        TextFieldVM.Text = defaultText;
    }

    public override void SetWidth(int width)
    {
        TextFieldVM.Width = width;
        BlockSymbolModel.Width = width;

        var textFieldWidth = BlockSymbolModel.GetTextFieldWidth();
        var textFieldLeftOffset = BlockSymbolModel.GetTextFieldLeftOffset();

        TextFieldVM.Width = textFieldWidth;
        TextFieldVM.LeftOffset = textFieldLeftOffset;

        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(int height)
    {
        TextFieldVM.Height = height;
        BlockSymbolModel.Height = height;

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