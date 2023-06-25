using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersEditors;

public partial class AnswerOpenUserControl : UserControl, IAnswerUserControl
{
    private ObservableCollection<AnswerViewModel> _answers;
    
    public AnswerOpenUserControl()
    {
        _answers = new();
        
        InitializeComponent();

        AnswersItemsControl.ItemsSource = _answers;
    }

    private void AddAnswerButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _answers.Add(new AnswerViewModel {IsCorrect = true});
    }
    
    public List<AnswerResponse> GetAnswers()
    {
        return _answers.Select(x => new AnswerResponse { Text = x.Text, IsCorrect = x.IsCorrect }).ToList();
    }
}