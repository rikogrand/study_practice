using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace study_practice;

public partial class Success_BuyWindow : Window
{
    public Success_BuyWindow()
    {
        InitializeComponent();
    }

    private void ScheduleButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ScheduleWindow scheduleWindow = new ScheduleWindow();
        scheduleWindow.Show();
        this.Close();
    }

    private void TeacherButton_OnClick(object? sender, RoutedEventArgs e)
    {
        TeacherWindow teacherWindow = new TeacherWindow();
        teacherWindow.Show();
        this.Close();
    }
}