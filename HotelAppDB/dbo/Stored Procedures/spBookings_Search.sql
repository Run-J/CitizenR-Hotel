-- ====================================================================================
-- Stored Procedure: spBookings_Search
-- 
-- Purpose:
--   Retrieves detailed booking information for a specific guest's last name 
--   and booking start date.
--
-- Parameters:
--   @lastName (NVARCHAR(50)) - The last name of the guest.
--   @startDate (DATE)        - The start date of the booking to search.
--
-- Logic:
--   1. Joins the `Bookings` table with `Guests`, `Rooms`, and `RoomTypes`.
--   2. Filters bookings by the guest's last name and start date.
--   3. Returns detailed booking information, including guest, room, and room type details.

CREATE PROCEDURE [dbo].[spBookings_Search]
	@lastName nvarchar(50),
	@startDate date
AS
begin
	set nocount on;

	select [b].[Id], [b].[RoomId], [b].[GuestId], [b].[StartDate], [b].[EndDate], [b].[CheckedIn], [b].[TotalCost], 
			[g].[FirstName], [g].[LastName], 
			[r].[RoomNumber], [r].[RoomTypesId], 
			[rt].[Title], [rt].[Description], [rt].[Price] 
	from dbo.Bookings b
	inner join dbo.Guests g on b.GuestId = g.Id
	inner join dbo.Rooms r on b.RoomId = r.Id
	inner join dbo.RoomTypes rt on r.RoomTypesId = rt.Id
	where b.startDate = @startDate and g.LastName = @lastName;
end
