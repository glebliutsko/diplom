using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Client;

namespace ExamPapers.Application.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ErrorTextBlock.Text = "";
        
        var login = LoginTextBox.Text;
        var password = PasswordTextBox.Text;

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            ErrorTextBlock.Text = "Заполните все поля";
            return;
        }

        try
        {
            var token = await ExamApiClientKeeper.Client.Token(login, password);
            ExamApiClientKeeper.Client.Authorization.Token = token;
        }
        catch (ApiResponseError error)
        {
            if (error.Response is { ErrorCode: "InvalidCredential" })
            {
                ErrorTextBlock.Text = "Неверный логин или пароль";
                return;
            }
                
            throw;
        }
    }
}