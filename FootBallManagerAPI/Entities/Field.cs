using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Field
{
    public int IdField { get; set; }

    public int? IdDiaDiem { get; set; }

    public byte[]? Images { get; set; }

    public string? FieldName { get; set; }

    public string? TechnicalInformation { get; set; }

    public int? NumOfSeats { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Fieldservice> Fieldservices { get; set; } = new List<Fieldservice>();

    public virtual ICollection<Footballmatch> Footballmatches { get; set; } = new List<Footballmatch>();

    public virtual Diadiem? IdDiaDiemNavigation { get; set; }
}
