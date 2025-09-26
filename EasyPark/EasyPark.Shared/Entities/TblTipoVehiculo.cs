using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblTipoVehiculo
{
    public int IdTipoVehiculo { get; set; }

    public string Nombre { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<TblBahium> TblBahia { get; set; } = new List<TblBahium>();
    [JsonIgnore]

    public virtual ICollection<TblTarifa> TblTarifas { get; set; } = new List<TblTarifa>();
    [JsonIgnore]

    public virtual ICollection<TblVehiculo> TblVehiculos { get; set; } = new List<TblVehiculo>();
}
