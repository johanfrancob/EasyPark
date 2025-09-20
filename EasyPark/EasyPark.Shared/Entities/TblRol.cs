using System;
using System.Collections.Generic;

namespace EasyPark.Shared.Entities;
public partial class TblRol
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<TblEmpleado> TblEmpleados { get; set; } = new List<TblEmpleado>();
}
