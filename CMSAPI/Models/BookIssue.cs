using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class BookIssue
{
    public Guid BookIssueId { get; set; }

    public Guid BookId { get; set; }

    public string StudentId { get; set; } = null!;

    public DateOnly IssueDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;
}
