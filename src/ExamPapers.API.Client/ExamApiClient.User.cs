using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<UserResponse> GetMe()
    {
        return (await GetAsync<UserResponse>("users/me").ConfigureAwait(false))!;
    }
    
    public async Task<UserResponse[]> GetAllUsers()
    {
        return (await GetAsync<UserResponse[]>("users/all").ConfigureAwait(false))!;
    }
}