namespace EdblockViewModel.ComponentsVM;

public class PopupBoxMenuVM
{
    public EdblockVM EdblockVM { get; set; }
    public ScaleAllSymbolVM ScaleAllSymbolVM { get; set; } 
    public CheckBoxLineGostVM CheckBoxLineGostVM { get; set; }

    public PopupBoxMenuVM(EdblockVM edblockVM)
    {
        EdblockVM = edblockVM;
        ScaleAllSymbolVM = new();
        CheckBoxLineGostVM = new();
    }
}