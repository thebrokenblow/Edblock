using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface IFontSizeComponentVM
{
    List<double> FontSizes { get; }
    double SelectedFontSize { get; set; }

    void SetFontSize(IHasTextFieldVM selectedSymbolHasTextField);
}