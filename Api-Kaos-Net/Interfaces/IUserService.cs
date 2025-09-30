using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto dto);
        Task<bool> UpdateAsync(int id, UserDto dto);
        Task<bool> DeleteAsync(int id);
        Task<UserDto?> LoginAsync(string email, string secretAnswer);
    }
}
