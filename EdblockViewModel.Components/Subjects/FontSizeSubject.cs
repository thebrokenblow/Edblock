using System.Numerics;
using EdblockViewModel.Components.Observers.Interfaces;
using EdblockViewModel.Components.Subjects.Interfaces;

namespace EdblockViewModel.Components.Subjects;

public class FontSizeSubject<T> : IFontSizeSubject<T> where T : INumber<T>
{
    public List<T> FontSizes { get; } = [];
    public List<IObserverFontSize> Observers { get; } = [];

    private T? selectedFontSize;
    public T? SelectedFontSize
    {
        get => selectedFontSize;
        set
        {
            selectedFontSize = value;
            NotifyObservers();
        }
    }

    public FontSizeSubject(T minFontSize, T maxFontSize, T stepFontSize, T defaultFontSize)
    {
        for (T i = minFontSize; i <= maxFontSize; i += stepFontSize)
        {
            FontSizes.Add(i);
        }

        selectedFontSize = defaultFontSize;
    }

    public void NotifyObservers()
    {
        if (selectedFontSize is null)
        {
            return;
        }

        foreach (var observer in Observers)
        {
            observer.UpdateFontSize();
        }
    }

    public void RegisterObserver(IObserverFontSize observerFontSize)
    {
        Observers.Add(observerFontSize);
    }
}