using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<TestShortResponse[]> GetAllTests()
    {
        return (await GetAsync<TestShortResponse[]>("test/all").ConfigureAwait(false))!;
    }

    public async Task<SuccessResponse> CreateTest(TestRequest newTest)
    {
        return (await PostAsync<SuccessResponse, TestRequest>("test", newTest).ConfigureAwait(false))!;
    }

    public async Task<SuccessResponse> DistributionTestForGroup(TestSessionRequest newSession, int testId, int groupId)
    {
        return (await PostAsync<SuccessResponse, TestSessionRequest>($"test/{testId}/distribution?groupId={groupId}", newSession)
            .ConfigureAwait(false))!;
    }
    
    public async Task<SuccessResponse> DistributionTestForStudent(TestSessionRequest newSession, int testId, int studentId)
    {
        return (await PostAsync<SuccessResponse, TestSessionRequest>($"test/{testId}/distribution?studentId={studentId}", newSession)
            .ConfigureAwait(false))!;
    }
}