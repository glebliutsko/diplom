using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class SelectQuestionFromBankDialog : Window
{
    public QuestionResponse? Result { get; private set; }
    
    public SelectQuestionFromBankDialog()
    {
        InitializeComponent();

        QuestionListBox.ItemsSource = ExamApiClientKeeper.Client.GetAllQuestion().Result;
    }

    private void SelectButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (QuestionListBox.SelectedItem == null)
            return;

        var selQuestion = (QuestionResponse)QuestionListBox.SelectedItem;
        Result = selQuestion;
        Close();
    }
}