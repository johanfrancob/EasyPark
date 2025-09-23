using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;
public partial class TblUsuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int IdEmpleado { get; set; }

    public string Estado { get; set; } = null!;

    [JsonIgnore]

    public virtual TblEmpleado IdEmpleadoNavigation { get; set; } = null!;
}
