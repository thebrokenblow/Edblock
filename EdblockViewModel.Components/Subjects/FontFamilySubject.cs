using EdblockViewModel.Components.Subjects.Interfaces;
using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Components.Subjects;

public class FontFamilySubject(List<string> fontFamilies, string defaultFontFamilies) : IFontFamilySubject
{
    public List<IObserverFontFamily> Observers { get; } = [];
    public List<string> FontFamilies { get; } = fontFamilies;

    private string? selectedFontFamily = defaultFontFamilies;
    public string? SelectedFontFamily
    {
        get => selectedFontFamily;
        set
        {
            selectedFontFamily = value;
            NotifyObservers();
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