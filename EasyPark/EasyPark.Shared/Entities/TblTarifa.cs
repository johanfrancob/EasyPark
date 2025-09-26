using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblTarifa
{
    public int IdTarifa { get; set; }

    public int IdTipoVehiculo { get; set; }

    public decimal ValorHora { get; set; }
    [JsonIgnore]

    public virtual TblTipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<TblFactura> TblFacturas { get; set; } = new List<TblFactura>();
}
