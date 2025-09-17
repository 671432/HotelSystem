using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using HotelLibrary.Services;
using WPFReceptionist;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for AllTasks.xaml
    /// </summary>
    public partial class AllTasks : Window
    {
        private readonly ITaskService _taskService;
        private ObservableCollection<HotelLibrary.Models.Task> tasks { get; set; }
            = new ObservableCollection<HotelLibrary.Models.Task>();
        

        public AllTasks(ITaskService taskService)
        {
            InitializeComponent();
            this._taskService = taskService;
            TaskListView.DataContext = tasks;
            Loaded += Tasks_Loaded;
        }

        public async void Tasks_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
            var task = await _taskService.GetAllTasksAsync();

            this.tasks.Clear();

            foreach (var t in task)
            {
                this.tasks.Add(t);
            }
            }catch (Exception ex) {

                MessageBox.Show("Failed to load reservations: " + ex.Message);

            }
        }
    }
}

