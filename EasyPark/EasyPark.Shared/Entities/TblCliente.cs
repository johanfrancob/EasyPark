using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblCliente
{

    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string? Telefono { get; set; }
    [JsonIgnore]
    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}
