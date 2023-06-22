using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Client;

namespace ExamPapers.Application.Desktop.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private async void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var locker = InputAutoLocker.Lock(LoginButton, LoginTextBox, PasswordTextBox);
        
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