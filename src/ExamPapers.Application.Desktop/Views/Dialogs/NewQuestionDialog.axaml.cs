using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs.AnswersEditors;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class NewQuestionDialog : Window
{
    private IAnswerUserControl? _answerEditor;
    public QuestionRequest? Result { get; private set; } = null;
    
    public NewQuestionDialog()
    {
        InitializeComponent();

        TypeComboBox.ItemsSource = Enum.GetValues(typeof(QuestionTypeResponse));
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private async void AcceptButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ErrorTextBlock.Text = "";
        
        var questionText = QuestionTextBox.Text;
        var selectionType = (QuestionTypeResponse?)TypeComboBox.SelectedItem;
        
        if (string.IsNullOrEmpty(questionText) || _answerEditor == null || selectionType == null)
        {
            ErrorTextBlock.Text = "Заполните все поля";
            return;
        }
        
        var answers = _answerEditor.GetAnswers();
        if (answers.Count(x => x.IsCorrect) == 0)
        {
            ErrorTextBlock.Text = "У вопроса должен быть правильный ответ";
            return;
        }

        Result = new QuestionRequest
        {
            Text = questionText,
            Type = (QuestionTypeResponse)selectionType,
            Answers = answers
        };
        
        Close();
    }

    private void TypeComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var senderComboBox = (ComboBox)sender!;
        if (senderComboBox.SelectedItem == null)
            return;
        
        var selectionType = (QuestionTypeResponse)senderComboBox.SelectedItem;
        _answerEditor = selectionType switch
        {
            QuestionTypeResponse.Single => new AnswersSingleUserControl(),
            QuestionTypeResponse.Multiple => new AnswerUserControl(),
            QuestionTypeResponse.Open => new AnswerOpenUserControl(),
            _ => null
        };

        AnswersEditorContentControl.Content = _answerEditor;
    }
}