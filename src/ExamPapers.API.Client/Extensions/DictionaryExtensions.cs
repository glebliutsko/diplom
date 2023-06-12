using System.Collections.Generic;
using System.Web;

namespace ExamPapers.API.Client.Extensions;

public static class DictionaryExtensions
{
    public static string ToQueryString(this IDictionary<string, string> dictionaryParams)
    {
        var keyValueQuery = new List<string>();
        foreach (var queryParams in dictionaryParams)
        {
            var encodedKey = HttpUtility.UrlEncode(queryParams.Key);
            var encodedValue = HttpUtility.UrlEncode(queryParams.Value);
            
            keyValueQuery.Add($"{encodedKey}={encodedValue}");
        }

        return string.Join("&", keyValueQuery);
    }
}