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

    public async Task<SuccessResponse> CreateUser(NewUserRequest newUser)
    {
        return (await PostAsync<SuccessResponse, NewUserRequest>("users", newUser).ConfigureAwait(false))!;
    }

    public async Task<SuccessResponse> EditUser(int id, NewUserRequest editedUser)
    {
        return (await PutAsync<SuccessResponse, NewUserRequest>($"users/{id}", editedUser).ConfigureAwait(false))!;
    }
    
    public async Task<SuccessResponse> DeleteUser(int id)
    {
        return (await DeleteAsync<SuccessResponse>($"users/{id}").ConfigureAwait(false))!;
    }
    
    public async Task<DistributionTestShortResponse[]> GetMeAvailableTesting()
    {
        return (await GetAsync<DistributionTestShortResponse[]>("users/me/available-testing").ConfigureAwait(false))!;
    }
}