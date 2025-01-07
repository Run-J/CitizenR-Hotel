using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp.Desktop
{
    /// <summary>
    /// Main application window for managing guest bookings and check-ins.
    /// </summary>
    public partial class MainWindow : Window
    {

        public IDatabaseData _db;


        /// <summary>
        /// Initializes the main window with database access.
        /// </summary>
        /// <param name="db">Database access dependency.</param>
        public MainWindow(IDatabaseData db)
        {
            InitializeComponent();
            _db = db;
        }


        /// <summary>
        /// Handles the search button click event to find guest bookings by last name.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        public void SearchForGuest_Click(object sender, RoutedEventArgs e)
        {
            // Fetch bookings matching the guest's last name.
            List<BookingFullModel> bookings = _db.SearchBookings(lastNameText.Text);

            // Display the search results in the ListBox.
            resultsList.ItemsSource = bookings;
        }


        /// <summary>
        /// Handles the Check-In button click event for a selected booking.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        public void CheckInButton_click(object sender, RoutedEventArgs e)
        {
            if (App.serviceProvider == null)
            {
                throw new InvalidOperationException("ServiceProvider is not initialized.");
            }

            // Retrieve an instance of the CheckInForm from the service provider.
            var checkInForm = App.serviceProvider.GetService<CheckInForm>();

            // Get the booking details from the clicked button's DataContext.
            var model = (BookingFullModel)((Button)e.Source).DataContext; // Retrieves the BookingFullModel instance tied to the button (check In) that triggered the event.


            if (checkInForm != null)
            {
                // Populate the Check In info
                checkInForm.PopulateCheckInfo(model);

                // Show the Check In Form
                checkInForm.Show();
            }
            else
            {
                throw new InvalidOperationException("Failed to resolve CheckInForm from ServiceProvider.");
            }
        }

    }
}
