using Avalonia;
using Material.Icons;

namespace ExamPapers.Application.Desktop.Views.MainMenuItems;

public interface IMainMenuItem
{
    public MaterialIconKind Icon { get; set; }
    public string Title { get; set; }

    public AvaloniaObject GetContent();
}