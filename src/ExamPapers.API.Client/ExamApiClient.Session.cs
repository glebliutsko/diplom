using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<TestSessionResponse[]> GetMyTestingSessions()
    {
        return (await GetAsync<TestSessionResponse[]>("testingsession/all").ConfigureAwait(false))!;
    }
}