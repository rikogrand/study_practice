using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class EditAttendance_Window : Window
{
    
   private readonly Attendance _attendance;
   public Client Client { get; init; }
   private List<Client> _clients;
    private MySqlConnection _connection;
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    
    public EditAttendance_Window(Attendance attendance)
    {
        InitializeComponent();

        _attendance = attendance;
        FillClient();
    }
    

    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        List<Attendance> _attendances;
        MySqlConnection _connection;
        string sql = "update Attendance set Attendance_ID = @Attendance_ID, " +
                     "Client = @Client, " +
                     "Number_Of_Classes_Per_Month = @Number_Of_Classes_Per_Month, " +
                     "Number_Of_Classes_Client = @Number_Of_Classes_Client, " +
                     "Number_Of_Pass = @Number_Of_Pass," +
                     " Ratings = @Ratings " +
                     "where Attendance_ID = @Attendance_ID";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Attendance_ID", MySqlDbType.Int32);
        command.Parameters["@Attendance_ID"].Value = attendanceID.Text;
        
        
        
        command.Parameters.Add("@Client", MySqlDbType.Int32);
        command.Parameters["@Client"].Value = (ClientComboBox.SelectedItem as study_practice.Client).Client_ID;
        
        command.Parameters.Add("@Number_Of_Classes_Per_Month", MySqlDbType.Int32);
        command.Parameters["@Number_Of_Classes_Per_Month"].Value = numberOfClassesPerMonth.Text;
        
        command.Parameters.Add("@Number_Of_Classes_Client", MySqlDbType.Int32);
        command.Parameters["@Number_Of_Classes_Client"].Value = numberOfClassesClient.Text;
        
        command.Parameters.Add("@Number_Of_Pass", MySqlDbType.Int32);
        command.Parameters["@Number_Of_Pass"].Value = numberOfPass.Text;
        
        command.Parameters.Add("@Ratings", MySqlDbType.String);
        command.Parameters["@Ratings"].Value = ratings.Text;
        
        command.ExecuteReader();
        _connection.Close();
        Close(true);
    }
    
    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }
    public bool Result { get; private set; }
    
    public void FillClient()
    {
        string sql = "select Client_ID, Surname from pro1_11.Client";
        _clients = new List<Client>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curClient = new Client()
            {
                Client_ID = reader.GetInt32("Client_ID"),
                Surname = reader.GetString("Surname")
            };
            _clients.Add(curClient);
        }
        _connection.Close();
        var clientComboBox = this.Find<ComboBox>("ClientComboBox");
        clientComboBox.ItemsSource = _clients;
        clientComboBox.SelectedItem = clientComboBox.ItemsSource.Cast<Client>().First(it => it.Surname == _attendance.Client);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        AttendanceWindow attendanceWindow = new AttendanceWindow();
        attendanceWindow.Show();
        this.Close();
    }
    
}