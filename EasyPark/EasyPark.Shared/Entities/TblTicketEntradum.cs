using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblTicketEntradum
{
    public int IdTicket { get; set; }

    public DateTime FechaHoraEntrada { get; set; }

    public string Placa { get; set; } = null!;

    public int IdCliente { get; set; }

    public int IdBahia { get; set; }
    [JsonIgnore]

    public virtual TblBahium IdBahiaNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual TblCliente IdClienteNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual TblVehiculo PlacaNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<TblFactura> TblFacturas { get; set; } = new List<TblFactura>();
}
