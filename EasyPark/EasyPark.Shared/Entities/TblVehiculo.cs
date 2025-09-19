using System;
using System.Collections.Generic;

namespace EasyPark.Shared.Entities;
public class TblVehiculo
{
    public string Placa { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string? Color { get; set; }

    public string? Marca { get; set; }

    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}
