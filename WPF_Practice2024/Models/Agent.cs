using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class Agent
{
    public int IdAgent { get; set; }

    public int? DealShare { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public virtual ICollection<Demand> Demands { get; set; } = new List<Demand>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
