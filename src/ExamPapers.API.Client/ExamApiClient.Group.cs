using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<GroupResponse[]> GetAllGroups()
    {
        return (await GetAsync<GroupResponse[]>("groups/all").ConfigureAwait(false))!;
    }
}