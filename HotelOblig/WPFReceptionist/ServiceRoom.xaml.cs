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
using HotelLibrary.Services;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for ServiceRoom.xaml
    /// </summary>
    public partial class ServiceRoom : Window
    {

        private readonly IRoomService roomService;
        private ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ServiceRoom(IRoomService roomService)
        {
            InitializeComponent();
            this.roomService = roomService;

            RoomListView.DataContext = Rooms;

            Loaded += Rooms_Loaded;
        }
        private async void Rooms_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var rooms = await roomService.GetAllRoomsAsync();

                this.Rooms.Clear();

                foreach (var room in rooms)
                {
                    this.Rooms.Add(room);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load rooms: " + ex.Message);

            }
        }

        private void RoomListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Room r = RoomListView.SelectedItem  as Room;

            if (r != null)
            {
                var a = App.GetServices<TaskManager>();

                a.SelectedRoom = r;
                a.Show();

            }
    }
}
}
