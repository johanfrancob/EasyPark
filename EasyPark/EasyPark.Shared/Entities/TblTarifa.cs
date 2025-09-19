using System;
using System.Collections.Generic;

namespace EasyPark.Shared.Entities;
public class TblTarifa
{
    public int IdTarifa { get; set; }

    public string TipoVehiculo { get; set; } = null!;

    public decimal ValorHora { get; set; }

    public virtual ICollection<TblFactura> TblFacturas { get; set; } = new List<TblFactura>();
}
