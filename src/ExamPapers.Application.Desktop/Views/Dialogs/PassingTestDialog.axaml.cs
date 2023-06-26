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

    private int _currentQuestionIndex = 0;
    private ISingleInputUserControl? _inputAnswer;

    public int CurrentScore { get; private set; }

    public PassingTestDialog(TestFullResponse test)
    {
        _currentTest = test;
        CurrentScore = 0;
        
        InitializeComponent();

        TitleTestTextBlock.Text = _currentTest.Title;
        ShowCurrentQuestion();
    }

    private void ShowCurrentQuestion()
    {
        var currentQuestion = _currentTest.Questions[_currentQuestionIndex];
        
        QuestionTextBlock.Text = _currentTest.Questions[_currentQuestionIndex].Text;

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
        var currentQuestion = _currentTest.Questions[_currentQuestionIndex];
        
        if (_inputAnswer?.IsCorrect() == true)
            CurrentScore += currentQuestion.Score ?? 1;
        
        _currentQuestionIndex++;
        if (_currentQuestionIndex >= _currentTest.Questions.Count)
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