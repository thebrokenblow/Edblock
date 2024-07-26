using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Components.Subjects.Interfaces;

public interface IColorSubject
{
    List<IObserverColor> Observers { get; }
    string? SelectedColor { get; set; }

    void RegisterObserver(IObserverColor observerColor);
    void NotifyObservers();
}