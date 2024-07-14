
using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects;

public class ColorSubject(string defaultColor)  : IColorSubject
{
    public List<IObserverColor> Observers { get; } = [];

    private string selectedColor = defaultColor;
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