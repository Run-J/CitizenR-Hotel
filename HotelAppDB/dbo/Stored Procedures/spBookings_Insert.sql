-- ====================================================================================
-- Stored Procedure: spBookings_Insert
-- 
-- Purpose:
--   Inserts a new booking record into the `Bookings` table for a specific room and guest.
--
-- Parameters:
--   @roomId (INT)      - The ID of the booked room.
--   @guestId (INT)     - The ID of the guest making the booking.
--   @startDate (DATE)  - The start date of the booking.
--   @endDate (DATE)    - The end date of the booking.
--   @totalCost (MONEY) - The total cost of the booking.
--
-- Logic:
--   1. Adds a new booking record to the `Bookings` table.
--   2. Includes details such as room ID, guest ID, start date, end date, and total cost.
--
-- Returns:
--   - No result set is returned.

CREATE PROCEDURE [dbo].[spBookings_Insert]
	@roomId int,
	@guestId int,
	@startDate date,
	@endDate date,
	@totalCost money
AS
begin
	set nocount on;

	insert into dbo.Bookings(RoomId, GuestId, StartDate, EndDate, TotalCost)
	values (@roomId, @guestId, @startDate, @endDate, @totalCost);

end
