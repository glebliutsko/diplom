using Avalonia.Controls;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class UserManagementUserControl : UserControl
{
    public UserManagementUserControl()
    {
        InitializeComponent();

        UsersItemsControl.ItemsSource = GetAllUsers().Result;
    }

    private async Task<UserResponse[]> GetAllUsers()
    {
        return await ExamApiClientKeeper.Client.GetAllUsers()
            .ConfigureAwait(false);
    }
}