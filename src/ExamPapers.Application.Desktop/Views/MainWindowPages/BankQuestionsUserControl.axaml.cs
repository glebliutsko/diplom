using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class BankQuestionsUserControl : UserControl
{
    private readonly MainWindow _mainWindow;

    public BankQuestionsUserControl(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        
        InitializeComponent();

        LoadQuestion();
    }

    private void LoadQuestion()
    {
        QuestionItemsControl.ItemsSource = GetAllUsers().Result;
    }

    private async Task<QuestionResponse[]> GetAllUsers()
    {
        return await ExamApiClientKeeper.Client.GetAllQuestion()
            .ConfigureAwait(false);
    }

    private void AddQuestionButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
}