using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages
{
    public class RoomSearchModel : PageModel
    {
        private readonly IDatabaseData _db; // Database access dependency

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now; // Default start date is today

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1); // Default end date is tomorrow

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false; // Toggle for search functionality

        public List<RoomTypeModel> AvailableRoomTypes { get; set; } = new List<RoomTypeModel>(); // For stores search results



        public RoomSearchModel(IDatabaseData db)
        {
            _db = db;
        }


        public IActionResult OnPost() 
        {
            // Handles POST requests and redirects to the same page with query parameters.
            return RedirectToPage 
            (new 
                {SearchEnabled = true, 
                StartDate = StartDate.ToString("yyyy-MM-dd"), 
                EndDate = EndDate.ToString("yyyy-MM-dd")}
            );
        }

        public void OnGet()
        {
            if (SearchEnabled == true)
            {
                AvailableRoomTypes = _db.GetAvailableRoomTypes(StartDate, EndDate);
            }
        }
    }
}
