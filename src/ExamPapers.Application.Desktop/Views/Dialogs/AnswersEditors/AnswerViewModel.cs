using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersEditors;

public class AnswerViewModel : INotifyPropertyChanged
{
    #region Text
    private string _text;

    public string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
    #endregion

    #region IsCorrect
    private bool _isCorrect;

    public bool IsCorrect
    {
        get => _isCorrect;
        set => SetField(ref _isCorrect, value);
    }
    #endregion
    
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