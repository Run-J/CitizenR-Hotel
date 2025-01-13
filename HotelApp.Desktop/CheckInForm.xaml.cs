using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using System;
using System.Windows;

namespace HotelApp.Desktop
{
    /// <summary>
    /// Provides functionality for guest check-in.
    /// </summary>
    public partial class CheckInForm : Window
    {
        private readonly IDatabaseData _db;
        private BookingFullModel? data = null;

        /// <summary>
        /// Initializes the Check-In form with database access.
        /// </summary>
        /// <param name="db">Database access dependency.</param>
        public CheckInForm(IDatabaseData db)
        {
            InitializeComponent();
            _db = db;
        }

        /// <summary>
        /// Populates the UI with booking details.
        /// </summary>
        /// <param name="data">Booking details to display via "BookingFullModel".</param>
        public void PopulateCheckInfo(BookingFullModel data)
        {
            this.data = data;
            firstNameText.Text = this.data.FirstName;
            lastNameText.Text = this.data.LastName;
            titleText.Text = this.data.Title;
            roomNumberText.Text = this.data.RoomNumber.ToString();
            totalCostText.Text = String.Format("{0:c}", this.data.TotalCost);

            // Format the check-out date to display only the date part
            checkOutDateText.Text = this.data.EndDate.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Handles the Check-In button click event.
        /// </summary>
        private void CheckInUser_Click(object sender, RoutedEventArgs e)
        {
            // Check In
            if (data == null)
            {
                throw new InvalidOperationException("Data is not initialized before attempting to check in a guest.");
            }

            // Marks the guest as checked-in in database.
            _db.CheckInGuest(data.Id);

            // Closes the check-in window after check-in.
            this.Close();
        }
    }
}