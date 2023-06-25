using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<QuestionResponse[]> GetAllQuestion()
    {
        return (await GetAsync<QuestionResponse[]>("question/all").ConfigureAwait(false))!;
    }
}