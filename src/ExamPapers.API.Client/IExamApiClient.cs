namespace ExamPapers.API.Client;

public interface IExamApiClient
{
    public Task<TResponse?> GetAsync<TResponse>(string url, IDictionary<string, string> queryStringParams);
    public Task<TResponse?> PostAsync<TResponse>(string url, HttpContent content);
    public Task<TResponse?> PostAsync<TResponse, TRequest>(string url, TRequest content);
}