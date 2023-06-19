using System.Collections.Generic;
using System.Threading.Tasks;
using ExamPapers.API.Client;
using ExamPapers.API.Entity;
using ExamPapers.Application.Desktop.ViewModels.Command;

namespace ExamPapers.Application.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        LoginCommand = new AsyncRelayCommand(SignUp);
    }
    
    #region Login
    private string _login = string.Empty;
    public string Login
    {
        get => _login;
        set => SetField(ref _login, value);
    }
    #endregion

    #region Password
    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }
    #endregion
    
    public AsyncRelayCommand LoginCommand { get; }

    private async Task SignUp(object? _)
    {
        var apiClient = new ExamApiClient("https://localhost:5000");

        var credential = new Dictionary<string, string>
        {
            ["login"] = Login,
            ["Password"] = Password
        };

        var token = await apiClient.GetAsync<TokenResponse>("/", credential);
    } 
}