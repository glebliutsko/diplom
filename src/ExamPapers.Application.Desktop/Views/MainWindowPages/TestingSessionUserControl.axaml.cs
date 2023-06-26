using Avalonia.Controls;
using Avalonia.Interactivity;

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
        
    }
}