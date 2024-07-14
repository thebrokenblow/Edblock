using EdblockViewModel.Components.Observers.Interfaces;

namespace EdblockViewModel.Components.Subjects.Interfaces;

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