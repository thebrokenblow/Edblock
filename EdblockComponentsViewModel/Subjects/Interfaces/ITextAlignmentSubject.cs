using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects.Interfaces;

public interface ITextAlignmentSubject
{
    int IndexFormatAlign { get; set; }
    void RegisterObserver(IObserverTextAlignment observerTextAlignment);
    void NotifyObservers();
}