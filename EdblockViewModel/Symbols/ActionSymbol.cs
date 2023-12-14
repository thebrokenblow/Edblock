using EdblockModel.Symbols.Abstraction;
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

    public ActionSymbol(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolModel blockSymbolModel, string id) : base(canvasSymbolsVM, blockSymbolModel, id)
    {
        Color = defaultColor;

        TextField.Width = Width;
        TextField.Height = Height;
        TextField.Text = defaultText;
    }

    public override void SetWidth(int width)
    {
        if (width >= BlockSymbolModel.MinWidth)
        {
            BlockSymbolModel.SetWidth(width);

            TextField.Width = width;
            ChangeCoordinateAuxiliaryElements();
        }
    }

    public override void SetHeight(int height)
    {
        if (height >= BlockSymbolModel.MinHeight)
        {
            BlockSymbolModel.SetHeight(height);

            TextField.Height = height;
            ChangeCoordinateAuxiliaryElements();
        }
    }
}