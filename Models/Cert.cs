﻿using System;
using System.Collections.Generic;

namespace CertBE.Models;

public partial class Cert
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ExamDate> ExamDates { get; set; } = new List<ExamDate>();
}
