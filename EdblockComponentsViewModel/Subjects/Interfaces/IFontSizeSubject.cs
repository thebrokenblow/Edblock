using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects.Interfaces;

public interface IFontSizeSubject<T>
{
    public List<T> FontSizes { get; }
    public T? SelectedFontSize { get; set; }

    void RegisterObserver(IObserverFontSize<T> observerFontSize);
    void NotifyObservers();
}