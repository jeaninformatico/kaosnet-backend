using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface IStreamingAccountService
    {
        Task<IEnumerable<StreamingAccountDto>> GetAllAsync();
        Task<StreamingAccountDto?> GetByIdAsync(int id);
        Task<StreamingAccountDto> CreateAsync(StreamingAccountDto dto);
        Task<bool> UpdateAsync(int id, StreamingAccountDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
