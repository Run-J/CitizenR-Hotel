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
if not exists (select 1 from dbo.Rooms)
begin
    -- Declare variables to store RoomType IDs
    declare @roomId1 int; -- For 'King Size Bed'
    declare @roomId2 int; -- For 'Two Queen Size Bed'
    declare @roomId3 int; -- For 'Executive Suite'

    -- Fetch the RoomType IDs based on Title
    select @roomId1 = Id from dbo.RoomTypes where Title = 'King Size Bed';
    select @roomId2 = Id from dbo.RoomTypes where Title = 'Two Queen Size Bed';
    select @roomId3 = Id from dbo.RoomTypes where Title = 'Executive Suite';

    -- Insert room records into the Rooms table with associated RoomType IDs
    insert into dbo.Rooms (RoomNumber, RoomTypesId)
    values 
        ('101', @roomId1), -- Room 101 with 'King Size Bed'
        ('102', @roomId1), -- Room 102 with 'King Size Bed'
        ('103', @roomId1), -- Room 103 with 'King Size Bed'
        ('201', @roomId2), -- Room 201 with 'Two Queen Size Bed'
        ('202', @roomId2), -- Room 202 with 'Two Queen Size Bed'
        ('301', @roomId3); -- Room 301 with 'Executive Suite'
end