using Api_Kaos_Net.DTO;

namespace Api_Kaos_Net.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(int id);
        Task<RoleDto> CreateAsync(RoleDto dto);
        Task<bool> UpdateAsync(int id, RoleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
