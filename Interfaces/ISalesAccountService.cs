using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface ISalesAccountService
    {
        Task<IEnumerable<SalesAccountDto>> GetAllAsync();
        Task<SalesAccountDto?> GetByIdAsync(int id);
        Task<SalesAccountDto> CreateAsync(SalesAccountDto dto);
        Task<bool> UpdateAsync(int id, SalesAccountDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
