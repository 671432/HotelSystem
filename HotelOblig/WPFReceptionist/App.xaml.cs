using Microsoft.Extensions.DependencyInjection;
using HotelLibrary.Repos;
using HotelLibrary.Services;
using HotelLibrary.Interfaces;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using HotelLibrary.Models;
using System;
using WPFReceptionist;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<HotelContext>(options =>
                options.UseSqlServer(
                    "Server=tcp:bordel69.database.windows.net,1433;Initial Catalog=Hotel;" +
                    "Persist Security Info=False;User ID=h671432;Password=1234567890aA;" +
                    "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;" +
                    "Connection Timeout=30;"
                    ));
            //Bruk scoped
            services.AddTransient<IReservationRepo, ReservationRepo>();
            services.AddTransient<IRoomRepo, RoomRepo>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IRoomService, RoomService>();

            services.AddTransient<ITaskRepo, TaskRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IUserService, UserService>();


            //scoped
            services.AddTransient<MainWindow>();
            services.AddTransient<ManageReservation>();
            services.AddTransient<Authenticate>();

            services.AddTransient<CreateReservation>();
            services.AddTransient<CreateUser>();

            //Transient
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ServiceRoom>();
            services.AddTransient<Editor>();
            services.AddTransient<TaskManager>();
            services.AddTransient<AllTasks>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }

        public static T? GetServices<T>()
        {
            return _serviceProvider!.GetService<T>();
        }

    }

}
