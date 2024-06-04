using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class DemandLand
{
    public int IdLand { get; set; }

    public double? MinArea { get; set; }

    public double? MaxArea { get; set; }

    public virtual ICollection<Demand> Demands { get; set; } = new List<Demand>();
}
