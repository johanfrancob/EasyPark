using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend.Services.Abstractions
{
    public interface IBahiasService
    {
        Task<IReadOnlyList<TblBahium>> DisponiblesAsync();
        Task OcuparAsync(int idBahia);
        Task LiberarAsync(int idBahia);
    }
}
