using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class Apartment
{
    public int IdApartment { get; set; }

    public double? TotalArea { get; set; }

    public int? Rooms { get; set; }

    public int? Floors { get; set; }

    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
