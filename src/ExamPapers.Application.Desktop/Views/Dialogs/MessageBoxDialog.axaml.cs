using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class MessageBoxDialog : Window
{
    public MessageBoxDialog(string text)
    {
        InitializeComponent();

        TextTextBlock.Text = text;
    }

    private void YesButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}