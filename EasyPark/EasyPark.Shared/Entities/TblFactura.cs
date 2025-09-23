using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;
public partial class TblFactura
{

    [JsonIgnore]
    public int IdFactura { get; set; }

    public DateTime FechaHoraSalida { get; set; }

    public decimal Monto { get; set; }

    public int IdTicket { get; set; }

    public int IdEmpleado { get; set; }

    public int IdTarifa { get; set; }

    public string Estado { get; set; } = null!;

    [JsonIgnore]

    public virtual TblEmpleado IdEmpleadoNavigation { get; set; } = null!;
    [JsonIgnore]


    public virtual TblTarifa IdTarifaNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual TblTicketEntradum IdTicketNavigation { get; set; } = null!;
}
