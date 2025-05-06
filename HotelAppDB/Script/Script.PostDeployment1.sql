/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- Check if the RoomTypes table is empty
if not exists (select 1 from dbo.RoomTypes)
begin 
    -- Insert default room types into the RoomTypes table
    insert into dbo.RoomTypes(Title, Description, Price)
    values
        ('King Size Bed', 'A room with a king-size bed and a window.', 100),
        ('Two Queen Size Bed', 'A room with two queen-size beds and a window.', 115),
        ('Executive Suite', 'Two rooms, each with a king-size bed and a window.', 205);
end


-- Check if the Rooms table is empty
-- Insert default RoomTypes if none exist
IF NOT EXISTS (SELECT 1 FROM dbo.RoomTypes)
BEGIN 
    INSERT INTO dbo.RoomTypes (Title, Description, Price, ImageUrl)
    VALUES
        ('King Size Bed', 'A room with a king-size bed and a window.', 100, '/images/KingSize.jpg'),
        ('Two Queen Size Bed', 'A room with two queen-size beds and a window.', 115, '/images/TwoQueen.jpg'),
        ('Executive Suite', 'Two rooms, each with a king-size bed and a window.', 205, '/images/Suite.jpg');
END

-- Insert Rooms if table is empty
IF NOT EXISTS (SELECT 1 FROM dbo.Rooms)
BEGIN
    DECLARE @roomId1 INT, @roomId2 INT, @roomId3 INT;

    SELECT @roomId1 = Id FROM dbo.RoomTypes WHERE Title = 'King Size Bed';
    SELECT @roomId2 = Id FROM dbo.RoomTypes WHERE Title = 'Two Queen Size Bed';
    SELECT @roomId3 = Id FROM dbo.RoomTypes WHERE Title = 'Executive Suite';

    INSERT INTO dbo.Rooms (RoomNumber, RoomTypesId)
    VALUES 
        ('101', @roomId1),
        ('102', @roomId1),
        ('103', @roomId1),
        ('201', @roomId2),
        ('202', @roomId2),
        ('301', @roomId3);
END