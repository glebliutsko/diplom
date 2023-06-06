using System.Security.Cryptography;
using System.Text;

namespace ExamPapers.API.Server.Utils;

public class RandomString
{
    private const string DEFAULT_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public string Chars { get; }
    
    public RandomString(string chars)
    {
        Chars = chars;
    }
    
    public RandomString() : this(DEFAULT_CHARS)
    { }

    public string Generate(int length)
    {
        var randomStringBuilder = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var indexChar = RandomNumberGenerator.GetInt32(0, Chars.Length);
            randomStringBuilder.Append(Chars[indexChar]);
        }

        return randomStringBuilder.ToString();
    }
}