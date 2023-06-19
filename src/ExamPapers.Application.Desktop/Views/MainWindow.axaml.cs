using Avalonia.Controls;
using Avalonia.Interactivity;

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

        var token = await ExamApiClientKeeper.Client.Token(login, password);

        ErrorTextBlock.Text = token.AccessToken;
    }
}