# R Hotel

## Overview
A comprehensive hotel reservation and management platform hosted on Azure, allowing customers to seamlessly search for room availability, book accommodations via a hotel web portal, and facilitate efficient check-in processes via a desktop app installed in the hotel.

---

## Creative Inspiration
During my summer vacation in 2024, I traveled to Taipei and stayed at the **CitizenM Hotel**. Using Booking.com to make a reservation, I experienced their seamless check-in process, facilitated by a desktop app in the hotel lobby. Inspired by this system, I set out to create a similar comprehensive hotel reservation and management platform.

### My Booking Order on Booking Web
<div align="center">
  <img src="https://github.com/user-attachments/assets/dbe37280-8562-421e-a448-5c5c2c7556b9" alt="MyBookingOrder">
</div>

### The Desktop App for Check-In at CitizenM Hotel Lobby
<div align="center">
  <img src="https://github.com/user-attachments/assets/68c24927-b8ab-4e6a-b577-28ec17b483d5" alt="CitizenMDesktopApp">
</div>

---

## My R Hotel Demonstration

### 1: R Hotel Web Portal

#### 1.1 Searching for Available Rooms and Booking

In this example, we will search for an executive room with a check-in date of **01/13/2025** and a check-out date of **01/15/2025**, and then book the room using the guest's full name. This example also demonstrates input validation while booking, ensuring that a room cannot be rebooked once it has already been booked.

> **Note**:  
> The executive room can only be booked once at a time because there is only one executive room available.  
> In contrast, the king-size room and double queen-size room have three and two rooms available, respectively.  
> In other words, the king-size room can accommodate up to three reservations at the same time, while the double queen-size room can accommodate up to two.


<div align="center">
  <img src="https://github.com/user-attachments/assets/ff045160-6354-4c19-8717-57ccbdcb1735" alt="RHotelWebDemo">
</div>

#### 1.2 Viewing the **Booking Table** in the Database After Booking
After confirming the booking, the **BookingsTable** in the database reflects the changes. It stores all reservation details, including the guest's name, room type, and booking dates.

<div align="center">
  <img src="https://github.com/user-attachments/assets/d618e4af-f92f-416e-bab8-a187a69cf4fa" alt="BookingTableAfterBook">
</div>

#### 1.3 Exploring Related Tables in the Database
The portal’s database maintains a structured relationship between various tables. Here's how the **BookingsTable** links to other tables:

##### **a. Guests Table**
The `userId` in the **BookingsTable** maps to the corresponding entry in the **Guests Table**. For instance, `userId 1` is associated with "Run Ji," which was used during the booking process.

<div align="center">
  <img src="https://github.com/user-attachments/assets/b9233127-e15c-4ac9-9f92-597c3accd36a" alt="NameTableAfterBook">
</div>

##### **b. Room Type Table**
The `roomId` in the **BookingsTable** maps to the corresponding room type in the **Room Type Table**. For example, `roomId 3` represents the **executive room type**.

<div align="center">
  <img src="https://github.com/user-attachments/assets/1ec0130e-bfbf-4130-8ff5-d5a31fae9d32" alt="RoomTypeTableAfterBook">
</div>

##### **c. Rooms Table**
The `roomNumber` in the **Rooms Table** links back to the room type in the **Room Type Table**. For example, **Room Number 301** is associated with the executive room type, as shown in the **BookingsTable**.

<div align="center">
  <img src="https://github.com/user-attachments/assets/a328e762-87b7-4de7-8780-5787403f2463" alt="RoomsTableAfterBook">
</div>

---

### 2: R Hotel Desktop App Check-in Portal

#### 2.1 Searching for Reservations by Guest Last Name
In this example, we check in a reservation via the desktop app using the guest's last name, "Ji." The app displays reservation details, including the guest's full name, check-in/out dates, assigned room number, and total amount.

<div align="center">
  <img src="https://github.com/user-attachments/assets/19b34191-ff34-4867-b11f-495462d28fe1" alt="RHotelWPFDemo">
</div>

#### 2.2 Viewing the **Booking Table** in the Database After Check-In
Once the guest is checked in, the "Check-in" status in the **BookingsTable** updates from `0` to `1`.

<div align="center">
  <img src="https://github.com/user-attachments/assets/309020ac-dd9b-4d27-9a4c-6a7b9d37ef48" alt="BookingsTableAfterCheckIn">
</div>

---

## Technology Stack

- **Frontend**: HTML/CSS/JavaScript, Razor Pages, WPF
- **Backend**: C#, ASP.NET Core, .NET Core, Dapper
- **Database**: MS SQL Server, Azure SQL Database
- **Cloud Services**: Azure App Service, Azure SQL Server

---

## Key Features

### 1. Stored Procedures with Dapper
Implemented stored procedures to ensure efficient and secure database operations using Dapper, a lightweight and fast ORM. The stored procedures handle tasks such as booking management and data retrieval.

<div align="center">
  <img src="https://github.com/user-attachments/assets/9fe2a2f0-e34d-46d2-bac1-32617f43382d" alt="StoredProceduresWithDapper">
</div>

### 2. Generic Programming and Dependency Injection (DI)
Utilized generic programming and dependency injection to create modular, reusable, and testable components. This approach reduces code duplication and enhances maintainability.

<div align="center">
  <img src="https://github.com/user-attachments/assets/a714fe5e-2f22-47c3-8635-9f6de7c3eff7" alt="GenericProgrammingAndDI">
</div>
