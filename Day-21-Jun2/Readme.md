# Patient Module Overview

This document explains the main components related to patient management in the HealthCareAPI project: **PatientService**, **PatientController**, and **PatientProfile**.

---

## PatientService

**Location:** `HealthCareAPI/Services/PatientService.cs`

- Handles business logic for patient operations.
- Registers new patients by:
  - Mapping registration DTOs to `User` and `Patient` entities.
  - Encrypting the patient's password.
  - Saving the user and patient records to the database.
- Example method:  
  `RegisterPatient(PatientAddRequestDto patient)`  
  Registers a new patient and creates the corresponding user account.

---

## PatientController

**Location:** `HealthCareAPI/Controllers/PatientController.cs`

- Exposes API endpoints for patient operations.
- Handles HTTP requests related to patients.
- Example endpoint:  
  `POST /api/patient/register`  
  Calls `IPatientService.RegisterPatient` to register a new patient.

---

## PatientProfile

**Location:** `HealthCareAPI/Misc/PatientProfile.cs`

- AutoMapper profile for mapping between DTOs and the `Patient` entity.
- Defines mapping rules, e.g., from `PatientAddRequestDto` to `Patient`.
- Used by the service layer to simplify object transformations.

---

## Summary

- **PatientService**: Business logic for patients.
- **PatientController**: API endpoints for patient operations.
- **PatientProfile**: AutoMapper configuration for patient DTO/entity mapping.

For more details, see the respective files:
- `HealthCareAPI/Services/PatientService.cs`
- `HealthCareAPI/Controllers/PatientController.cs`
- `HealthCareAPI/Misc/PatientProfile.cs`