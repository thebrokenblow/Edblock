using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.ComponentsVM;

public class CheckBoxLineGostVM : INotifyPropertyChanged
{
    private bool isDrawingLinesAccordingGOST;
    public bool IsDrawingLinesAccordingGOST 
    {
        get => isDrawingLinesAccordingGOST;
        set
        {
            isDrawingLinesAccordingGOST = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}