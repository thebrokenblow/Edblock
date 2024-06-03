using System.Collections.Generic;
using EdblockViewModel.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface IFontSizeComponentVM
{
    List<double> FontSizes { get; }
    double FontSize { get; set; }

    void SetFontSize(IHasTextFieldVM selectedSymbolHasTextField);
}