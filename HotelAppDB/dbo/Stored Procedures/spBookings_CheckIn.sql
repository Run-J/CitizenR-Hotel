-- ====================================================================================
-- Stored Procedure: spBookings_CheckIn
-- 
-- Purpose:
--   Updates a booking record to mark a guest as "Checked In" based on the booking ID.
--
-- Parameters:
--   @Id (INT) - The unique identifier of the booking to be updated.
--
-- Logic:
--   1. Searches for the booking with the specified `Id`.
--   2. Updates the `CheckedIn` column to `1` to indicate the guest has checked in.
--
-- Returns:
--   - No data is returned by this procedure.
--   - The specified booking record is updated in the `Bookings` table.
--


CREATE PROCEDURE [dbo].[spBookings_CheckIn]
	@Id int
AS
begin
	set nocount on

	update dbo.Bookings
	set CheckedIn = 1
	where Id = @Id;
end
