using System.Linq;
using Avalonia.Controls;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersInput;

public partial class OpenInputUserControl : UserControl, ISingleInputUserControl
{
    private readonly QuestionResponse _question;
    
    public OpenInputUserControl(QuestionResponse question)
    {
        _question = question;
        
        InitializeComponent();
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(AnswerTextBox.Text);
    }

    public bool IsCorrect()
    {
        return _question.Answers
            .Where(x => x.IsCorrect)
            .Select(x => x.Text)
            .Contains(AnswerTextBox.Text);
    }
}