using Edblock.PagesViewModel.Core;

namespace Edblock.PagesViewModel.Components.PopupBox;

public class LineGostViewModel : BaseViewModel
{
    private bool isDrawingLinesAccordingGOST;
    public bool IsDrawingLinesAccordingGOST
    {
        get => isDrawingLinesAccordingGOST;
        set
        {
            isDrawingLinesAccordingGOST = value;
            OnPropertyChanged();
        }
    }
}