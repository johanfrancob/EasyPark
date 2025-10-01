using System.Threading.Tasks;

namespace EasyPark.Backend.Services.Abstractions
{
    public interface IBillingService
    {
        Task<decimal> CalcularMontoAsync(int idTicket);
    }
}
