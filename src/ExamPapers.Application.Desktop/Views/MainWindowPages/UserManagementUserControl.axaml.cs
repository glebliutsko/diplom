using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class UserManagementUserControl : UserControl
{
    private readonly MainWindow _mainWindow;

    public UserManagementUserControl(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        InitializeComponent();

        UsersItemsControl.ItemsSource = GetAllUsers().Result;
    }

    private async Task<UserResponse[]> GetAllUsers()
    {
        return await ExamApiClientKeeper.Client.GetAllUsers()
            .ConfigureAwait(false);
    }

    private void CreateUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        new NewUserDialog().ShowDialog(_mainWindow);
    }
}