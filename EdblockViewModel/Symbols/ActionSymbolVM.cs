using EdblockModel.Symbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ActionSymbolVM : BlockSymbolVM
{
    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";

    public ActionSymbolVM(EdblockVM edblockVM) : base(edblockVM)
    {
        Color = defaultColor;

        TextField.Width = Width;
        TextField.Height = Height;

        TextField.Text = defaultText;
    }

    public override void SetWidth(int width)
    {
        TextField.Width = width;
        BlockSymbolModel.Width = width;

        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(int height)
    {
        TextField.Height = height;
        BlockSymbolModel.Height = height;

        ChangeCoordinateAuxiliaryElements();
    }
}