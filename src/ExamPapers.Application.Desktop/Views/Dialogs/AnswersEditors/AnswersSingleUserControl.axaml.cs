using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersEditors;

public partial class AnswersSingleUserControl : UserControl
{
    private ObservableCollection<AnswerViewModel> _answers;

    public AnswersSingleUserControl()
    {
        _answers = new();
        
        InitializeComponent();

        AnswersItemsControl.ItemsSource = _answers;
    }

    private void AddAnswerButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _answers.Add(new AnswerViewModel());
    }
}