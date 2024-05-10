namespace Edblock.PagesViewModel.Components.TopSettingsPanel;

public class FontSizeViewModel
{
    public List<double> FontSizes { get; init; }

    //public double fontSize;
    //public double FontSize
    //{
    //    get => fontSize;
    //    set
    //    {
    //        fontSize = value;
    //        SetFontSize();
    //    }
    //}

    //private readonly List<BlockSymbolVM> _selectedBlockSymbols;
    //public FontSizeControlViewModel(List<BlockSymbolVM> selectedBlockSymbols)
    //{
    //    _selectedBlockSymbols = selectedBlockSymbols;

    //    FontSizes = [];

    //    for (int fontSize = 2; fontSize < 51; fontSize++)
    //    {
    //        FontSizes.Add(fontSize);
    //    }
    //}

    //public void SetFontSize(BlockSymbolVM selectedBlockSymbol)
    //{
    //    if (selectedBlockSymbol is IHasTextFieldVM symbolHasTextFieldVM)
    //    {
    //        symbolHasTextFieldVM.TextFieldSymbolVM.FontSize = fontSize;
    //    }
    //}

    //private void SetFontSize()
    //{
    //    foreach (var selectedBlockSymbol in _selectedBlockSymbols)
    //    {
    //        SetFontSize(selectedBlockSymbol);
    //    }
    //}

}