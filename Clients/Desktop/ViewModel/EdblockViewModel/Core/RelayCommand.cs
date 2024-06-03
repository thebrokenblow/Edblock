using System;
using System.Windows.Input;

namespace EdblockViewModel.Core;

public class RelayCommand(Action<object?> execute, Predicate<object?> canExecute) : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter = null) =>
        canExecute(parameter);

    public void Execute(object? parameter = null) =>
        execute(parameter);
}