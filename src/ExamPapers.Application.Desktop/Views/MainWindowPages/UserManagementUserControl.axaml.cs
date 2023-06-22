using System.Linq;
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

        LoadUsers();
    }

    private void LoadUsers()
    {
        UsersItemsControl.ItemsSource = GetAllUsers().Result;
    }

    private async Task<UserResponse[]> GetAllUsers()
    {
        return await ExamApiClientKeeper.Client.GetAllUsers()
            .ConfigureAwait(false);
    }
    
    private async Task<SuccessResponse> CreateUser(NewUserRequest newUser)
    {
        return await ExamApiClientKeeper.Client.CreateUser(newUser)
            .ConfigureAwait(false);
    }
    
    private async void CreateUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var newUserDialog = new NewUserDialog();
        await newUserDialog.ShowDialog(_mainWindow);
        
        if (newUserDialog.Result == null)
            return;

        var result = await CreateUser(newUserDialog.Result);
        
        LoadUsers();
    }
}