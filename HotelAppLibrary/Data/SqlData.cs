using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Data
{
    /// <summary>
    /// Talk with UI. Provides data access and business logic methods for hotel operations, including
    /// room availability checks and guest bookings etc.
    /// </summary>
    public class SqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlData"/> class.
        /// </summary>
        /// <param name="db">An implementation of <see cref="ISqlDataAccess"/> used for database operations.</param>
        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves a list of available room types for a specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the requested booking period.</param>
        /// <param name="endDate">The end date of the requested booking period.</param>
        /// <returns>
        /// A list of <see cref="RoomTypeModel"/> representing the available room types.
        /// </returns>
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return _db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                                                 new { startDate, endDate },
                                                 connectionStringName,
                                                 true);
        }

        /// <summary>
        /// Books a guest into a room for a specified date range and room type.
        /// </summary>
        /// <param name="firstName">The first name of the guest.</param>
        /// <param name="lastName">The last name of the guest.</param>
        /// <param name="startDate">The start date of the booking period.</param>
        /// <param name="endDate">The end date of the booking period.</param>
        /// <param name="roomTypeId">The identifier for the selected room type.</param>
        public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
        {
            GuestModel guest = _db.LoadData<GuestModel, dynamic>("dbo.spGuests_Insert",
                                                                 new { firstName, lastName },
                                                                 connectionStringName,
                                                                 true).First();

            RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>("select * from dbo.RoomTypes where Id = @Id",
                                                                          new {Id = roomTypeId},
                                                                          connectionStringName,
                                                                          false).First();

            TimeSpan timeStaying = endDate.Date.Subtract(startDate.Date);

            List<RoomModel> availableRooms = _db.LoadData<RoomModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                                              new { startDate, endDate, roomTypeId },
                                                                              connectionStringName,
                                                                              true);

            _db.SaveData("dbo.spBookings_Insert",
                         new 
                         { 
                             roomId = availableRooms.First().Id, 
                             guestId = guest.Id,
                             startDate = startDate,
                             endDate = endDate,
                             totalCost = timeStaying.Days * roomType.Price
                         },
                         connectionStringName,
                         true);
        }
    }
}
