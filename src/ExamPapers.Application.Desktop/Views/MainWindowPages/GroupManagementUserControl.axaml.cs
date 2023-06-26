using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public partial class GroupManagementUserControl : UserControl
{
    private readonly MainWindow _mainWindow;

    public GroupManagementUserControl(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        
        InitializeComponent();

        LoadGroup();
    }

    private void LoadGroup()
    {
        GroupsItemsControl.ItemsSource = ExamApiClientKeeper.Client.GetAllGroups().Result;
    }

    private async void CreateGroupButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var newGroupDialog = new NewGroupDialog();
        await newGroupDialog.ShowDialog(_mainWindow);

        if (newGroupDialog.Result == null)
            return;

        await ExamApiClientKeeper.Client.CreateGroup(newGroupDialog.Result);
        
        LoadGroup();
    }

    private async void EditGroupButton_OnClick(object? sender, RoutedEventArgs e)
    {   
        if (sender is not Button buttonSender)
            return;
        
        if (buttonSender.Tag is not GroupResponse group)
            return;
        
        var newGroupDialog = new NewGroupDialog(group);
        await newGroupDialog.ShowDialog(_mainWindow);
        
        if (newGroupDialog.Result == null)
            return;

        await ExamApiClientKeeper.Client.EditGroup(group.Id, newGroupDialog.Result);
        
        LoadGroup();
    }

    private async void DeleteGroupButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button buttonSender)
            return;
        
        if (buttonSender.Tag is not GroupResponse group)
            return;
        
        var confirmDialog = new ConfirmActionDialog($"Удалить группу {group.Name}?");
        await confirmDialog.ShowDialog(_mainWindow);

        if (confirmDialog.Result)
            await ExamApiClientKeeper.Client.DeleteGroup(group.Id);
        
        LoadGroup();
    }
}