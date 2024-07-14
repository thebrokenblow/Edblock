using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects.Interfaces;

public interface IColorSubject
{
    string SelectedColor { get; set; }

    void RegisterObserver(IObserverColor observerColor);
    void NotifyObservers();
}