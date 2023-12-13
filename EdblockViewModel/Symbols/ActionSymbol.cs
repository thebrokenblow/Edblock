using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ActionSymbol : BlockSymbolVM
{
    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";
    public ActionSymbol(CanvasSymbolsVM canvasSymbolsVM) : base(canvasSymbolsVM)
    {
        Color = defaultColor;

        TextField.Text = defaultText;
        TextField.Width = Width;
        TextField.Height = Height;
    }

    public override void SetWidth(int width)
    {
        if (width >= BlockSymbolModel.MinWidth)
        {
            BlockSymbolModel.SetWidth(width);

            Width = width;
            TextField.Width = width;
            ChangeCoordinateAuxiliaryElements();
        }
    }

    public override void SetHeight(int height)
    {
        if (height >= BlockSymbolModel.MinHeight)
        {
            BlockSymbolModel.SetHeight(height);

            Height = height;
            TextField.Height = height;
            ChangeCoordinateAuxiliaryElements();
        }
    }
}