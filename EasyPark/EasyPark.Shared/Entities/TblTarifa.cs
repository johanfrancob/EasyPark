using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;
public partial class TblTarifa
{
    public int IdTarifa { get; set; }

    public string TipoVehiculo { get; set; } = null!;

    public decimal ValorHora { get; set; }
    [JsonIgnore]


    public virtual ICollection<TblFactura> TblFacturas { get; set; } = new List<TblFactura>();
}
