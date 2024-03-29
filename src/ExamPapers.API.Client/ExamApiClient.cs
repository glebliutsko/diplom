using System.Net;
using System.Text;
using System.Text.Json;
using ExamPapers.API.Client.Extensions;
using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient : IExamApiClient
{
    private static readonly JsonSerializerOptions JSON_OPTION = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public TokenKeeper Authorization { get; }

    public ExamApiClient(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl;

        Authorization = new TokenKeeper(_httpClient);
    }

    public ExamApiClient(string baseUrl) : this(new HttpClient(), baseUrl)
    { }

    private async Task<HttpResponseMessage> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        HttpResponseMessage? response;
        try
        {
            response = await _httpClient
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (TaskCanceledException e)
        {
            if (cancellationToken.IsCancellationRequested)
                throw;

            throw new Exception("Request timed out", e); //TODO: Заменить на нормальное исключение
        }
        catch (Exception e)
        {
            throw new Exception("Exception during making request", e); //TODO: Заменить на нормальное исключение
        }

        return response;
    }

    private async Task<HttpResponseMessage> MakeRequest(
        HttpMethod method,
        string urlPath,
        IDictionary<string, string>? queryStringParams = null,
        HttpContent? requestContent = null)
    {
        var url = $"{_baseUrl}/{urlPath}";
        if (queryStringParams != null)
            url += "?" + queryStringParams.ToQueryString();

        var httpRequest = new HttpRequestMessage(method, url);
        if (requestContent != null)
            httpRequest.Content = requestContent;

        var httpResponse = await MakeRequest(httpRequest, CancellationToken.None).ConfigureAwait(false);

        return httpResponse;
    }

    private async Task<TResponse?> MakeRequestJson<TResponse>(
        string urlPath,
        HttpMethod method,
        IDictionary<string, string>? queryStringParams = null,
        HttpContent? requestContent = null)
    {
        using var responseMessage = await MakeRequest(method, urlPath, queryStringParams, requestContent)
            .ConfigureAwait(false);
        var responseContent = responseMessage.Content;

        if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            return default;

        if (responseContent.Headers.ContentType?.MediaType != "application/json")
            throw new Exception("Unsupported Content-Type");

        var streamContent = await responseContent.ReadAsStreamAsync()
            .ConfigureAwait(false);

        if (!responseMessage.IsSuccessStatusCode)
        {
            var errorResponse = await JsonSerializer.DeserializeAsync<ErrorsListResponse>(
                    streamContent, 
                    JSON_OPTION)
                .ConfigureAwait(false);

            throw new ApiResponseError("API returned error",
                responseMessage.StatusCode,
                errorResponse);
        }

        var parsedResult = await JsonSerializer.DeserializeAsync<TResponse>(
                streamContent,
                JSON_OPTION)
            .ConfigureAwait(false);

        return parsedResult;
    }

    public async Task<TResponse?> GetAsync<TResponse>(
        string urlPath,
        IDictionary<string, string>? queryStringParams = null)
    {
        return await MakeRequestJson<TResponse>(urlPath, HttpMethod.Get, queryStringParams)
            .ConfigureAwait(false);
    }
    
    public async Task<TResponse?> DeleteAsync<TResponse>(
        string urlPath,
        IDictionary<string, string>? queryStringParams = null)
    {
        return await MakeRequestJson<TResponse>(urlPath, HttpMethod.Delete, queryStringParams)
            .ConfigureAwait(false);
    }

    public async Task<TResponse?> PostAsync<TResponse>(string urlPath, HttpContent content)
    {
        return await MakeRequestJson<TResponse>(urlPath, HttpMethod.Post, requestContent: content)
            .ConfigureAwait(false);
    }

    public async Task<TResponse?> PostAsync<TResponse, TRequest>(string urlPath, TRequest content)
    {
        var jsonContent = JsonSerializer.Serialize(content);

        HttpContent requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return await PostAsync<TResponse>(urlPath, requestContent)
            .ConfigureAwait(false);
    }
    
    public async Task<TResponse?> PutAsync<TResponse>(string urlPath, HttpContent content)
    {
        return await MakeRequestJson<TResponse>(urlPath, HttpMethod.Put, requestContent: content)
            .ConfigureAwait(false);
    }
    
    public async Task<TResponse?> PutAsync<TResponse, TRequest>(string urlPath, TRequest content)
    {
        var jsonContent = JsonSerializer.Serialize(content);

        HttpContent requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return await PutAsync<TResponse>(urlPath, requestContent)
            .ConfigureAwait(false);
    }
}