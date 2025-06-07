using System;
using System.Collections.Generic;

namespace CertBE.Models;

public partial class ExamDate
{
    public int Id { get; set; }

    public int CertId { get; set; }

    public DateTime Startt { get; set; }

    public DateTime Endd { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Cert Cert { get; set; } = null!;
}
