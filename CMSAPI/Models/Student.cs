using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Address { get; set; }

    public int CampusId { get; set; }

    public int DepartmentId { get; set; }

    public int CourseId { get; set; }

    public string Batch { get; set; } = null!;

    public DateOnly AdmissionDate { get; set; }

    public string Status { get; set; } = null!;

    public string? GuardianName { get; set; }

    public string? GuardianPhone { get; set; }

    public string? GuardianEmail { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Campus Campus { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
