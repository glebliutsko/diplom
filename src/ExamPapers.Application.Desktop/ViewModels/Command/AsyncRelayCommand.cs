using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ExamPapers.Application.Desktop.ViewModels.Command;

public class AsyncRelayCommand : CommandBase, INotifyPropertyChanged
{
    private readonly Func<object?, Task> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public AsyncRelayCommand(Func<object?, Task> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    #region CurrentTask
    private Task? _currentTask;

    public Task? CurrentTask
    {
        get => _currentTask;
        private set => SetField(ref _currentTask, value);
    }
    #endregion

    public bool IsRunning => CurrentTask is { IsCompleted: false };
    public bool IsFailed => CurrentTask is { IsFaulted: true };

    public override bool CanExecute(object? parameter)
    {
        return !IsRunning && (_canExecute == null || _canExecute(parameter));
    }

    public override void Execute(object? parameter)
    {
        if (IsRunning)
            return;

        CurrentTask = ExecuteAsync(parameter);
        RaiseCanExecute();
    }

    private async Task ExecuteAsync(object? parameter)
    {
        if (IsRunning)
            return;

        try
        {
            await _execute(parameter);
        }
        finally
        {
            OnPropertyChanged(nameof(IsRunning));
            OnPropertyChanged(nameof(IsFailed));
            
            RaiseCanExecute();
        }
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    #endregion
}