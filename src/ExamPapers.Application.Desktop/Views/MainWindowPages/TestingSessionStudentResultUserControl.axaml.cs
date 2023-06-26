using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.MainWindowPages;

public class StudentTestingResultToString : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TestingResultResponse result)
            return "не пройдено";

        return $"{result.PassedTime:hh:mm:ss dd.MM.yyyy} {result.Score}/{result.AllScore} баллов";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public partial class TestingSessionStudentResultUserControl : UserControl
{
    private readonly TestSessionResponse _session;

    public TestingSessionStudentResultUserControl(TestSessionResponse session)
    {
        _session = session;
        
        InitializeComponent();

        ResultItemsControl.ItemsSource = session.Distributions;
    }
}