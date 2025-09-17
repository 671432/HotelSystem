using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using HotelLibrary.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for ManageReservation.xaml
    /// </summary>
    public partial class ManageReservation : Window
    {

        private readonly IReservationService reservationService;
        private ObservableCollection<Reservation> Reservations { get; set; }
            = new ObservableCollection<Reservation>();



        public ManageReservation(IReservationService reservationService)
        {
            InitializeComponent();
            this.reservationService = reservationService;
           
            ReservationsListView.DataContext = Reservations;

            Loaded += Reservation_Loaded;
            //Reservation_Loaded(Re);

            //ReservationsListView.DataContext = Reservations.OrderBy(s => s.ReservationId);
            //Reservations.CollectionChanged += Reservations_CollectionChanged;

        }

      

        private async void Reservation_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var reservations = await reservationService.GetAllReservationsIncudingAllAsync();

                this.Reservations.Clear();

                foreach (var reservation in reservations)
                {
                    this.Reservations.Add(reservation);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load reservations: " + ex.Message);

            }
        }

        private void ReservationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Reservation r = ReservationsListView.SelectedItem as Reservation;

            if(r != null)
            {
                var a = App.GetServices<Editor>();
                
                a.SelectedReservation = r;
                a.Show();
                    
                
                
            }
           
        }
    }
}

