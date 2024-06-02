using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

public interface IFontSizeComponentVM
{
    public List<double> FontSizes { get; }
    public double FontSize { get; set; }

    public void SetFontSize(IHasTextFieldVM selectedSymbolHasTextField);
}
