using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs.AnswersInput;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class PassingTestDialog : Window
{
    private readonly DistributionTestShortResponse _distributionTestShortResponse;
    private readonly TestFullResponse _currentTest;

    private int _currentQuestion = 0;
    private ISingleInputUserControl? _inputAnswer;

    public PassingTestDialog(TestFullResponse test)
    {
        _currentTest = test;
        
        InitializeComponent();

        TitleTestTextBlock.Text = _currentTest.Title;
        ShowCurrentQuestion();
    }

    private void ShowCurrentQuestion()
    {
        var currentQuestion = _currentTest.Questions[_currentQuestion];
        
        QuestionTextBlock.Text = _currentTest.Questions[_currentQuestion].Text;

        _inputAnswer = currentQuestion.Type switch
        {
            QuestionTypeResponse.Single => new SingleInputUserControl(currentQuestion),
            QuestionTypeResponse.Multiple => new MultipleInputUserControl(currentQuestion),
            QuestionTypeResponse.Open => new OpenInputUserControl(currentQuestion),
            _ => null
        };
        AnswerInputContentControl.Content = _inputAnswer;
    }

    private void NextQuestion()
    {
        _currentQuestion++;
        if (_currentQuestion >= _currentTest.Questions.Count)
        {
            FinishPassing();
            return;
        }

        ShowCurrentQuestion();
    }

    private void FinishPassing()
    {
        Close();
    }

    private void AnswerButton_OnClick(object? sender, RoutedEventArgs e)
    {
        NextQuestion();
    }
}