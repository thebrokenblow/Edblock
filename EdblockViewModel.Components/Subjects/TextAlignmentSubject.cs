using EdblockViewModel.Components.Observers.Interfaces;
using EdblockViewModel.Components.Subjects.Interfaces;

namespace EdblockViewModel.Components.Subjects;

public enum TextAlignment
{
    Left,
    Center,
    Right,
    Justify
}

public class TextAlignmentSubject : ITextAlignmentSubject
{
    public TextAlignmentSubject(int defaultIndexFormatAlig)
    {
        IndexFormatAlign = defaultIndexFormatAlig;
    }

    public List<IObserverTextAlignment> Observers { get; } = [];

    private int indexFormatAlign;
    public int IndexFormatAlign
    {
        get => indexFormatAlign;
        set
        {
            indexFormatAlign = value;
            SelectedTextAlignment = textAlignmentByIndex[indexFormatAlign];
            NotifyObservers();
        }
    }

    public TextAlignment SelectedTextAlignment { get; set; }

    private readonly Dictionary<int, TextAlignment> textAlignmentByIndex = new()
    {
        { 0, TextAlignment.Left },
        { 1, TextAlignment.Center },
        { 2, TextAlignment.Right },
        { 3, TextAlignment.Justify }
    };

    public void NotifyObservers()
    {
        foreach (var observers in Observers)
        {
            observers.UpdateTextAlignment();
        }
    }

    public void RegisterObserver(IObserverTextAlignment observerTextAlignment)
    {
        Observers.Add(observerTextAlignment);
    }
}