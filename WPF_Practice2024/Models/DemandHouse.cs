using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class DemandHouse
{
    public int IdHouse { get; set; }

    public double? MinArea { get; set; }

    public double? MaxArea { get; set; }

    public int? MinRooms { get; set; }

    public int? MaxRooms { get; set; }

    public int? MinFloor { get; set; }

    public int? MaxFloor { get; set; }

    public virtual ICollection<Demand> Demands { get; set; } = new List<Demand>();
}
