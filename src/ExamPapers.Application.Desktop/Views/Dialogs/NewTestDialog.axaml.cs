using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

class QuestionsInTestViewModel : INotifyPropertyChanged
{
    public QuestionsInTestViewModel(QuestionShortResponse question, int score)
    {
        Question = question;
        Score = score;
    }

    #region Score
    private int _score;

    public int Score
    {
        get => _score;
        set => SetField(ref _score, value);
    }
    #endregion

    public QuestionShortResponse Question { get; init; }

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

public partial class NewTestDialog : Window
{
    private ObservableCollection<QuestionsInTestViewModel> _questionsInTest;
    
    public TestRequest? Result { get; private set; }

    public NewTestDialog()
    {
        _questionsInTest = new();

        InitializeComponent();

        QuestionsListBox.ItemsSource = _questionsInTest;
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void AcceptButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ErrorTextBlock.Text = "";

        var title = TitleTextBox.Text;
        var description = DescriptionTextBox.Text;
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || _questionsInTest.Count == 0)
        {
            ErrorTextBlock.Text = "Заполните все поля";
            return;
        }

        Result = new TestRequest
        {
            Title = title,
            Description = description,
            Questions = _questionsInTest.Select(x => new QuestionInTestRequest
            {
                QuestionId = x.Question.Id,
                Score = x.Score
            }).ToList()
        };
        
        Close();
    }

    private void DeleteQuestionButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (QuestionsListBox.SelectedItem == null)
            return;
        
        _questionsInTest.Remove((QuestionsInTestViewModel)QuestionsListBox.SelectedItem);
    }

    private async void AddQuestionButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var selQuestionDialog = new SelectQuestionFromBankDialog();
        await selQuestionDialog.ShowDialog(this);
        if (selQuestionDialog.Result == null)
            return;

        _questionsInTest.Add(new QuestionsInTestViewModel(new QuestionShortResponse
        {
            Id = selQuestionDialog.Result.Id,
            Text = selQuestionDialog.Result.Text
        }, 1));
    }
}