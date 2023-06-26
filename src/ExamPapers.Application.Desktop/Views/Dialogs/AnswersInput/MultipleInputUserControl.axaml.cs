using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersInput;

public partial class MultipleInputUserControl : UserControl, ISingleInputUserControl
{
    private readonly QuestionResponse _question;
    private readonly ObservableCollection<AnswerInputViewModel> _answersViewModel;
    
    public MultipleInputUserControl(QuestionResponse question)
    {
        _question = question;
        
        InitializeComponent();
        
        _answersViewModel = new ObservableCollection<AnswerInputViewModel>(_question.Answers
            .Select(x => new AnswerInputViewModel(x)));
        AnswersItemsControl.ItemsSource = _answersViewModel;
    }

    public bool IsValid()
    {
        var isAllUnselected = _answersViewModel.All(x => !x.IsSelected);
        return !isAllUnselected;
    }

    public bool IsCorrect()
    {
        return _answersViewModel.All(x => x.IsSelected == x.Answer.IsCorrect);
    }
}