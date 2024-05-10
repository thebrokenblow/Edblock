using Edblock.PagesViewModel.Pages;

namespace Edblock.PagesViewModel.Components.PopupBox;

public class PopupBoxMenuViewModel(EditorViewModel editorViewModel)
{
    public EditorViewModel EditorViewModel { get; } = editorViewModel;
    public ScaleAllSymbolViewModel ScaleAllSymbolViewModel { get; } = new();
    public LineGostViewModel LineGostViewModel { get; } = new();
}