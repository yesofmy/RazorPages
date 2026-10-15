using System.ComponentModel.DataAnnotations;

namespace StudentAccounting.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the student's full name.")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter the academic group.")]
    [StringLength(20)]
    public string Group { get; set; } = string.Empty;

    [Range(0, 100, ErrorMessage = "The average grade must be between 0 and 100.")]
    public decimal AverageGrade { get; set; }
}