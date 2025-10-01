using System;
using EasyPark.Backend.Services.Abstractions;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend.Services
{
    public sealed class TarifaPorHoraStrategy : ITarifaStrategy
    {
        public decimal Calcular(DateTime entrada, DateTime salida, TblTarifa tarifa)
        {
            var horas = (decimal)Math.Ceiling((salida - entrada).TotalHours);
            if (horas < 1) horas = 1;
            return horas * tarifa.ValorHora;
        }
    }
}
