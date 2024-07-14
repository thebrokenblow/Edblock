using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects.Interfaces;

public interface IFormatTextSubject
{
    public bool IsTextBold { get; set; }
    public bool IsTextItalic { get; set; }
    public bool IsTextUnderline { get; set; }
    void RegisterObserver(IObserverFormatText observerFormatText);
    void NotifyObserversTextBold();
    void NotifyObserversFormatItalic();
    void NotifyObserversTextDecorations();
}