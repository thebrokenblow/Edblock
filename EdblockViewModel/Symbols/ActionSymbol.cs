using EdblockModel.Symbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ActionSymbol : BlockSymbolVM
{
    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";
    public ActionSymbol(CanvasSymbolsVM canvasSymbolsVM) : base(canvasSymbolsVM)
    {
        Color = defaultColor;

        TextField.Width = Width;
        TextField.Height = Height;
        TextField.Text = defaultText;
    }

    public override void SetWidth(int width)
    {
        BlockSymbolModel.Width = width;
        TextField.Width = width;

        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(int height)
    {
        BlockSymbolModel.Height = height;

        TextField.Height = height;
        ChangeCoordinateAuxiliaryElements();
    }
}