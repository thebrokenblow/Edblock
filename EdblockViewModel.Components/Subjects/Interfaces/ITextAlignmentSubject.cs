using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Components.Subjects.Interfaces;

public interface ITextAlignmentSubject
{
    int IndexFormatAlign { get; set; }
    void RegisterObserver(IObserverTextAlignment observerTextAlignment);
    void NotifyObservers();
}