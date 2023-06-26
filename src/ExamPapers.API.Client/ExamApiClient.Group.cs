using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<GroupResponse[]> GetAllGroups()
    {
        return (await GetAsync<GroupResponse[]>("groups/all").ConfigureAwait(false))!;
    }

    public async Task<SuccessResponse> CreateGroup(GroupRequest newGroup)
    {
        return (await PostAsync<SuccessResponse, GroupRequest>("groups", newGroup).ConfigureAwait(false))!;
    }

    public async Task<SuccessResponse> DeleteGroup(int groupId)
    {
        return (await DeleteAsync<SuccessResponse>($"groups/{groupId}").ConfigureAwait(false))!;
    }
}