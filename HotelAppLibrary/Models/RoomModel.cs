﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace HotelAppLibrary.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }

    }
}
