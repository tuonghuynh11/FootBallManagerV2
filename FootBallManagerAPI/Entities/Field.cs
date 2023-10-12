using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Field
{
    public int IdField { get; set; }

    public int? IdDiaDiem { get; set; }

    public string? Images { get; set; }

    public string? FieldName { get; set; }

    public string? TechnicalInformation { get; set; }

    public int? NumOfSeats { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Footballmatch> Footballmatches { get; set; } = new List<Footballmatch>();

    public virtual Diadiem? IdDiaDiemNavigation { get; set; }

    public virtual ICollection<Service> IdServices { get; set; } = new List<Service>();
}
