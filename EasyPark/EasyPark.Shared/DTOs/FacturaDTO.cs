using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPark.Shared.DTOs;

public class FacturaDTO
{
    public int IdFactura { get; set; }
    public DateTime FechaHoraSalida { get; set; }
    public decimal Monto { get; set; }

    public int IdTicket { get; set; }
    public string Placa { get; set; } = string.Empty;
    public DateTime FechaHoraEntrada { get; set; }

    public string Cliente { get; set; } = "Consumidor Final";
    public string Documento { get; set; } = "222222222222";
}
