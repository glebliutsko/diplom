using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class TestingSessionUserControl : UserControl
{
    private readonly MainWindow _mainWindow;

    public TestingSessionUserControl(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        
        InitializeComponent();

        SessionItemsControl.ItemsSource = ExamApiClientKeeper.Client.GetMyTestingSessions().Result;
    }

    private void ShowSessionButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        var session = (TestSessionResponse)button.Tag;
        
        _mainWindow.ShowContent(new TestingSessionStudentResultUserControl(session));
    }
}