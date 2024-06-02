using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

public interface IFontFamilyComponentVM
{
    List<string> FontFamilys { get; }
    string? FontFamily { get; set; }

    public void SetFontFamily(IHasTextFieldVM selectedSymbolHasTextField);
}