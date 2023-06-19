using System;
using System.Windows.Input;
using Avalonia.Threading;

namespace ExamPapers.Application.Desktop.ViewModels.Command;

public abstract class CommandBase : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public abstract bool CanExecute(object? parameter);

    public abstract void Execute(object? parameter);
    
    public void RaiseCanExecute()
    {
        var handler = CanExecuteChanged;

        if (handler != null)
        {
            Dispatcher.UIThread.Post(delegate { handler.Invoke(this, EventArgs.Empty); });
        }
    }
}