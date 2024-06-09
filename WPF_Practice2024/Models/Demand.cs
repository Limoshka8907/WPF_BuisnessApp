using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class Demand
{
    public int IdDemand { get; set; }

    public int IdClient { get; set; }

    public int IdAgent { get; set; }

    public string TypeRealEstate { get; set; } = null!;

    public string? Adress { get; set; }

    public int? MinPrice { get; set; }

    public int? MaxPrice { get; set; }

    public int? IdApartment { get; set; }

    public int? IdHouse { get; set; }

    public int? IdLand { get; set; }

    public virtual Agent IdAgentNavigation { get; set; } = null!;

    public virtual DemandApartment? IdApartmentNavigation { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual DemandHouse? IdHouseNavigation { get; set; }

    public virtual DemandLand? IdLandNavigation { get; set; }

    public virtual ICollection<Supply> IdSupplies { get; set; } = new List<Supply>();
    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();


}
