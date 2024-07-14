using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects;

public class FormatTextSubject : IFormatTextSubject, INotifyPropertyChanged
{
    public enum SelectedFormatText
    {
        Bold,
        Italic,
        Underline,
        Normal,
        None,
    }

    public List<IObserverFormatText> Observers { get; } = [];

    private bool isTextBold;
    public bool IsTextBold 
    { 
        get => isTextBold;
        set
        {
            isTextBold = value;

            if (isTextBold && value)
            {
                isTextBold = false;
            }
            else
            {
                isTextBold = true;
            }

            OnPropertyChanged();

            if (isTextBold)
            {
                TextBold = SelectedFormatText.Bold;
            }
            else
            {
                TextBold = SelectedFormatText.Normal;
            }
        }
    }

    private bool isTextItalic;
    public bool IsTextItalic 
    {
        get => isTextItalic;
        set
        {
            isTextItalic = value;

            if (isTextItalic && value)
            {
                isTextItalic = false;
            }
            else
            {
                isTextItalic = true;
            }

            OnPropertyChanged();

            if (isTextItalic)
            {
                TextItalic = SelectedFormatText.Italic;
            }
            else
            {
                TextItalic = SelectedFormatText.Normal;
            }
        }
    }

    private bool isTextUnderline;
    public bool IsTextUnderline 
    {
        get => isTextUnderline;
        set
        {
            isTextUnderline = value;

            if (isTextUnderline && value)
            {
                isTextUnderline = false;
            }
            else
            {
                isTextUnderline = true;
            }

            OnPropertyChanged();

            if (isTextUnderline)
            {
                TextUnderline = SelectedFormatText.Underline;
            }
            else
            {
                TextUnderline = SelectedFormatText.None;
            }
        }
    }

    private SelectedFormatText textBold;
    public SelectedFormatText TextBold
    {
        get => textBold;
        set
        {
            textBold = value;
        }
    }

    private SelectedFormatText textItalic;
    public SelectedFormatText TextItalic
    {
        get => textItalic;
        set
        {
            textItalic = value;
        }
    }

    private SelectedFormatText textUnderline;
    public SelectedFormatText TextUnderline
    {
        get => textUnderline;
        set
        {
            textUnderline = value;
        }
    }

    public void NotifyObserversTextBold()
    {
        foreach (var observer in Observers)
        {
            observer.UpdateTextBold();
        }
    }

    public void NotifyObserversFormatItalic()
    {
        foreach (var observer in Observers)
        {
            observer.UpdateFormatItalic();
        }
    }

    public void NotifyObserversTextDecorations()
    {
        foreach (var observer in Observers)
        {
            observer.UpdateTextDecorations();
        }
    }

    public void RegisterObserver(IObserverFormatText observerFormatText)
    {
        Observers.Add(observerFormatText);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}