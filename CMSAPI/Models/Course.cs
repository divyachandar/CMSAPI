using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<FeeStructure> FeeStructures { get; set; } = new List<FeeStructure>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
