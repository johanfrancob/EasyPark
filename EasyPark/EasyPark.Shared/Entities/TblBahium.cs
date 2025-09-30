using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EasyPark.Shared.Entities;

public partial class TblBahium
{
    [JsonPropertyName("idBahia")]
    public int IdBahia { get; set; }

    [JsonPropertyName("estado")]
    public string Estado { get; set; } = null!;

    [JsonPropertyName("ubicacion")]
    public string? Ubicacion { get; set; }

    [JsonPropertyName("idTipoVehiculo")]
    public int IdTipoVehiculo { get; set; }

    [JsonIgnore]
    public virtual TblTipoVehiculo? IdTipoVehiculoNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblTicketEntradum> TblTicketEntrada { get; set; } = new List<TblTicketEntradum>();
}