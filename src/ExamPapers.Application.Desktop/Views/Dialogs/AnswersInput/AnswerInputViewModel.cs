using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersInput;

public class AnswerInputViewModel : INotifyPropertyChanged
{
    public AnswerInputViewModel(AnswerResponse answer)
    {
        _answer = answer;
    }
    
    #region IsSelected
    private bool _isSelected = false;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetField(ref _isSelected, value);
    }
    #endregion

    #region Answer
    private AnswerResponse _answer;
    public AnswerResponse Answer
    {
        get => _answer;
        private set => SetField(ref _answer, value);
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