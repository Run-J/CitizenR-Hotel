-- ====================================================================================
-- Stored Procedure: spRooms_GetAvailableRooms
--
-- Purpose:
--   Retrieves a list of available rooms of a specific type that are not booked 
--   within the specified date range.
--
-- Parameters:
--   @startDate (DATE)    - The start date of the booking range to check availability.
--   @endDate (DATE)      - The end date of the booking range to check availability.
--   @roomTypeId (INT)    - The identifier for the room type to filter results.
--
-- Logic:
--   1. Joins the `Rooms` table with the `RoomTypes` table to filter by room type.
--   2. Excludes rooms that are already booked within the given date range.
--   3. Returns a list of available rooms matching the specified room type and date range.

CREATE PROCEDURE [dbo].[spRooms_GetAvailableRooms]
	@startDate date,
	@endDate date,
	@roomTypeId int
AS
begin
	set nocount on

	select r.*
	from dbo.Rooms r
	inner join dbo.RoomTypes t on t.Id = r.RoomTypesId
	where r.RoomTypesId = @roomTypeId
	and r.Id not in (
	select b.RoomId
	from dbo.Bookings b
	where (@startDate < b.StartDate and @endDate > b.EndDate)
		or (@endDate >= b.StartDate and @endDate < b.EndDate)
		or (@startDate >= b.StartDate and @startDate < b.EndDate)
	);

end
