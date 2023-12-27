using EdblockModel.SymbolsModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ActionSymbolVM : BlockSymbolVM
{
    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";

    public ActionSymbolVM(EdblockVM edblockVM) : base(edblockVM)
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
    }

    public override void SetHeight(int height)
    {
        BlockSymbolModel.Height = height;

        var textFieldHeight = BlockSymbolModel.GetTextFieldHeight();
        var textFieldTopOffset = BlockSymbolModel.GetTextFieldTopOffset();

        TextFieldVM.Height = textFieldHeight;
        TextFieldVM.TopOffset = textFieldTopOffset;

        ChangeCoordinateAuxiliaryElements();
    }

    public override BlockSymbolModel CreateBlockSymbolModel()
    {
        var nameBlockSymbolVM = GetType().BaseType?.ToString();

        var actionSymbolModel = new ActionSymbolModel
        {
            Id = Id,
            NameSymbol = nameBlockSymbolVM,
            Color = Color
        };

        return actionSymbolModel;
    }
}