using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Web.Pages
{
    public class BookingConfirmationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string LastName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int RoomTypeId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        public RoomTypeModel? RoomType { get; set; }

        public decimal TotalCost =>
            RoomType != null
                ? RoomType.Price * (decimal)(EndDate - StartDate).TotalDays
                : 0;

        private readonly IDatabaseData _db;

        public BookingConfirmationModel(IDatabaseData db)
        {
            _db = db;
        }

        public void OnGet()
        {
            if (RoomTypeId > 0)
            {
                RoomType = _db.GetRoomTypeById(RoomTypeId); // Get room details
            }
        }
    }
}