using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class Book
{
    public Guid BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();
}
