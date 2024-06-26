﻿using EdblockViewModel.Core;

namespace EdblockViewModel.Services.Interfaces;

public interface INavigationService
{
    BaseViewModel? CurrentViewModel { get; }
    void NavigateTo<T>() where T : BaseViewModel;
}