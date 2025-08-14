using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name cannot contain numbers or special characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Phone number must be digits and may include a plus sign.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Job Title is required.")]
        [StringLength(50, ErrorMessage = "Job Title cannot exceed 50 characters.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public decimal Salary { get; set; }

        [StringLength(50, ErrorMessage = "Department name cannot exceed 50 characters.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Hire Date is required.")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Manager ID is required.")]
        public int ManagerId { get; set; }
    }
}
