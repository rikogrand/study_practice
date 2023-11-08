using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class DeleteAttendance_Window : Window
{
    private readonly Attendance _attendance;
    public DeleteAttendance_Window(Attendance attendance)
    {
        InitializeComponent();
        _attendance = attendance;
    }

    private void No_Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Yes_Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
        MySqlConnection _connection;
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Attendance where Attendance_ID = @Attendance_ID LIMIT 1";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Attendance_ID", MySqlDbType.Int32);
        command.Parameters["@Attendance_ID"].Value = _attendance.Attendance_ID;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }
}
