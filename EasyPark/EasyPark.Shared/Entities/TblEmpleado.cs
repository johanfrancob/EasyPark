using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblEmpleado
{

    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Documento { get; set; } = null!;

    public string? Telefono { get; set; }

    public int IdRol { get; set; }
    [JsonIgnore]

    public virtual TblRol IdRolNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<TblFactura> TblFacturas { get; set; } = new List<TblFactura>();
    [JsonIgnore]
    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();
}
