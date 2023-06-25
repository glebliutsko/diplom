using ExamPapers.API.Entity;

namespace ExamPapers.API.Client;

public partial class ExamApiClient
{
    public async Task<QuestionResponse[]> GetAllQuestion()
    {
        return (await GetAsync<QuestionResponse[]>("question/all").ConfigureAwait(false))!;
    }

    public async Task<SuccessResponse> CreateQuestion(QuestionRequest newQuestion)
    {
        return (await PostAsync<SuccessResponse, QuestionRequest>("question", newQuestion).ConfigureAwait(false))!;
    }
}