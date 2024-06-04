using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class District
{
    public int IdDistrict { get; set; }

    public string? Names { get; set; }

    public string? Area { get; set; }

    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
