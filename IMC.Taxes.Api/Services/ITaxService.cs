using System.Threading.Tasks;
using IMC.Taxes.Api.Models;

namespace IMC.Taxes.Api.Services
{
    public interface ITaxService
    {
        Task<Tax> GetTaxesByLocationAsync();
        Task<Tax> GetOrderTaxesAsync(Order order);
    }
}