using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblBahium
{
    public int IdBahia { get; set; }

    public string Estado { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public int IdTipoVehiculo { get; set; }

    [JsonIgnore]
    public virtual TblTipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}
