using EdblockViewModel.Core;

namespace EdblockViewModel.Interfaces;

public interface INavigationServices
{
    ViewModel? CurrentView { get; }
    void NatigateTo<T>() where T : ViewModel;
}