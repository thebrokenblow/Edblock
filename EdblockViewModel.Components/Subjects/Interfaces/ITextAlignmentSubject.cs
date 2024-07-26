using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Components.Subjects.Interfaces;

public interface ITextAlignmentSubject
{
    List<IObserverTextAlignment> Observers { get; }
    int IndexFormatAlign { get; set; }
    TextAlignment SelectedTextAlignment { get; set; }
    void RegisterObserver(IObserverTextAlignment observerTextAlignment);
    void NotifyObservers();
}