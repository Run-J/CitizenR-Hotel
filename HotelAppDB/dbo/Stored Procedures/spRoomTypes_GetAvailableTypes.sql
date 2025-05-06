-- ====================================================================================
-- Stored Procedure: [dbo].[spRoomTypes_GetAvailableTypes]
--
-- Description:
--     Retrieves a list of room types along with the count of available rooms
--     for each type within a specified date range. This excludes rooms that are 
--     already booked during the overlap period.
--
-- Parameters:
--     @startDate (DATE) - Check-in date (inclusive)
--     @endDate   (DATE) - Check-out date (exclusive)
--
-- Logic:
--     1. Joins RoomTypes and Rooms to associate each room with its type.
--     2. Filters out rooms that are already booked for any overlapping period.
--     3. Groups the results by room type and counts the number of available rooms.
--
-- Output:
--     Id           - RoomType ID
--     Title        - Room type name
--     Description  - Room type description
--     Price        - Price per night
--     ImageUrl     - Optional image URL for display
--     AvailableRooms - Number of rooms available for the given date range
-- ====================================================================================
CREATE PROCEDURE [dbo].[spRoomTypes_GetAvailableTypes]
    @startDate DATE,
    @endDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        rt.Id, 
        rt.Title, 
        rt.Description, 
        rt.Price, 
        rt.ImageUrl, 
        COUNT(r.Id) AS AvailableRooms
    FROM dbo.RoomTypes rt
    INNER JOIN dbo.Rooms r ON r.RoomTypesId = rt.Id
    WHERE r.Id NOT IN (
        SELECT bk.RoomId
        FROM dbo.Bookings bk
        WHERE NOT (bk.EndDate <= @startDate OR bk.StartDate >= @endDate)
    )
    GROUP BY rt.Id, rt.Title, rt.Description, rt.Price, rt.ImageUrl;
END
