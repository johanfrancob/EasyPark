using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;
public partial class TblVehiculo
{
    public string Placa { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string? Color { get; set; }

    public string? Marca { get; set; }

    [JsonIgnore]

    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}
