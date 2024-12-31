-- ====================================================================================
-- Stored Procedure: spGuests_Insert
-- 
-- Purpose:
--   Adds a new guest to the `Guests` table if they do not already exist.
--   Returns the guest's details after insertion or if they already exist.
--
-- Parameters:
--   @firstName (NVARCHAR(50)) - The first name of the guest.
--   @lastName (NVARCHAR(50))  - The last name of the guest.
--
-- Logic:
--   1. Checks if a guest with the given first and last name already exists.
--   2. If the guest does not exist:
--      - Inserts a new record into the `Guests` table.
--   3. Selects and returns the guest's `Id`, `FirstName`, and `LastName`.
--
-- Returns:
--   - Id: Unique identifier of the guest.
--   - FirstName: The guest's first name.
--   - LastName: The guest's last name.

CREATE PROCEDURE [dbo].[spGuests_Insert]
	@firstName nvarchar(50),
	@lastName nvarchar(50)
AS
begin
	set nocount on;

	if not exists (select 1 from dbo.Guests where FirstName = @firstName and LastName = @lastName)
	begin
		insert into dbo.Guests (FirstName, LastName)
		values (@firstName, @lastName);
	end

	select top 1 [Id], [FirstName], [LastName]
	from dbo.Guests
	where FirstName = @firstName and LastName = @lastName;
end
