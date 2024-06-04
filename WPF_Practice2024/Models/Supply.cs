using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class Supply
{
    public int IdSupply { get; set; }

    public int Price { get; set; }

    public int IdAgent { get; set; }

    public int IdClient { get; set; }

    public int IdRealEstate { get; set; }

    public virtual Agent IdAgentNavigation { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual RealEstate IdRealEstateNavigation { get; set; } = null!;

    public virtual ICollection<Demand> IdDemands { get; set; } = new List<Demand>();
}
