using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models;

public class Employee
{
    public Int64 Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; }

    [Phone]
    public string Phone { get; set; }

    [Required]
    [StringLength(50)]
    public string JobTitle { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
    public decimal Salary { get; set; }

    [StringLength(50)]
    public string Department { get; set; }

    [Required]
    public DateTime HireDate { get; set; }

    public int ManagerId { get; set; }
}
