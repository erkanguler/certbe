using System;
using System.Collections.Generic;

namespace CertBE.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ExamDateId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ExamDate ExamDate { get; set; } = null!;
}
