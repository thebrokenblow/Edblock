namespace Edblock.PagesViewModel.Components.TopSettingsPanel;

public class FontFamilyViewModel(List<FontFamilyViewModel/*BlockSymbolVM*/> selectedBlockSymbols)
{
    public List<string> FontFamilys { get; init; } =
        [
            "Times New Roman"
        ];

    public string? fontFamily;
    public string? FontFamily
    {
        get => fontFamily;
        set
        {
            fontFamily = value;
            SetFontFamily();
        }
    }

    //public void SetFontFamily(BlockSymbolVM selectedBlockSymbol) 
    //{
    //    if (selectedBlockSymbol is IHasTextFieldVM symbolHasTextFieldVM)
    //    {
    //        symbolHasTextFieldVM.TextFieldSymbolVM.FontFamily = fontFamily;
    //    }
    //}

    private void SetFontFamily()
    {
        //foreach (var selectedBlockSymbol in selectedBlockSymbols)
        //{
        //    SetFontFamily(selectedBlockSymbol);
        //}
    }
}