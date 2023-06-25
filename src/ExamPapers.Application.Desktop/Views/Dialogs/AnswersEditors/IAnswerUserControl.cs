using System.Collections.Generic;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersEditors;

public interface IAnswerUserControl
{
    List<AnswerResponse> GetAnswers();
}