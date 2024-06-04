using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class Land
{
    public int IdLand { get; set; }

    public double? TotalArea { get; set; }

    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
