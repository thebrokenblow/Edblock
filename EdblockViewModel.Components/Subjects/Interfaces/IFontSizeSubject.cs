using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Components.Subjects.Interfaces;

public interface IFontSizeSubject<T>
{
    List<T> FontSizes { get; }
    T? SelectedFontSize { get; set; }

    void RegisterObserver(IObserverFontSize observerFontSize);
    void NotifyObservers();
}