using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using WPFReceptionist;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for TaskManager.xaml
    /// </summary>
    public partial class TaskManager : Window
    {
       
        private readonly ITaskService taskService;
        public Room SelectedRoom { get; set; }
        private ObservableCollection<HotelLibrary.Models.Task> tasks { get; set; } 
            = new ObservableCollection<HotelLibrary.Models.Task>();   
        public TaskManager(ITaskService taskService)
        {
            this.taskService = taskService;
            
            InitializeComponent();
            
        }

        private async void Cleaing_Click(object sender, RoutedEventArgs e)
        {

            HotelLibrary.Models.Task a = new();
            a.Status = "New";
            a.Type = "Clean";
            a.RoomNr = SelectedRoom.RoomNr;
            a.RoomNrNavigation = SelectedRoom;
            await taskService.AddTaskAsync(a);

            MessageBox.Show("Cleaning ordered");
           
        }

        private async void RoomService_Click(object sender, RoutedEventArgs e)
        {
            HotelLibrary.Models.Task a = new();
            a.Status = "New";
            a.Type = "RoomService";
            a.RoomNr = SelectedRoom.RoomNr;
            a.RoomNrNavigation = SelectedRoom;
            await taskService.AddTaskAsync(a);

            MessageBox.Show("RoomService ordered");
        }

        private async void Maintenance_Click(object sender, RoutedEventArgs e)
        {
            HotelLibrary.Models.Task a = new();
            a.Status = "New";
            a.Type = "Maintenance";
            a.RoomNr = SelectedRoom.RoomNr;
            a.RoomNrNavigation = SelectedRoom;
            await taskService.AddTaskAsync(a);

            MessageBox.Show("Maintenance ordered");
        }

        private void ListAll_Click(object sender, RoutedEventArgs e)
        {
            var a = App.GetServices<AllTasks>();
            a.Show();
        }
    }
}

