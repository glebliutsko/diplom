using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;
using SkiaSharp;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class NewUserDialog : Window
{
    private readonly GroupResponse[] _groups;

    public NewUserRequest? Result { get; private set; } = null;

    public NewUserDialog(UserResponse? user = null)
    {
        InitializeComponent();

        _groups = GetAllGroups().Result;
        GroupComboBox.ItemsSource = _groups;

        if (user != null)
        {
            HeaderTextBlock.Text = "Изменение пользователя";
            
            FullNameTextBox.Text = user.FullName;
            LoginTextBox.Text = user.Login;
            RoleComboBox.SelectedIndex = user.Role switch
            {
                "Admin" => 0,
                "Teacher" => 1,
                "Student" => 2,
                _ => -1
            };
            if (user.Group != null)
                GroupComboBox.SelectedIndex = _groups.ToList().FindIndex(x => x.Id == user.Group?.Id);
        }
    }

    private async Task<GroupResponse[]> GetAllGroups()
    {
        return await ExamApiClientKeeper.Client.GetAllGroups()
            .ConfigureAwait(false);
    }

    private void AcceptButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ErrorTextBlock.Text = "";
        
        var fullName = FullNameTextBox.Text;
        var login = LoginTextBox.Text;

        var roleSelectedCombobox = RoleComboBox.SelectedItem as ComboBoxItem;
        var role = roleSelectedCombobox?.Content?.ToString() switch
        {
            "Администратор" => "Admin",
            "Преподаватель" => "Teacher",
            "Студент" => "Student",
            _ => null
        };
        
        var group = GroupComboBox.SelectedIndex != -1 ? _groups[GroupComboBox.SelectedIndex] : null;

        var password = PasswordTextBox.Text;
        var repeatPassword = RepeatPasswordLoginTextBox.Text;

        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(role))
        {
            ErrorTextBlock.Text = "Заполните все поля";
            return;
        }
        
        var newUser = new NewUserRequest
        {
            FullName = fullName,
            Login = login,
            Role = role,
        };

        if (!string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(repeatPassword))
        {
            if (password != repeatPassword)
            {
                ErrorTextBlock.Text = "Пароли не совпадают";
                return;
            }

            newUser.Password = password;
        }

        if (group != null)
        {
            newUser.GroupId = group.Id;
        }

        Result = newUser;

        Close();
    }
    
    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}