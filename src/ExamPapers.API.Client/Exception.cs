using System.Net;
using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public class ApiResponseError : Exception
{
    public HttpStatusCode StatusCode { get; }
    public ErrorsListResponse? Response { get; }
    
    public ApiResponseError(string message, HttpStatusCode statusCode, ErrorsListResponse? response) : base(message)
    {
        StatusCode = statusCode;
        Response = response;
    }
}