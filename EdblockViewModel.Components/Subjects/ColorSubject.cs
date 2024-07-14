using EdblockViewModel.Components.Observers.Interfaces;
using EdblockViewModel.Components.Subjects.Interfaces;

namespace EdblockViewModel.Components.Subjects;

public class ColorSubject() : IColorSubject
{
    public List<IObserverColor> Observers { get; } = [];

    private string selectedColor;
    public string SelectedColor
    {
        get => selectedColor;
        set
        {
            selectedColor = value;
            NotifyObservers();
        }
    }

    public void NotifyObservers()
    {
        foreach (var observer in Observers)
        {
            observer.UpdateColor();
        }
    }

    public void RegisterObserver(IObserverColor observerColor)
    {
        Observers.Add(observerColor);
    }
}