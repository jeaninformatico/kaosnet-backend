using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface IPasswordService
    {
        Task<IEnumerable<PasswordDto>> GetAllAsync();
        Task<PasswordDto?> GetByIdAsync(int id);
        Task<PasswordDto> CreateAsync(PasswordDto dto);
        Task<bool> UpdateAsync(int id, PasswordDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
