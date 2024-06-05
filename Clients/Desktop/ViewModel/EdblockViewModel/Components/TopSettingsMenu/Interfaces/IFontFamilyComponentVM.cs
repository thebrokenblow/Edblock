using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface IFontFamilyComponentVM
{
    List<string> FontFamilys { get; }
    string? FontFamily { get; set; }

    void SetFontFamily(IHasTextFieldVM selectedSymbolHasTextField);
}