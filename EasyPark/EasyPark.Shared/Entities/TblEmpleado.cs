using System;
using System.Collections.Generic;

namespace EasyPark.Shared.Entities;
public partial class  TblEmpleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string? Telefono { get; set; }

    public int IdRol { get; set; }

    public virtual TblRol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<TblFactura> TblFacturas { get; set; } = new List<TblFactura>();

    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();
}
