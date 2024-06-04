using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public virtual ICollection<Demand> Demands { get; set; } = new List<Demand>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
