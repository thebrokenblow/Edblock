using EdblockViewModel.Components.Observers.Interfaces;
using static EdblockViewModel.Components.Subjects.FormatTextSubject;

namespace EdblockViewModel.Components.Subjects.Interfaces;

public interface IFormatTextSubject
{
    List<IObserverFormatText> Observers { get; }
    public bool IsTextBold { get; set; }
    public bool IsTextItalic { get; set; }
    public bool IsTextUnderline { get; set; }
    public SelectedFormatText TextBold { get; set; }
    public SelectedFormatText TextItalic { get; set; }
    public SelectedFormatText TextUnderline { get; set; }
    void RegisterObserver(IObserverFormatText observerFormatText);
    void NotifyObserversTextBold();
    void NotifyObserversFormatItalic();
    void NotifyObserversTextDecorations();
}