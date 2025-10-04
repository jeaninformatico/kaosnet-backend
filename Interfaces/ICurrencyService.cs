using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetAllAsync();
        Task<CurrencyDto?> GetByIdAsync(int id);
        Task<CurrencyDto> CreateAsync(CurrencyDto dto);
        Task<bool> UpdateAsync(int id, CurrencyDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
