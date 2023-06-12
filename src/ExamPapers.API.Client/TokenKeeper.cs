using System.Net.Http;
using System.Net.Http.Headers;
using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public class TokenKeeper
{
    private readonly HttpClient _httpClient;

    private TokenResponse? _token;

    public TokenResponse? Token
    {
        private get => _token;
        set
        {
            _token = value;
            UpdateHeaders();
        }
    }

    public bool IsAuthorized => Token != null;

    public TokenKeeper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private void UpdateHeaders()
    {
        if (_token == null)
            _httpClient.DefaultRequestHeaders.Authorization = null;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _token!.AccessToken);
    }
}