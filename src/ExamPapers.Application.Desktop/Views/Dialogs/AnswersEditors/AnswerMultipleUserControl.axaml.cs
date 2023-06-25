using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ExamPapers.Application.Desktop.Views.Dialogs.AnswersEditors;

public partial class AnswerMultipleUserControl : UserControl
{
    private ObservableCollection<AnswerViewModel> _answers;
    
    public AnswerMultipleUserControl()
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