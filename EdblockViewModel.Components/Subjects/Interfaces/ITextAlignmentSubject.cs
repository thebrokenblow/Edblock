using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Components.Subjects.Interfaces;

public interface ITextAlignmentSubject
{
    int IndexFormatAlign { get; set; }
    TextAlignment SelectedTextAlignment { get; set; }
    void RegisterObserver(IObserverTextAlignment observerTextAlignment);
    void NotifyObservers();
}