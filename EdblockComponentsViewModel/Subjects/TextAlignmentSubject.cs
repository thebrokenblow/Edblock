﻿using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Observers.Interfaces;

namespace EdblockComponentsViewModel.Subjects;

public class TextAlignmentSubject : ITextAlignmentSubject
{
    public enum TextAlignment
    {
        Left, 
        Center, 
        Right, 
        Justify
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