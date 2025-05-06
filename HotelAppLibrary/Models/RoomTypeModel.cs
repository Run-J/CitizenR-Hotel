using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Models
{
    /// <summary>
    /// Represents a room type with details such as title, description, and price. Used as a specific type of generic with SqlDataAccess process.
    /// </summary>
    public class RoomTypeModel
    {
        // Attributes of RoomType database
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } // New property for room image URL


        public int AvailableRooms { get; set; } // This is NOT stored in the DB, just filled form SQL result
    }
}
