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
        Width = width;
        TextField.Width = width;
        //BlockSymbolModel.Width = width;

        ChangeCoordinateAuxiliaryElements();
    }

    public override void SetHeight(int height)
    {
        Height = height;

        BlockSymbolModel.Height = height;
        TextField.Height = height;

        ChangeCoordinateAuxiliaryElements();
    }
}