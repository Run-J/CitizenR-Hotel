-- ====================================================================================
-- Stored Procedure: spRoomTypes_GetAvailableTypes
-- 
-- Purpose:
--	 Retrieves a list of available room types that are not booked within a specified date range.
--
-- Parameters:
--   @startDate (DATE) - The start date of the booking range to check availability.
--   @endDate (DATE)   - The end date of the booking range to check availability.
--
-- Logic:
--   1. Joins the `Rooms` table with the `RoomTypes` table to get room details.
--   2. Filters out rooms that are already booked within the given date range (These will not be displyed).
--   3. Groups results by room type details (Id, Title, Description, Price).

CREATE PROCEDURE [dbo].[spRoomTypes_GetAvailableTypes]
	@startDate date,
	@endDate date
AS
begin
	set nocount on;

	select t.Id, t.Title, t.Description, t.Price, t.ImageUrl
	from dbo.Rooms r
	inner join dbo.RoomTypes t on t.Id = r.RoomTypesId
	where r.Id not in (
	select b.RoomId
	from dbo.Bookings b
	where (@startDate < b.StartDate and @endDate > b.EndDate)
		or (@endDate >= b.StartDate and @endDate < b.EndDate)
		or (@startDate >= b.StartDate and @startDate < b.EndDate)
	)
	group by t.Id, t.Title, t.Description, t.Price, t.ImageUrl
end
