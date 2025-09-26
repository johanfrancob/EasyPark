using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblVehiculo
{
    public string Placa { get; set; } = null!;

    public int IdTipoVehiculo { get; set; }

    public string? Color { get; set; }

    public string? Marca { get; set; }
    [JsonIgnore]

    public virtual TblTipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}
