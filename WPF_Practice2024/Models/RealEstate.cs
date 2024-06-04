using System;
using System.Collections.Generic;

namespace WPF_Practice2024.Models;

public partial class RealEstate
{
    public int IdRealEstate { get; set; }

    public int? IdDistrict { get; set; }

    public string? AdressCity { get; set; }

    public string? AdressStreet { get; set; }

    public string? AdressHouse { get; set; }

    public string? AdressNumber { get; set; }

    public double? CoordinateLatitude { get; set; }

    public double? CoordinateLongitude { get; set; }

    public int? IdApartment { get; set; }

    public int? IdHouse { get; set; }

    public int? IdLand { get; set; }

    public virtual Apartment? IdApartmentNavigation { get; set; }

    public virtual District? IdDistrictNavigation { get; set; }

    public virtual House? IdHouseNavigation { get; set; }

    public virtual Land? IdLandNavigation { get; set; }

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
