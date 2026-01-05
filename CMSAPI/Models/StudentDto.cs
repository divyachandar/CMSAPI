using System;

namespace CMSAPI.Models;

public class StudentDto
{
    public int StudentId { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Address { get; set; }
    public int CampusId { get; set; }
    public int DepartmentId { get; set; }
    public int CourseId { get; set; }
    public string Batch { get; set; } = string.Empty;
    public DateTime AdmissionDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? GuardianName { get; set; }
    public string? GuardianPhone { get; set; }
    public string? GuardianEmail { get; set; }
}