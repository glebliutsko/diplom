using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class StudentTestingUserControl : UserControl
{
    private readonly MainWindow _mainWindow;

    public StudentTestingUserControl(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        InitializeComponent();

        TestsItemsControl.ItemsSource = ExamApiClientKeeper.Client.GetMeAvailableTesting().Result;
    }

    private async void GetTestedButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button buttonSender)
            return;

        var selectedTest = (DistributionTestShortResponse)buttonSender.Tag!;
        var selectedFullTest = await ExamApiClientKeeper.Client.GetTestFull(selectedTest.TestId);
        
        var passingTestDialog = new PassingTestDialog(selectedFullTest);
        await passingTestDialog.ShowDialog(_mainWindow);
    }
}