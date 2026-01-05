using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public string ExamName { get; set; } = null!;

    public DateOnly ExamDate { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}
