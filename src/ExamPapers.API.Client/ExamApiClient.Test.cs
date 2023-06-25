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
}