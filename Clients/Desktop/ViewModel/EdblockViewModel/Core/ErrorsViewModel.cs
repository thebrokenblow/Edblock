using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.Collections;

namespace EdblockViewModel.Core;

public class ErrorsViewModel : INotifyDataErrorInfo
{
    public bool HasErrors => propertyErrors.Count != 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    private readonly Dictionary<string, List<string>> propertyErrors = [];


    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName == null || !propertyErrors.TryGetValue(propertyName, out List<string>? value))
        {
            return Enumerable.Empty<string>();
        }

        return value;
    }

    public void AddError(string propertyName, string errorMessage)
    {
        if (!propertyErrors.TryGetValue(propertyName, out List<string>? errors))
        {
            errors = [];
            propertyErrors.Add(propertyName, errors);
        }

        errors.Add(errorMessage);
        OnErrorsChanged(propertyName);
    }

    public void ClearErrors(string propertyName)
    {
        if (propertyErrors.Remove(propertyName))
        {
            OnErrorsChanged(propertyName);
        }
    }

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}