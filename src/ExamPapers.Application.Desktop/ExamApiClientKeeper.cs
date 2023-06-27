using System;
using ExamPapers.API.Client;

namespace ExamPapers.Application.Desktop;

public static class ExamApiClientKeeper
{
    private static readonly Lazy<ExamApiClient> CLIENT_LAZY =
        new Lazy<ExamApiClient>(() => new ExamApiClient("http://127.0.0.1:5000"));

    public static ExamApiClient Client => CLIENT_LAZY.Value;
}