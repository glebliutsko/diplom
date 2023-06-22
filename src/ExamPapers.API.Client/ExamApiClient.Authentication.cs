using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<TokenResponse> Token(string login, string password)
    {
        ArgumentException.ThrowIfNullOrEmpty(login);
        ArgumentException.ThrowIfNullOrEmpty(password);

        var requestParams = new Dictionary<string, string>
        {
            ["login"] = login,
            ["password"] = password
        };

        return (await GetAsync<TokenResponse>("authentication/token", requestParams).ConfigureAwait(false))!;
    }
}