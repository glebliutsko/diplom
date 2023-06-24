using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class BankQuestionsUserControl : UserControl
{
    private readonly MainWindow _mainWindow;

    public BankQuestionsUserControl(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        
        InitializeComponent();
    }

    private void AddQuestionButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
}