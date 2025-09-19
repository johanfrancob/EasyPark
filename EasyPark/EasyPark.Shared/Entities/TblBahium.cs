using System;
using System.Collections.Generic;

namespace EasyPark.Shared.Entities;

public class TblBahium
{
    public int IdBahia { get; set; }

    public string Estado { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}
