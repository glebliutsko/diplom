using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class ConfirmActionDialog : Window
{
    public bool Result { get; private set; } = false;
    
    public ConfirmActionDialog(string text)
    {
        InitializeComponent();

        QuestionTextBlock.Text = text;
    }

    private void YesButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Result = true;
        Close();
    }

    private void NoButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Result = false;
        Close();
    }
}