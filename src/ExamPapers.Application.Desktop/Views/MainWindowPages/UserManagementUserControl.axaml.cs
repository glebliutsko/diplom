using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.Views.Dialogs;
using NReco.Csv;

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

    private async void ImportButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var fileDialog = new OpenFileDialog();
        fileDialog.Filters.Add(new() { Name = "CSV File", Extensions =  { "csv" } });

        string[] files = await fileDialog.ShowAsync(_mainWindow);
        
        if (files == null)
            return;

        await using var fstream = File.OpenRead(files[0]);
        using var streamRdr = new StreamReader(fstream);
        
        var csvReader = new CsvReader(streamRdr, ",");
        if (!csvReader.Read())
            return;

        var header = new List<string>();
        for (var i = 0; i < csvReader.FieldsCount; i++)
            header.Add(csvReader[i]);

        var fullNameIndex = header.IndexOf("fullname");
        var loginIndex = header.IndexOf("login");
        var roleIndex = header.IndexOf("role");
        var groupIndex = header.IndexOf("group");
        var passwordIndex = header.IndexOf("password");

        var users = new List<NewUserRequest>(); 
        while (csvReader.Read())
        {
            var fullname = csvReader[fullNameIndex];
            var login = csvReader[loginIndex];
            var role = csvReader[roleIndex];
            var group = csvReader[groupIndex];
            var password = csvReader[passwordIndex];
          
            users.Add(new()
            {
                FullName = fullname,
                Login = login,
                Role = role,
                GroupName = group,
                Password = password
            });
        }

        foreach (var user in users)
            await CreateUser(user);

        LoadUsers();
    }

    private async void EditUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        var user = (UserResponse)button.Tag;
        
        var newUserDialog = new NewUserDialog(user);
        await newUserDialog.ShowDialog(_mainWindow);
        
        if (newUserDialog.Result == null)
            return;

        var result = await ExamApiClientKeeper.Client.EditUser(user.Id, newUserDialog.Result);
        
        LoadUsers();
    }

    private async void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;
        var user = (UserResponse)button.Tag;
        
        var confirmDialog = new ConfirmActionDialog($"Удалить пользователя {user.Login}?");
        await confirmDialog.ShowDialog(_mainWindow);

        if (confirmDialog.Result)
            await ExamApiClientKeeper.Client.DeleteUser(user.Id);
        
        LoadUsers();
    }
}