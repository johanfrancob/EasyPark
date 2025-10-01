using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPark.Backend.Repositories;
using EasyPark.Backend.Services.Abstractions;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend.Services
{
    public sealed class BahiasService : IBahiasService
    {
        private readonly IRepository<TblBahium> _bahias;

        public BahiasService(IRepository<TblBahium> bahias) => _bahias = bahias;

        public Task<IReadOnlyList<TblBahium>> DisponiblesAsync()
            => _bahias.ListAsync(b => b.Estado == "Disponible");

        public async Task OcuparAsync(int idBahia)
        {
            var bahia = await _bahias.GetByIdAsync(idBahia)
                        ?? throw new InvalidOperationException("Bahía no encontrada.");
            bahia.Estado = "Ocupada";
            _bahias.Update(bahia);
            await _bahias.SaveChangesAsync();
        }

        public async Task LiberarAsync(int idBahia)
        {
            var bahia = await _bahias.GetByIdAsync(idBahia)
                        ?? throw new InvalidOperationException("Bahía no encontrada.");
            bahia.Estado = "Disponible";
            _bahias.Update(bahia);
            await _bahias.SaveChangesAsync();
        }
    }
}
