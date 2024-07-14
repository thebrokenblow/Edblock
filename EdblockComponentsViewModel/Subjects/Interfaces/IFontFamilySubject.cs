using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects.Interfaces;

public interface IFontFamilySubject
{
    List<string> FontFamilies { get; }
    string? SelectedFontFamily { get; set; }

    void RegisterObserver(IObserverFontFamily observerFontFamily);
    void NotifyObservers();
}