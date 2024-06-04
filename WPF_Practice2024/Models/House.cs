using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class House
{
    public int IdHouse { get; set; }

    public int? TotalFloor { get; set; }

    public double? TotalArea { get; set; }

    public int? TotalRooms { get; set; }

    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
