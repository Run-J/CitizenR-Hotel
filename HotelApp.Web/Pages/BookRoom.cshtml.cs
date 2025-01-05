using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Web.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IDatabaseData _db; // Database access dependency

        [BindProperty(SupportsGet = true)]
        public int RoomTypeId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;


        public RoomTypeModel? RoomType { get; set; }

        public BookRoomModel(IDatabaseData db)
        {
            _db = db;
        }

        public void OnGet()
        {
            if (RoomTypeId > 0)
            {
                RoomType = _db.GetRoomTypeById(RoomTypeId); // Get room details for show on the web summary page
            }
        }

        public IActionResult OnPost()
        {
            _db.BookGuest(FirstName, LastName, StartDate, EndDate, RoomTypeId); // Write Guest booking info into database
            return RedirectToPage("/Index");
        }
    }
}
