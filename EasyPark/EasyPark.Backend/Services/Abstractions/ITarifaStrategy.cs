using System;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend.Services.Abstractions
{
    public interface ITarifaStrategy
    {
        decimal Calcular(DateTime entrada, DateTime salida, TblTarifa tarifa);
    }
}
