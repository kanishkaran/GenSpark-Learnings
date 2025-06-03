# HealthCareAPI - Appointment Service Task

## AppointmentService

The [`AppointmentService`](HealthCareAPI/Services/AppointmentService.cs) class handles core appointment-related business logic. It provides methods to:

- **Book an Appointment:**  
  Validates doctor and patient existence, checks appointment date, and creates a new appointment with status "Booked".

- **Cancel an Appointment:**  
  Validates appointment and doctor, ensures the appointment isn't already cancelled, and updates the status to "Cancelled".

Both methods interact with repository interfaces for data access and use AutoMapper for DTO-to-entity mapping.

## DTOs Used

- **AppointmentAddRequestDto:**  
  Used when booking an appointment. Contains details like `DoctorId`, `PatientId`, and `AppointmentDate`.

- **AppointmentCancelRequestDto:**  
  Used when cancelling an appointment. Contains `AppointmentId` and `DoctorId` to identify the appointment and verify the doctor.

## Authorization Policy

### MinExpYears.cs

[`MinExpYears`](HealthCareAPI/Authorization/MinExpYears.cs) defines a custom authorization requirement specifying a minimum years of experience (e.g., 3 years).

### MinExpYearsHandler.cs

[`MinExpYearsHandler`](HealthCareAPI/Authorization/MinExpYearsHandler.cs) is the handler for the above requirement. It checks the authenticated user's `YearsOfExp` claim and ensures it meets or exceeds the required minimum. If so, the policy succeeds and access is granted.

---