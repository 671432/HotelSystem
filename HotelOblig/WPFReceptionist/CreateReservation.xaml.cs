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
    /// Interaction logic for CreateReservation.xaml
    /// </summary>
    public partial class CreateReservation : Window

    {

        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;
        private ObservableCollection<User> Users;

        public CreateReservation(IReservationService reservationService, IUserService userService)

        {
            this.Users = new ObservableCollection<User>();
            this._reservationService = reservationService;
            this._userService = userService;
            InitializeComponent();
            //_reservationService = reservationService;
            //Loaded += CreateReservation_Click();

        }

        private async void CreateReservation_Click(object sender, RoutedEventArgs e)
        {

            if (_reservationService == null)
            {
                MessageBox.Show("Reservation service is not initialized.");
                return;
            }

            try
            {
                //var userId = int.Parse(userIdTextBox.Text); // Anta at UserId er en integer
                var phone = userPhoneTextBox.Text; 
                var roomNumber = int.Parse(roomNumberTextBox.Text);
                var fromDate = fromDatePicker.SelectedDate.Value;
                var toDate = toDatePicker.SelectedDate.Value;

                // Hente brukeren basert på telefonnummer
                var user = await _userService.GetUserByPhoneAsync(phone);
                if (user == null)
                {
                    MessageBox.Show("No user found with the given phone number.");
                    return;
                }

                var reservation = new Reservation
                {
                    User = user,
                    RoomNr = roomNumber,
                    FromDate = DateOnly.FromDateTime(fromDate), // Convert DateTime to DateOnly
                    ToDate = DateOnly.FromDateTime(toDate)      // Convert DateTime to DateOnly

                };

                await _reservationService.AddReservationAsync(reservation);

                MessageBox.Show("Reservasjon lagret");
                Close(); // Lukk vinduet etter lagring
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                string innerMessages = "";

                // Traversere gjennom alle inner exceptions
                while (innerException != null)
                {
                    innerMessages += innerException.Message + Environment.NewLine;
                    innerException = innerException.InnerException;
                }

                MessageBox.Show($"En feil oppsto: {ex.Message} Inner exceptions: {innerMessages}");
            }
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {

            var x = App.GetServices<CreateUser>();
            x.Show();

        }
    }
}