using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class TestsUserControl : UserControl
{
    private readonly MainWindow _mainWindow;

    public TestsUserControl(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        
        InitializeComponent();

        LoadTests();
    }

    private void LoadTests()
    {
        TestsItemsControl.ItemsSource = GetAllTests().Result;
    }
    
    private async Task<TestShortResponse[]> GetAllTests()
    {
        return await ExamApiClientKeeper.Client.GetAllTests()
            .ConfigureAwait(false);
    }

    private void EditTestButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }

    private void DeleteTestButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }

    private async void CreateTestButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var newTestDialog = new NewTestDialog();
        await newTestDialog.ShowDialog(_mainWindow);
        
        if (newTestDialog.Result == null)
            return;

        await ExamApiClientKeeper.Client.CreateTest(newTestDialog.Result);

        LoadTests();
    }

    private async void SendTestButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button buttonSender)
            return;

        var clickedTest = (TestShortResponse)buttonSender.Tag!;
        
        var selectGroupOrUserDialog = new SelectGroupOrUserDialog();
        await selectGroupOrUserDialog.ShowDialog(_mainWindow);

        var session = new TestSessionRequest
        {
            Deadline = null
        };
        if (selectGroupOrUserDialog.StudentId != null)
        {
            await ExamApiClientKeeper.Client.DistributionTestForStudent(session, clickedTest.Id,
                (int)selectGroupOrUserDialog.StudentId);
        }
        else if (selectGroupOrUserDialog.GroupId != null)
        {
            await ExamApiClientKeeper.Client.DistributionTestForGroup(session, clickedTest.Id,
                (int)selectGroupOrUserDialog.GroupId);
        }
    }
}