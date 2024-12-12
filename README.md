# Dentist Appointment System

A robust web API for managing dentist appointments, built using ASP.NET Core and Entity Framework Core. The system handles appointment scheduling, dentist management, and patient data, with database integration and RESTful API endpoints.

## Table of Contents

1. [Introduction](#introduction)
2. [Features](#features)
3. [Installation](#installation)
4. [Usage](#usage)
5. [API Endpoints](#api-endpoints)
6. [Database Structure](#database-structure)
7. [Testing](#testing)
8. [Examples](#examples)
9. [Contributors](#contributors)
10. [License](#license)

## Introduction

The Dentist Appointment System allows clinics to efficiently manage appointments, dentists, and patients. It includes features for real-time scheduling, availability checks, and data storage using Entity Framework Core.

## Features

- **Appointment Scheduling**: Create and manage appointments, ensuring no overlap for dentists.
- **Dentist and Patient Management**: Add, retrieve, and manage data for dentists and patients.
- **Database Integration**: Seamlessly stores and retrieves data using Entity Framework Core.
- **RESTful API**: Exposes endpoints for integration with front-end applications or other services.

## Installation

### Prerequisites

- [.NET 6.0 or later](https://dotnet.microsoft.com/)
- A database system compatible with Entity Framework Core (e.g., SQL Server, SQLite).

### Build Project
```bash
       dotnet build
```
### Restore Project
```bash
      dotnet restore 
```
## Usage

The application exposes RESTful endpoints to manage dentists, patients, and appointments. Use tools like Postman or Swagger UI to interact with the API.

### Running the Main Program

 To run this project you go to **./DentistAppointmetnSystem/src** 

 ```bash
        dotnet run
   ```



### API Endpoints
  
  - Get All Appointments
       ```bash
          GET /api/appointments
       ```
    Retrieves a list of all appointments with associated dentist and patient details.
    

  - Schedule an Appointment
       ```bash
          POST /api/appointments
       ```     
       
    Request Body :

       ```bash
            {
               "appointmentDate": "2024-12-12T10:00:00",
               "dentistId": 1,
               "patientId": 1,
               "notes": "Follow-up appointment"
            }    
       ```
### Dentists

 - Get All Patients
    ```bash
        GET /api/patients
    ```   
 - Add a Patient

    ```bash
       POST /api/patients
    ```
   Request Body:

     ```bash
        {
         "name": "John Doe",
         "phone": "123-456-7890"
        }
     ``` 
## Database Structure

 ### Entities
 
  1. Dentist 

     - **Id**: Unique dentist ID
     - **Name**: Dentist's name
     - **Specialty**: Dentist's area of expertise
    
  2. Patient
     
     - **Id**: Unique patient ID
     - **Name**: Patient's name
     - **Phone**: Contact number
  3. Appointment

     - **Id**: Unique appointment ID
     - **AppointmentDate**: Date and time
     - **DentistId**: Associated dentist
     - **PatientId**: Associated patient
     - **Notes**: Additional details

### Relationships
   
- #### One-to-Many:

   - Dentists ↔ Appointments
   - Patients ↔ Appointments


## Testing
The project includes unit tests to validate:

- Appointment scheduling
- Data retrieval for dentists and patients
- Error scenarios like overlapping appointments

### Run tests:

```bash
   dotnet test 
```

## Examples

### Schedule an Appointment

```bash
var appointment = new Appointment
{
    AppointmentDate = DateTime.Now.AddDays(1),
    DentistId = 1,
    PatientId = 2,
    Notes = "Routine checkup"
};
await context.Appointments.AddAsync(appointment);
await context.SaveChangesAsync();
```

### Fetch All Dentists

```bash
var dentists = await context.Dentists.ToListAsync();
foreach (var dentist in dentists)
{
    Console.WriteLine($"{dentist.Id} - {dentist.Name}");
}
```

## Contributors

- **Fiordi Toska** - Project Developer
- Contributions welcome! Feel free to submit a pull request or raise an issue.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.


