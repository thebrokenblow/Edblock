using EdblockComponentsViewModel.Subjects.Interfaces;

namespace EdblockComponentsViewModel.Observers.Interfaces;

public interface IObserverFontSize<T>
{
    IFontSizeSubject<T> FontSizeSubject { get; }
    void UpdateFontSize();
}