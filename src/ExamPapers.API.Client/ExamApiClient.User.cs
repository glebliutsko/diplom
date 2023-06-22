using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<UserResponse> GetMe()
    {
        return (await GetAsync<UserResponse>("users/me"))!;
    }
}