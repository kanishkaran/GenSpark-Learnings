# DoctorService Methods Explained

This service manages doctors and their specializations. Here’s a more detailed explanation of each method:

---

### 1. `Task<Doctor> AddDoctor(DoctorAddRequestDto doctor)`
- **Purpose:** Adds a new doctor to the system, along with their specializations.
- **How it works:**
  - Creates a new `Doctor` entity and saves it.
  - For each specialization in the request:
    - Checks if the specialization exists.
    - If not, creates and saves the new specialization.
    - Links the doctor and specialization via a `DoctorSpecialization` entity.
- **Returns:** The created `Doctor` object, or an empty `Doctor` if an error occurs.

---

### 2. `Task<Doctor> GetDoctByName(string name)`
- **Purpose:** Finds a doctor by their name.
- **How it works:**
  - Retrieves all doctors.
  - Searches for a doctor whose name matches the input (case-insensitive).
- **Returns:** The matching `Doctor` object, or an empty `Doctor` if not found or on error.

---

### 3. `Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality)`
- **Purpose:** Gets all doctors with a specific specialization.
- **How it works:**
  - Finds the specialization by name.
  - Retrieves all doctor-specialization links for that specialization.
  - Collects and returns all doctors linked to it.
- **Returns:** A list of `Doctor` objects with the given specialization, or an empty list if none found or on error.

---

### 4. `Task<ICollection<Doctor>> GetAllDoctors()`
- **Purpose:** Lists all doctors in the system.
- **How it works:** Retrieves and returns all doctors from the repository.
- **Returns:** A list of all `Doctor` objects.

---

**Note:**  
All methods handle exceptions by logging the error and returning an empty result (empty object or list).