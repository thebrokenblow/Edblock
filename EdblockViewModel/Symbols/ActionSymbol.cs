using EdblockModel.Symbols;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ActionSymbol : BlockSymbolVM
{
    private const string defaultText = "Действие";
    private const string defaultColor = "#FF52C0AA";

    public ActionSymbol(
        CanvasSymbolsVM canvasSymbolsVM, 
        ScaleAllSymbolVM scaleAllSymbolVM,
        CheckBoxLineGostVM checkBoxLineGostVM,
        FontFamilyControlVM fontFamilyControlVM,
        FontSizeControlVM fontSizeControlVM,
        TextAlignmentControlVM textAlignmentControlVM,
        FormatTextControlVM formatTextControlVM) : base(canvasSymbolsVM, scaleAllSymbolVM, checkBoxLineGostVM, fontFamilyControlVM, fontSizeControlVM, textAlignmentControlVM, formatTextControlVM)
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