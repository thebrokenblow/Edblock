using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects;

public class FontFamilySubject(List<string> fontFamilies) : IFontFamilySubject
{
    public List<IObserverFontFamily> Observers { get; } = [];
    public List<string> FontFamilies { get; } = fontFamilies;

    private string? selectedFontFamily;
    public string? SelectedFontFamily
    { 
        get => selectedFontFamily;
        set
        {
            selectedFontFamily = value;
        }
    }

    public void NotifyObservers()
    {
        foreach (var observer in Observers)
        {
            observer.UpdateFontFamily();
        }
    }

    public void RegisterObserver(IObserverFontFamily observerFontFamily)
    {
        Observers.Add(observerFontFamily);
    }
}