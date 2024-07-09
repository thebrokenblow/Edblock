using System.Numerics;
using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects;

public class FontSizeSubject<T> : IFontSizeSubject<T> where T : INumber<T>
{
    public List<T> FontSizes { get; } = [];
    public List<IObserverFontSize<T>> Observers { get; } = [];

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

    public FontSizeSubject(T minFontSize, T maxFontSize, T stepFontSize)
    {
        for (T i = minFontSize; i <= maxFontSize; i += stepFontSize)
        {
            FontSizes.Add(i);
        }

        selectedFontSize = minFontSize;
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

    public void RegisterObserver(IObserverFontSize<T> observerFontSize)
    {
        Observers.Add(observerFontSize);
    }
}