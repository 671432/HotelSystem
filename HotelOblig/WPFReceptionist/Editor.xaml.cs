using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {

        private readonly IReservationService reservationService;
        private readonly ITaskService taskService;
        private ObservableCollection<Reservation> Reservations { get; set; }
            = new ObservableCollection<Reservation>();
        public Reservation SelectedReservation { get; set; }

        public Editor(IReservationService reservationService, ITaskService taskService)
        {
            InitializeComponent();
            this.reservationService = reservationService;
            this.taskService = taskService;
        }

        

        private async void Update_Click(object sender, RoutedEventArgs e)
        {

            var fromDate = fromDatePicker.SelectedDate.Value;
            var toDate = toDatePicker.SelectedDate.Value;

            if (fromDatePicker!=null && toDatePicker!=null)
            {

                SelectedReservation.FromDate = DateOnly.FromDateTime(fromDate);
                SelectedReservation.ToDate = DateOnly.FromDateTime(toDate);
                await reservationService.UpdateReservationAsync(SelectedReservation);
                MessageBox.Show("Utdated!");
                this.Close();
            }
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedReservation != null && SelectedReservation.Status != "In Progress")
            {
                await reservationService.CancleReservationAsync(SelectedReservation.ReservationId);
            }

        }



        private async void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null && SelectedReservation.Status == "Checked In")
            {
                SelectedReservation.Status = "Checked Out";
                
                try
                {
                    
                    
                    await reservationService.UpdateReservationAsync(SelectedReservation);
                    HotelLibrary.Models.Task a = new();
                    a.RoomNrNavigation = SelectedReservation.RoomNrNavigation;
                    a.Status = "New";
                    a.Type = "Clean";
                    a.RoomNr = SelectedReservation.RoomNr;
                    await taskService.AddTaskAsync(a);

                    MessageBox.Show("Check-out successful.");
                    this.Close();
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

        
        }

        private async void Checkin_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedReservation != null && SelectedReservation.Status == "active")
            {
                bool x = SelectedReservation.FromDate<=DateOnly.FromDateTime(DateTime.Now);
                if(x) { 
                SelectedReservation.Status = "Checked In";
                try
                {
                    await reservationService.UpdateReservationAsync(SelectedReservation);
                    MessageBox.Show("Check-in successful.");
                    this.Close();
                }catch (Exception ex)
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

            }else
            {
                MessageBox.Show("Check in failed");
                this.Close();
            }
        }
    }
}







