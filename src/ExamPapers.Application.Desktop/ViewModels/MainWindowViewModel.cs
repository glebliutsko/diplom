namespace ExamPapers.Application.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
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
}