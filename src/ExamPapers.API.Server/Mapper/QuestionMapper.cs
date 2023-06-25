namespace ExamPapers.API.Server.Mapper;

public static class QuestionMapper
{
    public static QuestionResponse OrmModel2ApiEntity(ORMModels.Question question)
    {
        return new QuestionResponse
        {
            Id = question.Id,
            Text = question.Text,
            Type = Enum.Parse<QuestionTypeResponse>(question.Type.ToString()),
            Answers = question.Answers
                .Select(x => new AnswerResponse {Text = x.Text, IsCorrect = x.IsCorrect})
                .ToList()
        };
    }
}