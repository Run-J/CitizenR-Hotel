CREATE PROCEDURE [dbo].[spRoomTypes_GetById]
	@Id int
AS
begin
	set nocount on

	select [Id], [Title], [Description], [Price], [ImageUrl]
	from dbo.RoomTypes
	where Id = @id;

end
