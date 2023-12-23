namespace EdblockViewModel.ComponentsVM;

public class PopupBoxMenuVM
{
    public ScaleAllSymbolVM ScaleAllSymbolVM { get; set; } 
    public CheckBoxLineGostVM CheckBoxLineGostVM { get; set; }

    public PopupBoxMenuVM()
    {
        ScaleAllSymbolVM = new();
        CheckBoxLineGostVM = new();
    }
}