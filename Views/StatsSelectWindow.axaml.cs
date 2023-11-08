using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace study_practice;

public partial class StatsSelectWindow : Window
{
    public StatsSelectWindow()
    {
        InitializeComponent();
    }

    private void CourseStatsWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        CourseStatsWindow courseStatsWindow = new CourseStatsWindow();
        courseStatsWindow.Show();
        this.Close();
    }
    private void AttendanceStatsWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        AttendanceStatsWindow attendanceStatsWindow = new AttendanceStatsWindow();
        attendanceStatsWindow.Show();
        this.Close();
    }
    
    private void PaymentStatsWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        PaymentStatsWindow paymentStatsWindow = new PaymentStatsWindow();
        paymentStatsWindow.Show();
        this.Close();
    }

    private void SchleduleWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        ScheduleWindow scheduleWindow = new ScheduleWindow();
        scheduleWindow.Show();
        this.Close();
    }

    private void Exit_Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}