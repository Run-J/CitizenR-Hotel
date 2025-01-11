using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string? RoomNumber { get; set; }
        public int RoomTypeId { get; set; }

    }
}
