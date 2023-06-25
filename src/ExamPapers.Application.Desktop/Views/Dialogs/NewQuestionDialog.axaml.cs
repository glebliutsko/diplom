using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs.AnswersEditors;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class NewQuestionDialog : Window
{
    private object? _answerEditor;
    
    public NewQuestionDialog()
    {
        InitializeComponent();

        TypeComboBox.ItemsSource = Enum.GetValues(typeof(QuestionTypeResponse));
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void AcceptButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
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
            QuestionTypeResponse.Multiple => new AnswerMultipleUserControl(),
            QuestionTypeResponse.Open => new AnswerOpenUserControl(),
            _ => null
        };

        AnswersEditorContentControl.Content = _answerEditor;
    }
}