using System;
using System.Collections.Generic;

namespace EasyPark.Shared.Entities;
public class TblCliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}
